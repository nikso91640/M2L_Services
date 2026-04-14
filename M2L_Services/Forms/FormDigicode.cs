using System.Windows.Forms;
using M2L_Services.DataAccess;
using M2L_Services.Models;
using M2L_Services.Utils;

namespace M2L_Services.Forms;

/// <summary>
/// Formulaire d'affichage et de saisie du digicode et de la clé Wifi du jour.
/// L'administrateur peut modifier les valeurs du jour.
/// Tout utilisateur peut consulter l'historique.
/// </summary>
public partial class FormDigicode : Form
{
    private readonly InfoJourDAO _dao = new InfoJourDAO();

    public FormDigicode()
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
        ChargerInfoDuJour();
        ChargerHistorique();
        AppliquerDroits();
    }

    /// <summary>Crée les colonnes du DataGridView de l'historique.</summary>
    private void ConfigurerGrille()
    {
        _dgvHistorique.Columns.Clear();

        _dgvHistorique.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name       = "Date",
            HeaderText = "Date",
            FillWeight = 25,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
        });
        _dgvHistorique.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name       = "Digicode",
            HeaderText = "Digicode",
            FillWeight = 30,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
        });
        _dgvHistorique.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name       = "CleWifi",
            HeaderText = "Clé Wifi",
            FillWeight = 45
        });
    }

    /// <summary>Charge le digicode et la clé Wifi du jour dans les champs de saisie.</summary>
    private void ChargerInfoDuJour()
    {
        try
        {
            InfoJour? info = _dao.GetAujourdhui();

            if (info != null)
            {
                _txtDigicode.Text = info.Digicode;
                _txtWifi.Text     = info.CleWifi;
            }
            else
            {
                // Aucune info saisie aujourd'hui — champs vides
                _txtDigicode.Text = string.Empty;
                _txtWifi.Text     = string.Empty;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Erreur lors du chargement de l'info du jour :\n" + ex.Message,
                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>Recharge l'historique des infos passées dans la grille.</summary>
    private void ChargerHistorique()
    {
        _dgvHistorique.Rows.Clear();

        try
        {
            foreach (InfoJour info in _dao.GetHistorique())
            {
                _dgvHistorique.Rows.Add(
                    info.DateInfo.ToString("dd/MM/yyyy"),
                    info.Digicode,
                    info.CleWifi
                );
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Erreur lors du chargement de l'historique :\n" + ex.Message,
                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>Désactive le bouton Enregistrer si l'utilisateur n'est pas admin.</summary>
    private void AppliquerDroits()
    {
        _btnEnregistrer.Enabled = Session.EstAdmin;
    }

    // -------------------------------------------------------------------------
    // Bouton
    // -------------------------------------------------------------------------

    /// <summary>Enregistre ou met à jour le digicode et la clé Wifi du jour.</summary>
    private void BtnEnregistrer_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_txtDigicode.Text))
        {
            MessageBox.Show("Veuillez saisir un digicode.",
                "Champ requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _txtDigicode.Focus();
            return;
        }
        if (string.IsNullOrWhiteSpace(_txtWifi.Text))
        {
            MessageBox.Show("Veuillez saisir la clé Wifi.",
                "Champ requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _txtWifi.Focus();
            return;
        }

        string digicode = _txtDigicode.Text.Trim();
        string cleWifi = _txtWifi.Text.Trim();

        if (digicode.Length < 4 || digicode.Length > 20)
        {
            MessageBox.Show("Le digicode doit contenir entre 4 et 20 caractères.",
                "Format invalide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _txtDigicode.Focus();
            return;
        }

        if (cleWifi.Length < 6)
        {
            MessageBox.Show("La clé Wifi doit contenir au moins 6 caractères.",
                "Format invalide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _txtWifi.Focus();
            return;
        }

        InfoJour info = new InfoJour
        {
            DateInfo = DateTime.Today,
            Digicode = digicode,
            CleWifi  = cleWifi
        };

        try
        {
            _dao.EnregistrerOuModifier(info);
            MessageBox.Show("Informations du jour enregistrées.", "Succès",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Recharger l'historique pour refléter la mise à jour
            ChargerHistorique();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Erreur lors de l'enregistrement :\n" + ex.Message,
                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnCopierDigicode_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_txtDigicode.Text))
            return;

        Clipboard.SetText(_txtDigicode.Text.Trim());
        MessageBox.Show("Digicode copié dans le presse-papiers.",
            "Copie", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void BtnCopierWifi_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_txtWifi.Text))
            return;

        Clipboard.SetText(_txtWifi.Text.Trim());
        MessageBox.Show("Clé Wifi copiée dans le presse-papiers.",
            "Copie", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
