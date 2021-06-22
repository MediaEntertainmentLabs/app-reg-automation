param location string = resourceGroup().location
param resourcenamePrefix string = uniqueString(resourceGroup().id)

param functionsAuthClientId string
@secure()
param functionsAuthClientSecret string
param functionsAuthAllowedAudiances array
param functionsAuthIssuerURL string

var defaultTags={}

var logAnalyticsName = take('log-${resourcenamePrefix}', 63)
var appInsightsName = take('appi-${resourcenamePrefix}', 255)


var FunctionAppName = take('func${resourcenamePrefix}', 60)
var FunctionsHostingPlanName = take('plan${resourcenamePrefix}', 40)
var FunctionsAppStorageName = take('stfuncb${resourcenamePrefix}', 23)

var KeyValultName = take('kv-${resourcenamePrefix}', 24)

var webStorageName = take('webstor${resourcenamePrefix}', 23)
var dataStorageName = take('datastor${resourcenamePrefix}', 23)

module logAnalytics 'logAnalytics.bicep'={
  name: 'logAnalytics'
  params:{
    logAnalyticsName:logAnalyticsName
    tags: defaultTags
  }
}

module applicationInsights 'applicationInsights.bicep' = {
  name: 'appInsights'
  params: {
    appInsightsName: appInsightsName
    logAnalyticsId: logAnalytics.outputs.logAnalyticsId
    tags: defaultTags
  }
}

module keyvault 'keyVault.bicep'={
  name: 'keyvault'
  params:{
    keyVaultName: KeyValultName
    logAnalyticsId: logAnalytics.outputs.logAnalyticsId
    tags: defaultTags
  }
}

module functionApp 'functionApp.bicep'={
  name: 'functionApp'
  params:{
    functionsAppName: FunctionAppName
    functionsHostingPlanName: FunctionsHostingPlanName
    functionsAppStorageName: FunctionsAppStorageName
    appInsightsInstrumentationKey: applicationInsights.outputs.appInsightsInstrumentationKey
    logAnalyticsId: logAnalytics.outputs.logAnalyticsId
    location: location
    tags: defaultTags
    functionsAuthClientId:functionsAuthClientId
    functionsAuthClientSecret:functionsAuthClientSecret
    functionsAuthAllowedAudiances:functionsAuthAllowedAudiances
    functionsAuthIssuerURL:functionsAuthIssuerURL
  }
}

module webStorage './storage.bicep' = {
  name: 'webStorage'
  params: {
    storageAccountName: webStorageName
    location: location
    logAnalyticsId: logAnalytics.outputs.logAnalyticsId
    tags: defaultTags
  }
}

module dataStorage './storage.bicep' = {
  name: 'dataStorage'
  params: {
    storageAccountName: dataStorageName
    location: location
    logAnalyticsId: logAnalytics.outputs.logAnalyticsId
    tags: defaultTags
  }
}
