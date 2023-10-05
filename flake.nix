{
  description = "lahoda.xyz";

  inputs = {
    nixpkgs-unstable = {
      url = "github:NixOS/nixpkgs/nixos-unstable";
    };
    nixpkgs.url = "github:NixOS/nixpkgs/23.05";

    home-manager = {
      url = "github:nix-community/home-manager/release-23.05";
    };
    rust-overlay = {
      url = "github:oxalica/rust-overlay/stable";
    };
    nixgl = {
      url = "github:guibou/nixGL/main";
    };
    nix-vscode-extensions.url = "github:nix-community/nix-vscode-extensions";
    helix = {
      url = "github:helix-editor/helix/23.05";
    };
  };

  outputs = inputs@{ self, flake-parts, home-manager, nixpkgs, rust-overlay, nixgl, helix, nixpkgs-unstable, ... }:
    flake-parts.lib.mkFlake { inherit inputs; } {
      systems = [ "x86_64-linux" ];
      perSystem = { config, self', inputs', pkgs, system, ... }: {
        devShells.default = pkgs.mkShell {
          nativeBuildInputs = [ pkgs.act pkgs.helix pkgs.home-manager ];
        };

        # export NIXPKGS_ALLOW_UNFREE=1 && home-manager switch --flake .
      };

      flake =
        let
          system = "x86_64-linux";
          overlays = [
            (import rust-overlay)
            nixgl.overlay
          ];
          pkgs = import nixpkgs {
            inherit system overlays;
            config = {
              packageOverrides = pkgs : {
               helix = helix.packages.${system}.helix;
               vscode = nixpkgs-unstable.legacyPackages.${system}.vscode;
               brave = nixpkgs-unstable.legacyPackages.${system}.brave;
              };
            };
          };
        in
        {
          homeConfigurations.dz = home-manager.lib.homeManagerConfiguration {
            inherit pkgs;
            modules = [ ./home.nix ];
          };
        };
    };
}
