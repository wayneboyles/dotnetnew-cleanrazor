function CleanBinAndObj() {
    [CmdletBinding()]
    param ()
    process {
        if (Test-Path -Path "$PSScriptRoot\bin") {
            Remove-Item -Path "$PSScriptRoot\bin" -Recurse -Force | Out-Null
        }

        if (Test-Path -Path "$PSScriptRoot\obj") {
            Remove-Item -Path "$PSScriptRoot\obj" -Recurse -Force | Out-Null
        }
    }
}

CleanBinAndObj

dotnet pack .\CleanRazor.Templates.csproj -o .

CleanBinAndObj