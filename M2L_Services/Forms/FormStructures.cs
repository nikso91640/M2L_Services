using System.Windows.Forms;
using M2L_Services.DataAccess;
using M2L_Services.Models;
using M2L_Services.Utils;

namespace M2L_Services.Forms;

/// <summary>
/// Formulaire de gestion des structures (ligues, clubs, associations, etc.).
/// Affiche la liste dans un DataGridView.
/// Les opérations d'ajout et de modification se font via FormStructureDetail.
/// Seul l'administrateur peut ajouter, modifier ou supprimer.
/// </summary>
public partial class FormStructures : Form
{
    private readonly StructureDAO _dao = new StructureDAO();
    private List<Structure> _structures = new List<Structure>();

    public FormStructures()
    {
        InitializeComponent();
    }

    // -------------------------------------------------------------------------
    // Chargement initial
    // -------------------------------------------------------------------------

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        ConfigurerGrille();
        ChargerStructures();
        AppliquerDroits();
    }

    /// <summary>Crée les colonnes du DataGridView.</summary>
    private void ConfigurerGrille()
    {
        _dgvStructures.Columns.Clear();

        _dgvStructures.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name    = "IdStructure",
            Visible = false
        });

        _dgvStructures.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name       = "Nom",
            HeaderText = "Nom de la structure",
            FillWeight = 60
        });

        _dgvStructures.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name       = "TypeStructure",
            HeaderText = "Type",
            FillWeight = 40
        });
    }

    /// <summary>Recharge la liste des structures depuis la base de données.</summary>
    private void ChargerStructures()
    {
        _dgvStructures.Rows.Clear();

        try
        {
            _structures = _dao.GetTout();
            AppliquerFiltre();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Erreur lors du chargement des structures :\n" + ex.Message,
                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void AppliquerFiltre()
    {
        // On récupère le texte saisi dans la barre de recherche (en minuscules pour comparer sans tenir compte de la casse)
        string filtre = _txtRecherche.Text.Trim().ToLower();

        _dgvStructures.Rows.Clear();

        // On parcourt toutes les structures chargées depuis la base
        foreach (Structure s in _structures)
        {
            // Si un filtre est saisi, on vérifie que le nom ou le type le contient
            if (filtre != "" && !s.Nom.ToLower().Contains(filtre) && !s.TypeStructure.ToLower().Contains(filtre))
                continue; // cette structure ne correspond pas → on passe à la suivante

            // La structure correspond aux critères : on l'ajoute à la grille
            _dgvStructures.Rows.Add(s.IdStructure, s.Nom, s.TypeStructure);
        }
    }

    /// <summary>Active ou désactive les boutons selon le rôle.</summary>
    private void AppliquerDroits()
    {
        bool estAdmin = Session.EstAdmin;
        _btnAjouter.Enabled   = estAdmin;
        _btnModifier.Enabled  = estAdmin;
        _btnSupprimer.Enabled = estAdmin;
    }

    private bool NomStructureDejaUtilise(string nom, int idStructureCourante = 0)
    {
        // On cherche si une autre structure porte déjà ce nom
        foreach (Structure s in _structures)
        {
            // On ignore la structure qu'on est en train de modifier
            if (s.IdStructure == idStructureCourante)
                continue;

            if (s.Nom.Trim().ToLower() == nom.Trim().ToLower())
                return true; // doublon trouvé
        }
        return false; // aucun doublon
    }

    /// <summary>Construit un objet Structure à partir de la ligne sélectionnée.</summary>
    private Structure? GetStructureSelectionnee()
    {
        if (_dgvStructures.SelectedRows.Count == 0)
            return null;

        DataGridViewRow row = _dgvStructures.SelectedRows[0];

        return new Structure
        {
            IdStructure   = Convert.ToInt32(row.Cells["IdStructure"].Value),
            Nom           = row.Cells["Nom"].Value?.ToString() ?? string.Empty,
            TypeStructure = row.Cells["TypeStructure"].Value?.ToString() ?? "LIGUE"
        };
    }

    // -------------------------------------------------------------------------
    // Boutons de la ToolStrip
    // -------------------------------------------------------------------------

    /// <summary>Ouvre le formulaire de détail en mode ajout.</summary>
    private void BtnAjouter_Click(object? sender, EventArgs e)
    {
        using FormStructureDetail dlg = new FormStructureDetail(null);

        if (dlg.ShowDialog() == DialogResult.OK && dlg.StructureResultat != null)
        {
            if (NomStructureDejaUtilise(dlg.StructureResultat.Nom))
            {
                MessageBox.Show("Une structure avec ce nom existe déjà.",
                    "Doublon", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _dao.Ajouter(dlg.StructureResultat);
                MessageBox.Show("Structure ajoutée avec succès.", "Succès",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ChargerStructures();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'ajout :\n" + ex.Message,
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    /// <summary>Ouvre le formulaire de détail en mode modification.</summary>
    private void BtnModifier_Click(object? sender, EventArgs e)
    {
        OuvrirModification();
    }

    /// <summary>Double-clic sur une ligne = ouvrir la modification.</summary>
    private void DgvStructures_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
            OuvrirModification();
    }

    /// <summary>Logique commune pour ouvrir la modification d'une structure.</summary>
    private void OuvrirModification()
    {
        Structure? structure = GetStructureSelectionnee();
        if (structure == null)
        {
            MessageBox.Show("Veuillez d'abord sélectionner une structure dans la liste.",
                "Aucune sélection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using FormStructureDetail dlg = new FormStructureDetail(structure);

        if (dlg.ShowDialog() == DialogResult.OK && dlg.StructureResultat != null)
        {
            if (NomStructureDejaUtilise(dlg.StructureResultat.Nom, dlg.StructureResultat.IdStructure))
            {
                MessageBox.Show("Une structure avec ce nom existe déjà.",
                    "Doublon", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _dao.Modifier(dlg.StructureResultat);
                MessageBox.Show("Structure modifiée avec succès.", "Succès",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ChargerStructures();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la modification :\n" + ex.Message,
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    /// <summary>Supprime la structure sélectionnée après confirmation.</summary>
    private void BtnSupprimer_Click(object? sender, EventArgs e)
    {
        Structure? structure = GetStructureSelectionnee();
        if (structure == null)
        {
            MessageBox.Show("Veuillez d'abord sélectionner une structure dans la liste.",
                "Aucune sélection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        DialogResult confirmation = MessageBox.Show(
            $"Confirmer la suppression de la structure « {structure.Nom} » ?",
            "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (confirmation != DialogResult.Yes)
            return;

        try
        {
            _dao.Supprimer(structure.IdStructure);
            MessageBox.Show("Structure supprimée.", "Succès",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            ChargerStructures();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Impossible de supprimer cette structure.\n" +
                "Elle est peut-être liée à des réservations existantes.\n\n" +
                "Détail : " + ex.Message,
                "Suppression impossible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    /// <summary>Recharge la liste des structures.</summary>
    private void BtnActualiser_Click(object? sender, EventArgs e)
    {
        ChargerStructures();
    }

    private void TxtRecherche_TextChanged(object? sender, EventArgs e)
    {
        AppliquerFiltre();
    }
}
