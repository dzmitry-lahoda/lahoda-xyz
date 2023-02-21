{
  description = "Description for the project";

  inputs = { nixpkgs.url = "github:NixOS/nixpkgs/nixos-unstable"; };

  outputs = { self, flake-parts, ... }:
    flake-parts.lib.mkFlake { inherit self; } {
      systems = [ "x86_64-linux" "aarch64-darwin" ];
      perSystem = { config, self', inputs', pkgs, system, ... }: {
        packages.default = pkgs.hello;

        devShells.default = pkgs.mkShell {
          nativeBuildInputs = [ pkgs.act];
        };

      };
      flake = { };
    };
}
