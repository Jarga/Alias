Param([String] $workingDir, [String] $xunitArgs, [String] $environment, [String] $browser, [String] $baseTestUrl)

Try
{	
	If(-not(Test-Path "$($workingDir)\tests\results")){
		New-Item -path "$($workingDir)\tests\results" -type directory
	}
		
	#set environment variables
	[Environment]::SetEnvironmentVariable("TestAutomationEnvironment", $environment, "Process")
	[Environment]::SetEnvironmentVariable("TestAutomationBrowser", $browser, "Process")
	[Environment]::SetEnvironmentVariable("TestAutomationUrl", $baseTestUrl, "Process")
	
	$xunitArgsAsArray = $xunitArgs.split(",") | Where-Object {$_ -ne " "} | % { $_.Trim() }
	$xunitArgsAsArray += @("-html", "$($workingDir)\tests\results\output.html")

	Write-Host "Xunit Args: $xunitArgsAsArray"
	
	#Use new process so that we can echo the stdout back up to a calling process
	$pinfo = New-Object System.Diagnostics.ProcessStartInfo
	$pinfo.FileName = "$($workingDir)\Automation.TestRunner.exe"
	$pinfo.RedirectStandardError = $true
	$pinfo.RedirectStandardOutput = $true
	$pinfo.UseShellExecute = $false
	$pinfo.Arguments = $xunitArgsAsArray
	$p = New-Object System.Diagnostics.Process
	$p.StartInfo = $pinfo
	$p.Start() | Out-Null
	$p.WaitForExit()
	$stdout = $p.StandardOutput.ReadToEnd()
	$stderr = $p.StandardError.ReadToEnd()
	Write-Host "stdout: $stdout"
	If(-not([string]::IsNullOrEmpty($stderr))){
		Write-Error "stderr: $stderr"
	}
	Write-Host "exit code: " + $p.ExitCode
} 
Catch 
{
	$ErrorMessage = $_.Exception.Message
	Write-Error $ErrorMessage
	exit(1)
}
