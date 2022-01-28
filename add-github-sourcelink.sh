#!/bin/bash

###############################################################################
# add-github-sourcelink                                                       #
###############################################################################
# Adds the Microsoft.SourceLink.GitHub package to projects in the solution or #
# the project file. Use it in the git repository's root directory.            #
###############################################################################

function err() 
{
    echo "[$(date +'%Y-%m-%dT%H:%M:%S%z')]: $*" >&2
}

function panic() 
{
    error_code="$1"
    shift

    for line in "$@"; do
        err "$line"
    done
    
    exit "$error_code"
}

function alter-solution()
{
    solution_file="$1"
    [ -f "$solution_file" ] || panic 1 "Couldn't find the solution '$solution_file' in '$PWD'."

    for project_path in $(dotnet sln solution_file list | sed '1,2 D'); do
        directory=$(dirname "$project_path")

        pushd "$directory" || panic 2 "Couldn't open the project directory '$directory'."
        alter-project "$(basename "$project_path")"
        popd || panic 3 "Couldn't return to the original directory."
    done
}

function alter-project()
{
    project_file="$1"
    [ -f "$project_file" ] || panic 4 "Couldn't find the project '$project_file' in '$PWD'."
    
    dotnet add "$project_file" package Microsoft.SourceLink.GitHub
}

if [ -f "$1" ]; then
    alter-solution "$1"
elif solutions=(./*.sln); (( ${#solutions[@]} )); then
    for solution in "${solutions[@]}"; do
        alter-solution "$solution"
    done
else
    for project in ./*.??proj; do
        alter-project "$project"
    done
fi