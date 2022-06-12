#!/bin/bash
declare -a archs=("win-x86" "win-x64" "win-arm" "win-arm64" "linux-x64" "linux-arm" "osx-x64")
targetdir="./bin/publish/"

# get the project name
projname=${PWD##*/}
projname=${projname:-/} 
echo "Project name: $projname"

for arch in "${archs[@]}"
do
  echo "Building $arch..."
  pubpath=$targetdir$arch
  mkdir -p $pubpath
  dotnet publish -c Release -r $arch -o $pubpath
  (cd $targetdir; zip -r "$projname-$arch.zip" "$arch")
done

