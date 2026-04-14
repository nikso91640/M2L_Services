using M2L_Services.Models;

namespace M2L_Services.Forms;

/// <summary>
/// Formulaire de détail d'une salle (ajout ou modification).
/// Ouvert en tant que boîte de dialogue modale.
/// Si l'utilisateur valide, la propriété SalleResultat contient la salle saisie.
/// </summary>
public partial class FormSalleDetail : Form
{
    // Résultat de la saisie — récupéré par le formulaire appelant après DialogResult.OK
    public Salle? SalleResultat { get; private set; }

    // ID de la salle en mode modification, 0 en mode ajout
    private readonly int _idSalle;

    /// <summary>
    /// Constructeur.
    /// Passer null pour un ajout, ou un objet Salle existant pour une modification.
    /// </summary>
    public FormSalleDetail(Salle? salleExistante)
    {
        InitializeComponent();

        if (salleExistante != null)
        {
            // Mode modification : on pré-remplit les champs
            _idSalle            = salleExistante.IdSalle;
            _txtNom.Text        = salleExistante.Nom;
            _nudCapacite.Value  = salleExistante.Capacite;

            int index = _cboType.Items.IndexOf(salleExistante.TypeSalle);
            _cboType.SelectedIndex = index >= 0 ? index : 0;

            Text = "Modifier une salle";
        }
        else
        {
            _idSalle = 0;
            Text = "Ajouter une salle";
        }
    }

    /// <summary>
    /// Valide la saisie et ferme la boîte de dialogue si tout est correct.
    /// </summary>
    private void BtnValider_Click(object? sender, EventArgs e)
    {
        // Validation du nom
        if (string.IsNullOrWhiteSpace(_txtNom.Text))
        {
            MessageBox.Show("Veuillez saisir un nom pour la salle.",
                "Champ requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _txtNom.Focus();
            return;
        }

        // Construction de l'objet résultat
        SalleResultat = new Salle
        {
            IdSalle   = _idSalle,
            Nom       = _txtNom.Text.Trim(),
            Capacite  = (int)_nudCapacite.Value,
            TypeSalle = _cboType.SelectedItem?.ToString() ?? "REUNION"
        };

        DialogResult = DialogResult.OK;
        Close();
    }
}
