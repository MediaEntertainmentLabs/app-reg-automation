#!/bin/bash
set -euo pipefail

#$1 file to source if it exists
sourceIfExists() {
    if [ -f "$1" ]; then
        source $1
    fi
}