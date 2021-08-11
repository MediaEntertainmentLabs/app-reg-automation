#!/bin/bash

set -euxo pipefail
#set -euo pipefail

export ROOT=../..
source $ROOT/.env
source $ROOT/deploy/scripts/utilities.sh
source $ROOT/deploy/scripts/login.sh

GetAppIdByName() {
    local appId
    appId=$(az ad app list --all --filter "displayName eq '$1'" -o tsv --query "[?displayName=='$1'].appId")
    echo "$appId"
}

#Parameters <app reg name>
CreateAppRegistration() {
    local appId
    appId=$(GetAppIdByName "$1")

    if [ -z "$appId" ]; then
        appId=$(az ad app create --display-name "$1" -o tsv --query "appId")
    fi

    echo "$appId"
}

#TODO: split up the roles and check to make sure it doesn't already exists
#If it exists, disable it first then update and reenable

#Parameters <app reg id> <role file>
AddRolesToAppReg() {
    az ad app update --id "$1" --app-roles "$2"
}

#Parameters <app reg id>
GetSpIdByApplicationId() {
    local objectId
    objectId=$(az ad sp list --all --filter "appId eq '$1'" -o tsv --query "[?appId=='$1'].objectId")
    echo "$objectId"
}

#Parameters <app reg id>
CreateServicePrincipal() {
    local objectId
    objectId=$(GetSpIdByApplicationId "$1")

    if [ -z "$objectId" ]; then
        objectId=$(az ad sp create --id "$1" -o tsv --query "objectId")
    fi

    echo "$objectId"
}

#Parameters <app reg id> <scope name>
GetScopeId() {
    local scopeId
    scopeId=$(az ad app list --all -o tsv --filter "appId eq '$1'" --query "[0].oauth2Permissions[?value=='$2'].id")
    echo "$scopeId"
}

PORTAL_APPID=$(CreateAppRegistration $PORTAL_APP_REG_NAME)
PORTAL_SP_OBJECTID=$(CreateServicePrincipal "$PORTAL_APPID")
AddRolesToAppReg $PORTAL_APPID @applicationRoles.json

FUNCTION_APPID=$(CreateAppRegistration $FUNCTIONS_APP_REG_NAME)
FUNCTION_SP_OBJECTID=$(CreateServicePrincipal "$FUNCTION_APPID")
AddRolesToAppReg $FUNCTION_APPID @applicationRoles.json

#Add user impersonation permission to the portal app
FUNCTION_APP_USER_IMPERSONATION_ID=$(GetScopeId "$FUNCTION_APPID" user_impersonation)

az ad app permission add --id "$PORTAL_APPID" --api "$FUNCTION_APPID" --api-permissions "$FUNCTION_APP_USER_IMPERSONATION_ID"
az ad app permission grant --id "$PORTAL_APPID" --api "$FUNCTION_APPID" --expires "never" --consent-type AllPrincipals --scope "user_impersonation"

#TODO: Add optional claims here az ad app update -h