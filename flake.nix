{
  description = "lahoda.xyz";

  inputs = {
    nixpkgs-unstable = {
      url = "github:NixOS/nixpkgs/nixos-unstable";
    };

    nixpkgs.url = "github:NixOS/nixpkgs/24.05";

    home-manager = {
      url = "github:nix-community/home-manager/release-24.05";
    };
    rust-overlay = {
      url = "github:oxalica/rust-overlay/master";
      inputs.nixpkgs.follows = "nixpkgs";
    };
    nixgl = {
      url = "github:guibou/nixGL/main";
      inputs.nixpkgs.follows = "nixpkgs";
    };
    nix-vscode-extensions.url = "github:nix-community/nix-vscode-extensions";
    helix = {
      url = "github:helix-editor/helix/24.03";
      inputs.nixpkgs.follows = "nixpkgs";
    };
  };

  outputs = inputs@{ self, flake-parts, home-manager, nixpkgs, rust-overlay, nixgl, helix, nixpkgs-unstable, ... }:
    flake-parts.lib.mkFlake { inherit inputs; } {
      systems = [ "x86_64-linux" ];
      perSystem = { config, self', inputs', pkgs, system, ... }:

        let
          overlays = [
            (import rust-overlay)
            nixgl.overlay
            (final: prev:
              {
                rust-toolchain = pkgs.rust-bin.fromRustupToolchainFile ./rust-toolchain.toml;
              }
            )
          ];
          pkgs = import nixpkgs {
            inherit system overlays;
          };
        in
        {
          devShells.default = pkgs.mkShell {
            packages = [
              pkgs.helix
              pkgs.home-manager
              pkgs.rust-toolchain
              ];
          };

          packages = {
            update = pkgs.writeShellApplication {
              name = "update";
              text = ''
                export NIXPKGS_ALLOW_UNFREE=1 && home-manager switch --flake .
              '';
            };
          };
        };

      flake =
        let
          system = "x86_64-linux";
          overlays = [
            (import rust-overlay)
            nixgl.overlay
            (final: prev:
              {
                rust-toolchain = pkgs.rust-bin.fromRustupToolchainFile ./rust-toolchain.toml;
              }
            )
          ];
          pkgs = import nixpkgs {
            inherit system overlays;
            config = {
              packageOverrides = pkgs: {
                helix = helix.packages.${system}.helix;
                vscode = nixpkgs-unstable.legacyPackages.${system}.vscode;
                brave = nixpkgs-unstable.legacyPackages.${system}.brave;
                nix = nixpkgs-unstable.legacyPackages.${system}.nix;
                rust-toolchain = nixpkgs-unstable.legacyPackages.${system}.rust-bin.fromRustupToolchainFile ./rust-toolchain.toml;
              };
            };
          };
        in
        {
          homeConfigurations.dz = home-manager.lib.homeManagerConfiguration {
            inherit pkgs;
            modules = [
              ./home.nix
            ];
          };
        };
    };
}
