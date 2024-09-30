{
  description = "Simple show case script showing language features and env state";
  inputs.nixpkgs.url = "github:NixOS/nixpkgs/nixpkgs-unstable";
  outputs =
    { self, nixpkgs }:
    {
      defaultPackage.x86_64-linux =
        with import nixpkgs { system = "x86_64-linux"; };
        stdenv.mkDerivation {
          name = "hello";
          src = "${self}/learning/languages/nix";
          buildPhase = ''
            gcc --output hello $src/hello.c
          '';
          installPhase = ''
            mkdir --parents $out/bin          
            cp hello $out/bin 
          '';
        };
    };
}
