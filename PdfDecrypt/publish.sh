#!/bin/bash
declare -a archs=("win-x86" "win-x64" "win-arm" "win-arm64" "linux-x64" "linux-arm" "osx-x64")
SCRIPT_DIR="$( cd -- "$( dirname -- "${BASH_SOURCE[0]:-$0}"; )" &> /dev/null && pwd 2> /dev/null; )";
TARGET_DIR="$SCRIPT_DIR/bin/publish/"
echo "Target dir:$TARGET_DIR"
projname="$(basename $SCRIPT_DIR)"
echo "Project name: $projname"

for arch in "${archs[@]}"
do
  echo "Building $arch..."
  pubpath=$TARGET_DIR$arch
  mkdir -p $pubpath
  dotnet publish -c Release -r $arch -o $pubpath
  (cd $TARGET_DIR; zip -r "$projname-$arch.zip" "$arch")
done

