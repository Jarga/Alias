param([String] $username, [String] $password, [String] $server, [String] $targetBuildFolder, [String] $targetDropFolder, [String] $environment, [String] $browser, [String[]] $xunitArgs)

$securePassword = $NULL;

#START Testing Values
$username = ""

#Generate string with: read-host -assecurestring | convertfrom-securestring | out-file C:\securestring.txt
#Pull string out with: $password = "" | convertto-securestring
$securePassword = "" | convertto-securestring

$server = ""

$share = "\\svrrbidevts01\x$"
$targetBuildFolder = "\Automation\Testing\*"
$targetDropFolder = "C:\Automation\Testing\"

$environment = "dev"
$browser = "chrome"
$xunitArgs = @("Automation.MarketOnce.Web.dll", "-namespace", "Automation.MarketOnce.Web.Tests.Admin.Users", "-xml", "output.xml", "-html", "output.html")

#END Testing Values

IF ( $username -eq $NULL)
{
	$username = read-host "Please enter your username"
}

IF ($securePassword -eq $NULL -And $password -eq $NULL)
{
	$securePassword = read-host -assecurestring "Please enter your password"
} 
ELSEIF($securePassword -eq $NULL)
{
	$securePassword = ConvertTo-SecureString $password –asplaintext –force 
}

IF ( $server -eq $NULL)
{
	$server = read-host "Please enter the target machine"
}

$cred = new-object -typename System.Management.Automation.PSCredential -ArgumentList $username, $securePassword

$session = New-PSSession -ComputerName $server -Credential $cred

    # TODO: Cleanup Old Build Folders as well
Invoke-Command -Session $session -ScriptBlock {
		param([String]$share,[PSCredential]$cred, [String]$targetBuildFolder, [String]$targetDropFolder)
        New-PSDrive -Name Z -Root $share -Persist -Credential $cred -PSProvider FileSystem
		xcopy "Z:$($targetBuildFolder)" $targetDropFolder /i /d /q /y /c /e 
        Remove-PSDrive Z
	} -ArgumentList $share, $cred, $targetBuildFolder, $targetDropFolder
    

Invoke-Command -Session $session {[Environment]::SetEnvironmentVariable("TestAutomationEnvironment", $environment, "Process")}
Invoke-Command -Session $session {[Environment]::SetEnvironmentVariable("TestAutomationBrowser", $browser, "Process")}
Invoke-Command -Session $session -ScriptBlock { 
		param([String[]]$xunitArgs, [String]$targetDropFolder)
		return Start-Process -Argumentlist $xunitArgs -WorkingDirectory $targetDropFolder .\Automation.TestRunner.exe -Wait
	} -ArgumentList $xunitArgs, $targetDropFolder
        

Remove-PSSession $session

Write-Host $return;
