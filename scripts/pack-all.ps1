param(
    [string]$Configuration = "Release",
    [string]$OutputDirectory,
    [string]$Version,
    [switch]$Clean
)

$ErrorActionPreference = "Stop"

$repoRoot = Split-Path -Parent $PSScriptRoot
$projectDir = Join-Path $repoRoot "Avalonia.SpellChecker"
$project = Join-Path $projectDir "Avalonia.SpellChecker.csproj"

if ([string]::IsNullOrWhiteSpace($OutputDirectory)) {
    $OutputDirectory = Join-Path $repoRoot "artifacts\nuget"
}

if ($Clean -and (Test-Path -LiteralPath $OutputDirectory)) {
    Remove-Item -LiteralPath $OutputDirectory -Recurse -Force
}

New-Item -ItemType Directory -Path $OutputDirectory -Force | Out-Null

$commonArgs = @(
    "pack",
    $project,
    "--configuration", $Configuration,
    "--output", $OutputDirectory,
    "/p:ContinuousIntegrationBuild=true",
    "/p:RestoreForceEvaluate=true",
    "/p:IncludeSymbols=true",
    "/p:SymbolPackageFormat=snupkg"
)

if (-not [string]::IsNullOrWhiteSpace($Version)) {
    $commonArgs += "/p:Version=$Version"
}

foreach ($avaloniaVersion in @("12", "11")) {
    Write-Host "Packing Avalonia $avaloniaVersion package..." -ForegroundColor Cyan
    & dotnet @commonArgs "/p:AvaloniaVersion=$avaloniaVersion"
}

Write-Host "Packages written to: $OutputDirectory" -ForegroundColor Green
