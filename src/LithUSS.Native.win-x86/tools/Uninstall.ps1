param($installPath, $toolsPath, $package, $project)

if ($project.Type -eq "Web Site") {
	$projectDirectoryPath = $project.Properties.Item("FullPath").Value
	$binDirectoryPath = Join-Path $projectDirectoryPath "bin"
	$assemblyFileName = "LithUSS.dll"

	$assemblyDirectoryPath = Join-Path $binDirectoryPath "x86"
	$assemblyFilePath = Join-Path $assemblyDirectoryPath $assemblyFileName

	if (Test-Path $assemblyFilePath) {
		Remove-Item $assemblyFilePath -Force
	}
}