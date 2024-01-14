#!/usr/bin/env sh
set -o errexit
if test ! -d build ; then mkdir build; fi
nix run nixpkgs#cmake -- -Bbuild
(
    cd build
    make
)
chmod +x ./build/test
./build/test 43