using System.Windows.Forms;
using M2L_Services.DataAccess;
using M2L_Services.Models;
using M2L_Services.Utils;

namespace M2L_Services.Forms;

/// <summary>
/// Formulaire de gestion des réservations de salles.
/// Affiche la liste dans un DataGridView.
/// Les opérations d'ajout et de modification se font via FormReservationDetail.
/// L'annulation et la suppression se font directement depuis la ToolStrip.
/// Seul l'administrateur peut effectuer les opérations CRUD.
/// </summary>
public partial class FormReservations : Form
{
    private readonly ReservationDAO _dao = new ReservationDAO();
    private List<Reservation> _reservations = new List<Reservation>();

    public FormReservations()
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
        ChargerReservations();
        AppliquerDroits();
    }

    /// <summary>Crée les colonnes du DataGridView.</summary>
    private void ConfigurerGrille()
    {
        _dgvReservations.Columns.Clear();

        // Colonnes cachées — nécessaires pour retrouver les IDs
        _dgvReservations.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdReservation", Visible = false });
        _dgvReservations.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdSalle",       Visible = false });
        _dgvReservations.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdStructure",   Visible = false });

        _dgvReservations.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name       = "Date",
            HeaderText = "Date",
            FillWeight = 12,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
        });
        _dgvReservations.Columns.Add(new DataGridViewTextBoxColumn { Name = "Salle",     HeaderText = "Salle",     FillWeight = 18 });
        _dgvReservations.Columns.Add(new DataGridViewTextBoxColumn { Name = "Structure", HeaderText = "Structure", FillWeight = 20 });
        _dgvReservations.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name       = "HeureDebut",
            HeaderText = "Début",
            FillWeight = 9,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
        });
        _dgvReservations.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name       = "HeureFin",
            HeaderText = "Fin",
            FillWeight = 9,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
        });
        _dgvReservations.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name       = "Statut",
            HeaderText = "Statut",
            FillWeight = 13,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
        });
        _dgvReservations.Columns.Add(new DataGridViewTextBoxColumn { Name = "Commentaire", HeaderText = "Commentaire", FillWeight = 19 });
    }

    /// <summary>Recharge la grille depuis la base de données.</summary>
    private void ChargerReservations()
    {
        _dgvReservations.Rows.Clear();

        try
        {
            _reservations = _dao.GetTout();
            AppliquerFiltre();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Erreur lors du chargement des réservations :\n" + ex.Message,
                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void AppliquerFiltre()
    {
        // Récupérer le statut sélectionné dans la ComboBox (par défaut "TOUS")
        string statutFiltre = "TOUS";
        if (_cboFiltreStatut.SelectedItem != null)
            statutFiltre = _cboFiltreStatut.SelectedItem.ToString();

        // Récupérer le texte de recherche (en minuscules pour comparer sans tenir compte de la casse)
        string recherche = _txtRecherche.Text.Trim().ToLower();

        _dgvReservations.Rows.Clear();

        // On parcourt toutes les réservations chargées depuis la base
        foreach (Reservation r in _reservations)
        {
            // Filtre par statut : on passe si le statut ne correspond pas
            if (statutFiltre != "TOUS" && r.Statut != statutFiltre)
                continue;

            // Filtre par texte : on passe si ni la salle ni la structure ne contient le texte recherché
            if (recherche != "" && !r.NomSalle.ToLower().Contains(recherche) && !r.NomStructure.ToLower().Contains(recherche))
                continue;

            // La réservation passe les filtres : on l'ajoute à la grille
            int index = _dgvReservations.Rows.Add(
                r.IdReservation,
                r.IdSalle,
                r.IdStructure,
                r.DateReservation.ToString("dd/MM/yyyy"),
                r.NomSalle,
                r.NomStructure,
                r.HeureDebut.ToString(@"hh\:mm"),
                r.HeureFin.ToString(@"hh\:mm"),
                r.Statut,
                r.Commentaire
            );

            ColorerLigneSelonStatut(_dgvReservations.Rows[index], r.Statut);
        }
    }

    private static void ColorerLigneSelonStatut(DataGridViewRow row, string statut)
    {
        if (statut == "CONFIRMEE")
        {
            row.Cells["Statut"].Style.ForeColor = Color.ForestGreen;
            row.Cells["Statut"].Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        }
        else if (statut == "ANNULEE")
        {
            row.Cells["Statut"].Style.ForeColor = Color.DarkRed;
            row.Cells["Statut"].Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        }
        else
        {
            row.Cells["Statut"].Style.ForeColor = Color.DarkOrange;
            row.Cells["Statut"].Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        }
    }

    /// <summary>Active ou désactive les boutons selon le rôle.</summary>
    private void AppliquerDroits()
    {
        bool estAdmin = Session.EstAdmin;
        _btnAjouter.Enabled    = estAdmin;
        _btnModifier.Enabled   = estAdmin;
        _btnAnnulerRes.Enabled = estAdmin;
        _btnSupprimer.Enabled  = estAdmin;
    }

    /// <summary>Construit un objet Reservation à partir de la ligne sélectionnée.</summary>
    private Reservation? GetReservationSelectionnee()
    {
        if (_dgvReservations.SelectedRows.Count == 0)
            return null;

        DataGridViewRow row = _dgvReservations.SelectedRows[0];

        DateTime.TryParseExact(
            row.Cells["Date"].Value?.ToString(), "dd/MM/yyyy",
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None, out DateTime date);

        return new Reservation
        {
            IdReservation   = Convert.ToInt32(row.Cells["IdReservation"].Value),
            IdSalle         = Convert.ToInt32(row.Cells["IdSalle"].Value),
            IdStructure     = Convert.ToInt32(row.Cells["IdStructure"].Value),
            DateReservation = date,
            HeureDebut      = TimeSpan.Parse(row.Cells["HeureDebut"].Value?.ToString() ?? "08:00"),
            HeureFin        = TimeSpan.Parse(row.Cells["HeureFin"].Value?.ToString() ?? "09:00"),
            Statut          = row.Cells["Statut"].Value?.ToString() ?? "EN_ATTENTE",
            Commentaire     = row.Cells["Commentaire"].Value?.ToString() ?? string.Empty,
            NomSalle        = row.Cells["Salle"].Value?.ToString() ?? string.Empty,
            NomStructure    = row.Cells["Structure"].Value?.ToString() ?? string.Empty
        };
    }

    // -------------------------------------------------------------------------
    // Boutons de la ToolStrip
    // -------------------------------------------------------------------------

    /// <summary>Ouvre le formulaire de détail en mode ajout.</summary>
    private void BtnAjouter_Click(object? sender, EventArgs e)
    {
        using FormReservationDetail dlg = new FormReservationDetail(null);

        if (dlg.ShowDialog() == DialogResult.OK && dlg.ReservationResultat != null)
        {
            try
            {
                _dao.Ajouter(dlg.ReservationResultat);
                MessageBox.Show("Réservation ajoutée avec succès.", "Succès",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ChargerReservations();
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
    private void DgvReservations_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
            OuvrirModification();
    }

    /// <summary>Logique commune pour ouvrir la modification d'une réservation.</summary>
    private void OuvrirModification()
    {
        Reservation? reservation = GetReservationSelectionnee();
        if (reservation == null)
        {
            MessageBox.Show("Veuillez d'abord sélectionner une réservation dans la liste.",
                "Aucune sélection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using FormReservationDetail dlg = new FormReservationDetail(reservation);

        if (dlg.ShowDialog() == DialogResult.OK && dlg.ReservationResultat != null)
        {
            try
            {
                _dao.Modifier(dlg.ReservationResultat);
                MessageBox.Show("Réservation modifiée avec succès.", "Succès",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ChargerReservations();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la modification :\n" + ex.Message,
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    /// <summary>Annule la réservation sélectionnée (statut → ANNULEE).</summary>
    private void BtnAnnulerRes_Click(object? sender, EventArgs e)
    {
        Reservation? reservation = GetReservationSelectionnee();
        if (reservation == null)
        {
            MessageBox.Show("Veuillez sélectionner une réservation à annuler.",
                "Aucune sélection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        DialogResult confirmation = MessageBox.Show(
            "Confirmer l'annulation de cette réservation ?",
            "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (confirmation != DialogResult.Yes)
            return;

        try
        {
            _dao.Annuler(reservation.IdReservation);
            MessageBox.Show("Réservation annulée.", "Succès",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            ChargerReservations();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Erreur lors de l'annulation :\n" + ex.Message,
                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>Supprime définitivement la réservation sélectionnée.</summary>
    private void BtnSupprimer_Click(object? sender, EventArgs e)
    {
        Reservation? reservation = GetReservationSelectionnee();
        if (reservation == null)
        {
            MessageBox.Show("Veuillez sélectionner une réservation à supprimer.",
                "Aucune sélection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        DialogResult confirmation = MessageBox.Show(
            "Confirmer la suppression définitive de cette réservation ?",
            "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (confirmation != DialogResult.Yes)
            return;

        try
        {
            _dao.Supprimer(reservation.IdReservation);
            MessageBox.Show("Réservation supprimée.", "Succès",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            ChargerReservations();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Erreur lors de la suppression :\n" + ex.Message,
                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>Recharge la liste des réservations.</summary>
    private void BtnActualiser_Click(object? sender, EventArgs e)
    {
        ChargerReservations();
    }

    private void CboFiltreStatut_SelectedIndexChanged(object? sender, EventArgs e)
    {
        AppliquerFiltre();
    }

    private void TxtRecherche_TextChanged(object? sender, EventArgs e)
    {
        AppliquerFiltre();
    }
}
