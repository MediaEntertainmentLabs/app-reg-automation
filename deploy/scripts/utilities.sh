#!/bin/bash
set -euo pipefail

SCRIPTS_DIR="$( dirname "${BASH_SOURCE[0]}" )"
export SCRIPTS_DIR

#$1 file to source if it exists
sourceIfExists() {
    if [ -f "$1" ]; then
        source "$1"
    fi
}
