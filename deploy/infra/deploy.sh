#! /bin/bash
set -euxo pipefail
#set -euo pipefail

# Change directory to where this script is
pushd "${0%/*}"

export ROOT=../..
source $ROOT/.env
source $ROOT/deploy/scripts/utilities.sh
source $ROOT/deploy/scripts/login.sh

# Make sure resource group exists or return a reasonable error
rgLocation=$(az group list -o tsv --query "[?name=='$1'].location")
if [ -z "$rgLocation" ]; then
    echo "Make sure you are logged in and resource group ""$1"" exitsts"
    exit 1
else
    echo "deploying to RG: $1 - $rgLocation"
fi

az deployment group create -g "$1" -f "$2"

popd
