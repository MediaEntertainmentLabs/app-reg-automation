param keyVaultName string
param location string = resourceGroup().location
param tenantId string = subscription().tenantId
param logAnalyticsId string
param tags object = {}

resource keyVault 'Microsoft.KeyVault/vaults@2021-04-01-preview'={
  name: keyVaultName
  location: location
  properties:{
    enabledForDeployment: false
    enabledForTemplateDeployment: false
    enabledForDiskEncryption: false
    enableSoftDelete: false
    softDeleteRetentionInDays: 90
    enableRbacAuthorization: true
    networkAcls:{
      defaultAction: 'Deny'
      bypass: 'AzureServices'
      ipRules: []
      virtualNetworkRules:[]
    }
    sku:{
      family: 'A'
      name: 'standard'
    }
    tenantId: tenantId
  }
  tags: tags
}


resource keyVaultDiagnosticSettings 'microsoft.insights/diagnosticSettings@2017-05-01-preview' ={
  name: 'Send_To_LogAnalytics'
  scope: keyVault  
  properties:{
    workspaceId: logAnalyticsId
    logs: [
      {
        enabled: true
        category: 'AuditEvent'
      }
    ]
    metrics:[
      {
        category: 'AllMetrics'
        enabled: true
      }
    ]
  }
}
