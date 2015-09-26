param([String] $userName, [String] $passwordFile, [String[][]] $machinesAndFilters, [String] $resultsShare, 
	  [String] $automationBuild, [String] $targetTestDll, [String] $environment, [String] $browser)
	  
If(!$passwordFile){
	$passwordFile = ""
}

$passString = Get-Content $passwordFile
$password = $passString | ConvertTo-SecureString

#TESTING VALUES

#TODO: Remove after pass generated in file

$machine1 = @("", "-Trait, Suite=Smoke")
#$machine2 = @("", "'-Trait', 'Suite=Smoke'")

$machinesAndFilters = New-Object String[] 1
$machinesAndFilters[0] = $machine1

$resultsShare = ""
$targetTestDll = "Automation.MarketOnce.Web.dll"
$environment = "dev"
$browser = "chrome"

#END TESTING VALUES


Write-Host "Starting Test Distribution, args: $machinesAndFilters"

$executionCount = 0;

$procs = New-Object System.Diagnostics.Process[] $machinesAndFilters.Length;
Foreach($machineAndFilters in $machinesAndFilters)
{
	$argList = @($userName, $passString, $machineAndFilters[0], $resultsShare, $targetTestDll, $environment, $browser, "output-$($executionCount)", $machineAndFilters[1])
	
	#turn args into quoted literals to prevent issues with passing to xunit
	$argList = $argList | % { "`"$($_)`"" }

	$rootArgs = @("-ExecutionPolicy", "ByPass", "-NonInteractive", "-File", "Automation.TestRunner\start-automation-remote.ps1")
	$rootArgs += $argList

	Write-Host "Executing start-automation-remote.ps1 with args: $rootArgs"
	
	Try{
		$pinfo = New-Object System.Diagnostics.ProcessStartInfo
		$pinfo.FileName = "powershell.exe"
		$pinfo.RedirectStandardError = $true
		$pinfo.RedirectStandardOutput = $true
		$pinfo.UseShellExecute = $false
		$pinfo.Arguments = $rootArgs
		$p = New-Object System.Diagnostics.Process
		$p.StartInfo = $pinfo
		$p.Start() | Out-Null

		$procs[$executionCount] = $p
		$executionCount++;
	}
	Catch 
	{
		$ErrorMessage = $_.Exception.Message
		Write-Error $ErrorMessage
		exit(1)
	} 
}


While($procs.Length -gt 0){
	$remainingProcs = New-Object System.Diagnostics.Process[] 0

	#wait 30 seconds for process to finish (redirect Wait-Process output so we don't see it)
	$procs | Wait-Process -Timeout 30 2>&1 >> $null

	Foreach($proc in $procs)
	{
		if($proc.HasExited)
		{
			Write-Host "Process Finished:"
			$stdout = $proc.StandardOutput.ReadToEnd()
			$stderr = $proc.StandardError.ReadToEnd()
			Write-Host "stdout: $stdout"
			If(-not([string]::IsNullOrEmpty($stderr))){
				Write-Error "stderr: $stderr"
			}
			Write-Host "exit code: " + $proc.ExitCode
		}
		Else 
		{
			$remainingProcs += @($proc)
		}
	}
	$procs = $remainingProcs;
}

Write-Host "Test Distribution Complete"
