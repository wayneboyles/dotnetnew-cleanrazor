$BuildDir = $env:APPVEYOR_BUILD_FOLDER
$BuildNumber = $env:APPVEYOR_BUILD_VERSION
$ProjectFile = $BuildDir + "\CleanRazor.Templates.csproj";
$NugetFile = $BuildDir + "\Boyles.CleanAspNetCore.Template." + $BuildNumber + ".nupkg";

# Generate a NuGet package for publishing
Write-Host "Generating NuGet Package" -ForegroundColor Green
& dotnet pack $ProjectFile -c Release /p:PackageVersion=$BuildNumber -o $BuildDir

# Save generated artifacts
Write-Host "Saving Artifacts" -ForegroundColor Green
Push-AppveyorArtifact $NugetFile

# Publish package to NuGet
Write-Host "Publishing NuGet package" -ForegroundColor Green
& nuget push $NugetFile -ApiKey $env:NUGET_API_KEY -Source https://www.nuget.org/api/v2/package