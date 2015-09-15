$username = ""

#Generate With: read-host -assecurestring | convertfrom-securestring | out-file C:\securestring.txt
$password = "" | convertto-securestring
$cred = new-object -typename System.Management.Automation.PSCredential -argumentlist $username, $password

$serverNameOrIp = ""

$session = New-PSSession -ComputerName $serverNameOrIp -Credential $cred

# TODO: Cleanup Old Build Folders as well
Invoke-Command -Session $session -ScriptBlock { 
		New-PSDrive -Name DropDrive -Credential $cred -Root \\SERVER\Share
		xcopy DropDrive:\Automation\$BUILD\* "C:\Automation\$BUILD\" /i /d /q /y /c
	}

Invoke-Command -Session $session {[Environment]::SetEnvironmentVariable("TestAutomationEnvironment", "dev", "Process")}
Invoke-Command -Session $session {[Environment]::SetEnvironmentVariable("TestAutomationBrowser", "chrome", "Process")}
Invoke-Command -Session $session -ScriptBlock { 
		$argumentlist = @("TestAutomation.dll", "-namespace", "TestAutomation.FunctionalTests.MarketOnce.Admin.Users", "-xml", "output.xml", "-html", "output.html")
		Start-Process -Argumentlist $argumentlist -WorkingDirectory "C:\GIT\Selenium-CodedUI-Automation-Framework\TestAutomationRunner\bin\Debug" .\TestAutomationRunner.exe 
	}

# Remove-PSSession $session