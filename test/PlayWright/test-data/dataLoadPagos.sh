#!/bin/bash
HOST=$1
SLEEPTIME=$2
CONNECTION_STRING=$3
query=""

export PATH="$PATH:/opt/mssql-tools/bin"

parse_connection_string() {
    local connection_string=$1
    local params
    IFS=';' read -r -a params <<< "$connection_string"

    for param in "${params[@]}"; do
        case $param in
            Server=*) SERVER="${param#*=}" ;;
            Data\ source=*) SERVER="${param#*=}" ;;
            Database=*) DATABASE="${param#*=}" ;;
            Initial\ catalog=*) DATABASE="${param#*=}" ;;
            User=*) DB_USER="${param#*=}" ;;
            Password=*) DB_PASSWORD="${param#*=}" ;;
        esac
    done
}

parse_connection_string "$CONNECTION_STRING"

sqlcmd -S "$SERVER" -d "$DATABASE" -U "$DB_USER" -P "$DB_PASSWORD" -Q "$query"

# Data Preparation