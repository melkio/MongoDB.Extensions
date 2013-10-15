$current_directory = Resolve-Path .
$psake_directory = Join-Path $current_directory -ChildPath "tools\psake"
$psake_module = Join-Path $psake_directory -ChildPath "psake.psm1"

Import-Module $psake_module
Invoke-psake default.ps1 build