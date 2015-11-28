param([String] $userName, [String] $passwordFile, [String] $workingDir, [String] $buildShare, [String] $automationBuild)

If(!$passwordFile){
	$passwordFile = ""
}

$password = Get-Content $passwordFile | ConvertTo-SecureString

#TESTING VALUES

#TODO: Remove after pass generated in file
$buildShare = ""
$automationBuild = "Testing"

#END TESTING VALUES

 
$cred = (New-Object System.Management.Automation.PSCredential($userName, $password))
 
$drive = New-PSDrive -Name Temp -Root $buildShare -Credential $cred -PSProvider FileSystem
 
Write-Host "Starting File Copy, Mapping Drive $($drive.Name)"

Copy-Item "$($workingDir)\Automation.TestRunner\bin\Release\*" "$($drive.Name):\Automation\$($automationBuild)" -Recurse -Force
#xcopy "$($workingDir)\Automation.TestRunner\bin\Release\*" "Temp:\Automation\$($automationBuild)" /i /d /q /y /c /e 
 
Remove-PSDrive Temp
 
Write-Host "File Copy Complete"
