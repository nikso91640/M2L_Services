using M2L_Services.DataAccess;
using M2L_Services.Models;
using M2L_Services.Utils;

namespace M2L_Services.Forms;

/// <summary>
/// Formulaire de gestion des salles.
/// Affiche la liste dans un DataGridView.
/// Les opérations d'ajout et de modification se font via une fenêtre de détail (FormSalleDetail).
/// Seul l'administrateur peut ajouter, modifier ou supprimer.
/// </summary>
public partial class FormSalles : Form
{
    private readonly SalleDAO _dao = new SalleDAO();
    private List<Salle> _salles = new List<Salle>();

    public FormSalles()
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
        ChargerSalles();
        AppliquerDroits();
    }

    /// <summary>Crée les colonnes du DataGridView (une seule fois au démarrage).</summary>
    private void ConfigurerGrille()
    {
        _dgvSalles.Columns.Clear();

        // Colonne ID cachée — nécessaire pour les opérations CRUD
        _dgvSalles.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name    = "IdSalle",
            Visible = false
        });

        _dgvSalles.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name       = "Nom",
            HeaderText = "Nom de la salle",
            FillWeight = 50
        });

        _dgvSalles.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name             = "Capacite",
            HeaderText       = "Capacité",
            FillWeight       = 20,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
        });

        _dgvSalles.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name       = "TypeSalle",
            HeaderText = "Type",
            FillWeight = 30
        });
    }

    /// <summary>Recharge la liste des salles depuis la base de données.</summary>
    private void ChargerSalles()
    {
        _dgvSalles.Rows.Clear();

        try
        {
            _salles = _dao.GetTout();
            AppliquerFiltre();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Erreur lors du chargement des salles :\n" + ex.Message,
                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void AppliquerFiltre()
    {
        // On récupère le texte saisi dans la barre de recherche (en minuscules pour comparer sans tenir compte de la casse)
        string filtre = _txtRecherche.Text.Trim().ToLower();

        _dgvSalles.Rows.Clear();

        // On parcourt toutes les salles chargées depuis la base
        foreach (Salle s in _salles)
        {
            // Si un filtre est saisi, on vérifie que le nom ou le type le contient
            if (filtre != "" && !s.Nom.ToLower().Contains(filtre) && !s.TypeSalle.ToLower().Contains(filtre))
                continue; // cette salle ne correspond pas → on passe à la suivante

            // La salle correspond aux critères : on l'ajoute à la grille
            _dgvSalles.Rows.Add(s.IdSalle, s.Nom, s.Capacite, s.TypeSalle);
        }
    }

    /// <summary>Active ou désactive les boutons de la ToolStrip selon le rôle.</summary>
    private void AppliquerDroits()
    {
        bool estAdmin = Session.EstAdmin;
        _btnAjouter.Enabled   = estAdmin;
        _btnModifier.Enabled  = estAdmin;
        _btnSupprimer.Enabled = estAdmin;
    }

    private bool NomSalleDejaUtilise(string nom, int idSalleCourante = 0)
    {
        // On cherche si une autre salle porte déjà ce nom
        foreach (Salle s in _salles)
        {
            // On ignore la salle qu'on est en train de modifier
            if (s.IdSalle == idSalleCourante)
                continue;

            if (s.Nom.Trim().ToLower() == nom.Trim().ToLower())
                return true; // doublon trouvé
        }
        return false; // aucun doublon
    }

    /// <summary>Construit un objet Salle à partir de la ligne sélectionnée dans la grille.</summary>
    private Salle? GetSalleSelectionnee()
    {
        if (_dgvSalles.SelectedRows.Count == 0)
            return null;

        DataGridViewRow row = _dgvSalles.SelectedRows[0];

        return new Salle
        {
            IdSalle   = Convert.ToInt32(row.Cells["IdSalle"].Value),
            Nom       = row.Cells["Nom"].Value?.ToString() ?? string.Empty,
            Capacite  = Convert.ToInt32(row.Cells["Capacite"].Value),
            TypeSalle = row.Cells["TypeSalle"].Value?.ToString() ?? "REUNION"
        };
    }

    // -------------------------------------------------------------------------
    // Boutons de la ToolStrip
    // -------------------------------------------------------------------------

    /// <summary>Ouvre le formulaire de détail en mode ajout.</summary>
    private void BtnAjouter_Click(object? sender, EventArgs e)
    {
        using FormSalleDetail dlg = new FormSalleDetail(null);

        if (dlg.ShowDialog() == DialogResult.OK && dlg.SalleResultat != null)
        {
            if (NomSalleDejaUtilise(dlg.SalleResultat.Nom))
            {
                MessageBox.Show("Une salle avec ce nom existe déjà.",
                    "Doublon", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _dao.Ajouter(dlg.SalleResultat);
                MessageBox.Show("Salle ajoutée avec succès.", "Succès",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ChargerSalles();
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
    private void DgvSalles_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
            OuvrirModification();
    }

    /// <summary>Logique commune pour ouvrir la modification d'une salle.</summary>
    private void OuvrirModification()
    {
        Salle? salle = GetSalleSelectionnee();
        if (salle == null)
        {
            MessageBox.Show("Veuillez d'abord sélectionner une salle dans la liste.",
                "Aucune sélection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using FormSalleDetail dlg = new FormSalleDetail(salle);

        if (dlg.ShowDialog() == DialogResult.OK && dlg.SalleResultat != null)
        {
            if (NomSalleDejaUtilise(dlg.SalleResultat.Nom, dlg.SalleResultat.IdSalle))
            {
                MessageBox.Show("Une salle avec ce nom existe déjà.",
                    "Doublon", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _dao.Modifier(dlg.SalleResultat);
                MessageBox.Show("Salle modifiée avec succès.", "Succès",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ChargerSalles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la modification :\n" + ex.Message,
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    /// <summary>Supprime la salle sélectionnée après confirmation.</summary>
    private void BtnSupprimer_Click(object? sender, EventArgs e)
    {
        Salle? salle = GetSalleSelectionnee();
        if (salle == null)
        {
            MessageBox.Show("Veuillez d'abord sélectionner une salle dans la liste.",
                "Aucune sélection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        DialogResult confirmation = MessageBox.Show(
            $"Confirmer la suppression de la salle « {salle.Nom} » ?",
            "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (confirmation != DialogResult.Yes)
            return;

        try
        {
            _dao.Supprimer(salle.IdSalle);
            MessageBox.Show("Salle supprimée.", "Succès",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            ChargerSalles();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Impossible de supprimer cette salle.\n" +
                "Elle est peut-être liée à des réservations existantes.\n\n" +
                "Détail : " + ex.Message,
                "Suppression impossible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    /// <summary>Recharge la liste des salles.</summary>
    private void BtnActualiser_Click(object? sender, EventArgs e)
    {
        ChargerSalles();
    }

    private void TxtRecherche_TextChanged(object? sender, EventArgs e)
    {
        AppliquerFiltre();
    }
}
