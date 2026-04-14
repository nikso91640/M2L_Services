Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing

$capturesDir = Join-Path $PSScriptRoot "."

$ecrans = @(
    "01_Login",
    "02_Login_Erreur",
    "03_Accueil_TableauDeBord",
    "04_Salles_Liste",
    "05_Salle_Detail",
    "06_Structures_Liste",
    "07_Structure_Detail",
    "08_Reservations_Liste",
    "09_Reservation_Detail",
    "10_Reservation_Creneau_Indisponible",
    "11_Digicode_Wifi"
)

function Save-FullScreenCapture {
    param(
        [Parameter(Mandatory = $true)]
        [string]$FilePath
    )

    $bounds = [System.Windows.Forms.Screen]::PrimaryScreen.Bounds
    $bitmap = New-Object System.Drawing.Bitmap($bounds.Width, $bounds.Height)
    $graphics = [System.Drawing.Graphics]::FromImage($bitmap)

    try {
        $graphics.CopyFromScreen($bounds.Location, [System.Drawing.Point]::Empty, $bounds.Size)
        $bitmap.Save($FilePath, [System.Drawing.Imaging.ImageFormat]::Png)
    }
    finally {
        $graphics.Dispose()
        $bitmap.Dispose()
    }
}

Write-Host "=" -ForegroundColor DarkGray
Write-Host "Capture des écrans M2L Services" -ForegroundColor Cyan
Write-Host "Dossier de sortie : $capturesDir" -ForegroundColor Gray
Write-Host "=" -ForegroundColor DarkGray
Write-Host ""
Write-Host "1) Lance l'application M2L Services" -ForegroundColor Yellow
Write-Host "2) Place l'écran demandé au premier plan" -ForegroundColor Yellow
Write-Host "3) Reviens ici puis appuie sur Entrée pour capturer" -ForegroundColor Yellow
Write-Host ""

for ($i = 0; $i -lt $ecrans.Count; $i++) {
    $nom = $ecrans[$i]
    $filePath = Join-Path $capturesDir ("$nom.png")

    Write-Host "Prépare l'écran : $nom" -ForegroundColor Green
    Read-Host "Appuie sur Entrée quand c'est prêt"

    Save-FullScreenCapture -FilePath $filePath
    Write-Host "Capture enregistrée : $filePath" -ForegroundColor Cyan
    Write-Host ""
}

Write-Host "Terminé. Toutes les captures sont dans : $capturesDir" -ForegroundColor Magenta
