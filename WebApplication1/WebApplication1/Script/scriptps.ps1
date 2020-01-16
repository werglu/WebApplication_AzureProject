Connect-AzAccount
$StorageAccount = Get-AzStorageAccount -Name jobwebappstorage -ResourceGroupName new-resource-group

$Container = "applications"
Set-AzStorageBlobContent -File "Test.jpg" `
	-Container $Container `
	-Blob "Image001.jpg" `
	-Context $StorageAccount.Context

$containerName = "newcontainer1"
New-AzStorageContainer -Name $containerName -Context $StorageAccount.Context -Permission blob