#! /bin/bash
set -euxo pipefail

# Change directory to where this script is
pushd "${0%/*}"

export ROOT=..

source "$ROOT"/deploy/scripts/utilities.sh
source "$ROOT"/deploy/scripts/loadEnvVars.sh
source "$ROOT"/deploy/scripts/login.sh

DEFAULT_PUBLISH_DIR=$ROOT/src/AppRegPortal/bin/Debug/net5.0/Publish/wwwroot/
DEFAULT_CODE_DIR=$ROOT/src/AppRegPortal
DEFAULT_PUBLISH_CONFIG=Debug

ACCOUNT_NAME=$1
PUBLISH_DIR=${2-$DEFAULT_PUBLISH_DIR}
CODE_DIR=${3-$DEFAULT_CODE_DIR}
PUBLISH_CONFIG=${4-$DEFAULT_PUBLISH_CONFIG}

pushd "$CODE_DIR"
dotnet publish -c "$PUBLISH_CONFIG" -o "$PUBLISH_DIR"
popd

az storage blob service-properties update --account-name "$ACCOUNT_NAME" --static-website --index-document 'index.html'
az storage blob upload-batch --destination '$web' --account-name "$ACCOUNT_NAME" -s "$PUBLISH_DIR"
#Need to add https://<account name>.web.core.windows.net//authentication/login-callback to portal app reg

popd