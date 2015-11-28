Param([String] $username, [String] $securePassword, [String] $remoteMachine, [String] $resultsShare, [String] $targetTestDll, [String] $environment, [String] $browser, [String] $outputFileName, [String] $xunitFilters)

Function Execute_Local_Automation 
{
	Param([PSCredential] $cred, [String] $resultsShare, [String] $targetTestDll,
			[String] $outputFileName, [String] $environment, [String] $browser, [String] $xunitFilters)
	
	$installDir = "C:\Automation"

	$webclient = new-object System.Net.WebClient
	$webclient.Credentials = $cred.GetNetworkCredential()
	$buildXml = [xml]$webclient.DownloadString("")
	$buildNumber = $buildXml.build.number

	
	#If nuget does not exist go get it
	If(-not(Test-Path "$($installDir)\nuget.exe")){
		$webclient = New-Object System.Net.WebClient
		$webclient.DownloadFile("https://www.nuget.org/nuget.exe", "$($installDir)\nuget.exe")
	}

	#If config not exists write empty config (Allows for custom configs per machine just in case)
	If(-not(Test-Path "$($installDir)\NuGet.Config")){
		New-Item -Path "$($installDir)\NuGet.Config" -ItemType file -Value "<?xml version=`"1.0`" encoding=`"utf-8`"?>`r`n<configuration />" -force
	}

	#Download latest
	$pinfo = New-Object System.Diagnostics.ProcessStartInfo
	$pinfo.FileName = "$($installDir)\nuget.exe"
	$pinfo.RedirectStandardError = $true
	$pinfo.RedirectStandardOutput = $true
	$pinfo.UseShellExecute = $false
	$pinfo.Arguments = @("install", "Automation.TestRunner", "-Prerelease", "-Version", $buildNumber, "-Output", "$($installDir)", "-ConfigFile", "$($installDir)\NuGet.Config", "-Source", "https://api.nuget.org/v3/index.json", "-Source", "http://ostm-build:80/guestAuth/app/nuget/v1/FeedService.svc")
	$p = New-Object System.Diagnostics.Process
	$p.StartInfo = $pinfo
	$p.Start() | Out-Null
	$p.WaitForExit()
	$stdout = $p.StandardOutput.ReadToEnd()
	$stderr = $p.StandardError.ReadToEnd()
	Write-Host "stdout: $stdout"
	Write-Host "stderr: $stderr"
	Write-Host "exit code: " + $p.ExitCode

	$outputDirectory = "$($installDir)\Automation.TestRunner.$($buildNumber)"
	Try
	{
		#create output directory
		New-Item -ItemType Directory -Force -Path "$($outputDirectory)\results"
		
		#set environment variables
		[Environment]::SetEnvironmentVariable("TestAutomationEnvironment", $environment, "Process")
		[Environment]::SetEnvironmentVariable("TestAutomationBrowser", $browser, "Process")

		#Start automation tests
		$xunitArgsAsArray = @("$($outputDirectory)\$($targetTestDll)", "-xml", "$($outputDirectory)\results\$($outputFileName).xml", "-html", "$($outputDirectory)\results\$($outputFileName).html", "-teamcity", "-parallel", "none")
		$xunitArgsAsArray += $xunitFilters.split(",") | Where-Object {$_ -ne " "} | % { $_.Trim() }

		Write-Host "Xunit Args: $xunitArgsAsArray"

		#Start-Process -Argumentlist $xunitArgs "C:\Automation\$($automationBuild)\Automation.TestRunner.exe"  -Wait

		#Use new process so that we can echo the stdout back up to a calling process
		$pinfo = New-Object System.Diagnostics.ProcessStartInfo
		$pinfo.FileName = "$($outputDirectory)\Automation.TestRunner.exe"
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

				#Copy results back to share
		Copy-Item "$($outputDirectory)\results\*" "$($resultsShare)\Automation\$($buildNumber)\results" -Recurse -Force
		#xcopy "C:\Automation\$($automationBuild)\results\*" "$($drive.Name):\Automation\$($automationBuild)\results" /i /d /q /y /c /e 
	} 
	Catch 
	{
		$ErrorMessage = $_.Exception.Message
		Write-Error $ErrorMessage
		exit(1)
	}
}

$secpasswd = $securePassword | ConvertTo-SecureString
$cred = New-Object System.Management.Automation.PSCredential ($username, $secpasswd)

$session = New-PSSession -ComputerName $remoteMachine -Credential $cred

Invoke-Command -Session $session -ScriptBlock ${function:Execute_Local_Automation} -ArgumentList $cred, "$($resultsShare)", "$($targetTestDll)", "$($outputFileName)", "$($environment)", "$($browser)", "$($xunitFilters)"

Remove-PSSession $session
