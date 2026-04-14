using M2L_Services.Models;

namespace M2L_Services.Forms;

/// <summary>
/// Formulaire de détail d'une structure (ajout ou modification).
/// Ouvert en tant que boîte de dialogue modale.
/// Si l'utilisateur valide, la propriété StructureResultat contient la structure saisie.
/// </summary>
public partial class FormStructureDetail : Form
{
    // Résultat de la saisie — récupéré par le formulaire appelant après DialogResult.OK
    public Structure? StructureResultat { get; private set; }

    // ID de la structure en mode modification, 0 en mode ajout
    private readonly int _idStructure;

    /// <summary>
    /// Constructeur.
    /// Passer null pour un ajout, ou un objet Structure existant pour une modification.
    /// </summary>
    public FormStructureDetail(Structure? structureExistante)
    {
        InitializeComponent();

        if (structureExistante != null)
        {
            // Mode modification : on pré-remplit les champs
            _idStructure = structureExistante.IdStructure;
            _txtNom.Text = structureExistante.Nom;

            int index = _cboType.Items.IndexOf(structureExistante.TypeStructure);
            _cboType.SelectedIndex = index >= 0 ? index : 0;

            Text = "Modifier une structure";
        }
        else
        {
            _idStructure = 0;
            Text = "Ajouter une structure";
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
            MessageBox.Show("Veuillez saisir un nom pour la structure.",
                "Champ requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _txtNom.Focus();
            return;
        }

        // Construction de l'objet résultat
        StructureResultat = new Structure
        {
            IdStructure   = _idStructure,
            Nom           = _txtNom.Text.Trim(),
            TypeStructure = _cboType.SelectedItem?.ToString() ?? "LIGUE"
        };

        DialogResult = DialogResult.OK;
        Close();
    }
}
