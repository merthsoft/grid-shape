param(
    [string]$RimWorldDir = "${env:ProgramFiles(x86)}\Steam\steamapps\common\RimWorld"
)
$ErrorActionPreference = 'Stop'
foreach ($name in @('Assembly-CSharp.dll', 'UnityStubs.dll', 'Waylith.dll', 'Waylith.UnityCompat.dll')) {
    if (!(Test-Path (Join-Path $RimWorldDir $name))) { throw "Missing $name. Set -RimWorldDir to the installed RimWorld 1.7 root." }
}
dotnet build (Join-Path $PSScriptRoot 'Source\Merthsoft.DesignatorShapes.Grids.csproj') -c Release "-p:RimWorldDir=$RimWorldDir"
if ($LASTEXITCODE -ne 0) { throw 'Build failed.' }
