using M2L_Services.DataAccess;
using M2L_Services.Models;
using M2L_Services.Services;

namespace M2L_Services.Forms;

/// <summary>
/// Formulaire de détail d'une réservation (ajout ou modification).
/// Charge les salles et structures depuis la base de données.
/// Vérifie la disponibilité du créneau avant validation.
/// </summary>
public partial class FormReservationDetail : Form
{
    // Résultat de la saisie — récupéré par le formulaire appelant après DialogResult.OK
    public Reservation? ReservationResultat { get; private set; }

    // ID de la réservation en mode modification, 0 en mode ajout
    private readonly int _idReservation;

    // DAOs pour charger les listes de salles et structures
    private readonly SalleDAO     _salleDao     = new SalleDAO();
    private readonly StructureDAO _structureDao = new StructureDAO();

    // Service de vérification de disponibilité
    private readonly DisponibiliteService _disponibilite = new DisponibiliteService();

    /// <summary>
    /// Constructeur.
    /// Passer null pour un ajout, ou un objet Reservation existant pour une modification.
    /// </summary>
    public FormReservationDetail(Reservation? reservationExistante)
    {
        InitializeComponent();

        // Charger les listes déroulantes depuis la BDD
        ChargerComboSalles();
        ChargerComboStructures();
        ChargerCreneaux();

        if (reservationExistante != null)
        {
            // Mode modification : on pré-remplit les champs
            _idReservation = reservationExistante.IdReservation;
            _dtpDate.Value = reservationExistante.DateReservation;

            // Sélectionner la salle
            for (int i = 0; i < _cboSalle.Items.Count; i++)
            {
                if (_cboSalle.Items[i] is Salle s && s.IdSalle == reservationExistante.IdSalle)
                { _cboSalle.SelectedIndex = i; break; }
            }

            // Sélectionner la structure
            for (int i = 0; i < _cboStructure.Items.Count; i++)
            {
                if (_cboStructure.Items[i] is Structure st && st.IdStructure == reservationExistante.IdStructure)
                { _cboStructure.SelectedIndex = i; break; }
            }

            // Sélectionner les heures
            string debut = reservationExistante.HeureDebut.ToString(@"hh\:mm");
            int idxDebut = _cboHeureDebut.Items.IndexOf(debut);
            _cboHeureDebut.SelectedIndex = idxDebut >= 0 ? idxDebut : 0;

            string fin = reservationExistante.HeureFin.ToString(@"hh\:mm");
            int idxFin = _cboHeureFin.Items.IndexOf(fin);
            _cboHeureFin.SelectedIndex = idxFin >= 0 ? idxFin : 0;

            // Sélectionner le statut
            int idxStatut = _cboStatut.Items.IndexOf(reservationExistante.Statut);
            _cboStatut.SelectedIndex = idxStatut >= 0 ? idxStatut : 0;

            _txtCommentaire.Text = reservationExistante.Commentaire;

            Text = "Modifier une réservation";
        }
        else
        {
            _idReservation = 0;
            Text = "Ajouter une réservation";
        }
    }

    /// <summary>Charge les salles dans le ComboBox.</summary>
    private void ChargerComboSalles()
    {
        _cboSalle.Items.Clear();
        foreach (Salle s in _salleDao.GetTout())
            _cboSalle.Items.Add(s);

        if (_cboSalle.Items.Count > 0)
            _cboSalle.SelectedIndex = 0;
    }

    /// <summary>Charge les structures dans le ComboBox.</summary>
    private void ChargerComboStructures()
    {
        _cboStructure.Items.Clear();
        foreach (Structure s in _structureDao.GetTout())
            _cboStructure.Items.Add(s);

        if (_cboStructure.Items.Count > 0)
            _cboStructure.SelectedIndex = 0;
    }

    /// <summary>Remplit les ComboBox d'heures avec des créneaux de 30 minutes (06:00 → 21:00).</summary>
    private void ChargerCreneaux()
    {
        _cboHeureDebut.Items.Clear();
        _cboHeureFin.Items.Clear();

        for (int h = 6; h <= 21; h++)
        {
            foreach (int min in new[] { 0, 30 })
            {
                if (h == 21 && min == 30) break;
                string creneau = $"{h:D2}:{min:D2}";
                _cboHeureDebut.Items.Add(creneau);
                _cboHeureFin.Items.Add(creneau);
            }
        }

        // Valeurs par défaut : 08:00 → 09:00
        int idx08 = _cboHeureDebut.Items.IndexOf("08:00");
        _cboHeureDebut.SelectedIndex = idx08 >= 0 ? idx08 : 0;

        int idx09 = _cboHeureFin.Items.IndexOf("09:00");
        _cboHeureFin.SelectedIndex = idx09 >= 0 ? idx09 : 1;
    }

    /// <summary>
    /// Valide la saisie, vérifie la disponibilité et ferme la boîte de dialogue.
    /// </summary>
    private void BtnValider_Click(object? sender, EventArgs e)
    {
        // Vérification de la salle
        if (_cboSalle.SelectedItem is not Salle salle)
        {
            MessageBox.Show("Veuillez sélectionner une salle.",
                "Champ requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // Vérification de la structure
        if (_cboStructure.SelectedItem is not Structure structure)
        {
            MessageBox.Show("Veuillez sélectionner une structure.",
                "Champ requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // Vérification des heures
        if (_cboHeureDebut.SelectedItem is null || _cboHeureFin.SelectedItem is null)
        {
            MessageBox.Show("Veuillez sélectionner les heures de début et de fin.",
                "Champ requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        TimeSpan heureDebut = TimeSpan.Parse(_cboHeureDebut.SelectedItem.ToString()!);
        TimeSpan heureFin   = TimeSpan.Parse(_cboHeureFin.SelectedItem.ToString()!);

        if (heureFin <= heureDebut)
        {
            MessageBox.Show("L'heure de fin doit être après l'heure de début.",
                "Horaire invalide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string statut = _cboStatut.SelectedItem?.ToString() ?? "EN_ATTENTE";

        // Vérification de disponibilité uniquement pour les réservations CONFIRMEE
        if (statut == "CONFIRMEE")
        {
            bool disponible = _disponibilite.EstDisponible(
                salle.IdSalle, _dtpDate.Value.Date,
                heureDebut, heureFin, _idReservation);

            if (!disponible)
            {
                MessageBox.Show(
                    _disponibilite.GetMessageIndisponibilite(
                        _dtpDate.Value.Date, heureDebut, heureFin),
                    "Créneau indisponible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        // Construction de l'objet résultat
        ReservationResultat = new Reservation
        {
            IdReservation   = _idReservation,
            IdSalle         = salle.IdSalle,
            IdStructure     = structure.IdStructure,
            DateReservation = _dtpDate.Value.Date,
            HeureDebut      = heureDebut,
            HeureFin        = heureFin,
            Statut          = statut,
            Commentaire     = _txtCommentaire.Text.Trim()
        };

        DialogResult = DialogResult.OK;
        Close();
    }
}
