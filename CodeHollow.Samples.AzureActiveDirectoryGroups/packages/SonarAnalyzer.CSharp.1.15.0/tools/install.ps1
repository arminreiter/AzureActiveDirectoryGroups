﻿param($installPath, $toolsPath, $package, $project)

if ($project.DTE.Version -ne '14.0')
{
    throw 'This package can only be installed on Visual Studio 2015.'
}

if ($project.Object.AnalyzerReferences -eq $null)
{
    throw 'This package cannot be installed without an analyzer reference.'
}

if($project.Type -ne "C#")
{
    throw 'This package can only be installed on C# projects.'
}

$analyzersPath = split-path -path $toolsPath -parent
$analyzersPath = join-path $analyzersPath "analyzers"

$analyzerFilePath = join-path $analyzersPath "SonarAnalyzer.dll"
$project.Object.AnalyzerReferences.Add($analyzerFilePath)

$analyzerFilePath = join-path $analyzersPath "SonarAnalyzer.CSharp.dll"
$project.Object.AnalyzerReferences.Add($analyzerFilePath)
