#! /bin/bash
set -euxo pipefail
#set -euo pipefail

# Change directory to where this script is
pushd "${0%/*}"

export ROOT=../..

source $ROOT/deploy/scripts/utilities.sh
source "$ROOT"/deploy/scripts/loadEnvVars.sh
source $ROOT/deploy/scripts/login.sh

#Use passed in or default RG name
rgName=${1-$DEFAULT_RG_NAME}

#Make sure reg is created, use Default region if need to deploy
if [ "$(az group exists --name "$rgName")" = false ]; then
    az group create --name "$rgName" --location "$DEFAULT_DEPLOY_REGION"
fi

#Makes incremental deploysments a bit easier if we can pass in a bicep file name
bicepfile=${2-$DEFAULT_BICEP_FILE}

az deployment group create -n "$DEFAULT_DEPLOYMENT_NAME" -g "$rgName" -f "$bicepfile" --parameters \
functionsAuthClientId="$FUNCTION_CLIENT_ID" \
functionsAuthClientSecret="$FUNCTION_CLIENT_SECRET" \
functionsAuthAllowedAudiances="$FUNCTION_ALLOWED_AUDIANCES" \
functionsAuthIssuerURL="$FUNCTION_AUTH_ISSUER"

popd
