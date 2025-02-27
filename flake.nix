{
  description = "lahoda.xyz";
  inputs = {
    nixpkgs.url = "github:NixOS/nixpkgs/release-24.11";
    home-manager = {
      url = "github:nix-community/home-manager/release-24.11";
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
      url = "github:helix-editor/helix/25.01.1";
      inputs.nixpkgs.follows = "nixpkgs";
    };
  };

  outputs =
    inputs@{
      self,
      flake-parts,
      nixpkgs,
      rust-overlay,
      nixgl,
      helix,
      ...
    }:
    flake-parts.lib.mkFlake { inherit inputs; } {
      systems = [ "x86_64-linux" ];
      perSystem =
        {
          config,
          self',
          inputs',
          pkgs,
          system,
          ...
        }:

        let
          overlays = [
            (import rust-overlay)
            nixgl.overlay
            (final: prev: {
              rust-toolchain = pkgs.rust-bin.fromRustupToolchainFile ./rust-toolchain.toml;
              #home-manager = home-manager.packages.${system}.home-manager;
            })
          ];
          pkgs = import nixpkgs { inherit system overlays; };
        in
        {
          _module.args.pkgs = import nixpkgs {
            inherit system;
            config.allowUnfree = true;
          };
          devShells.default = pkgs.mkShell {
            packages = with pkgs; [
              helix
              home-manager
              rust-toolchain
            ];
          };
          formatter = pkgs.nixfmt-rfc-style;

          packages = {
            update = pkgs.writeShellApplication {
              name = "update";
              runtimeInputs = with pkgs; [ home-manager ];
              text = builtins.readFile ./nix/activate.sh;
            };
          };
        };

      flake =
        let
          system = "x86_64-linux";
          overlays = [
            (import rust-overlay)
            nixgl.overlay
            (final: prev: { rust-toolchain = pkgs.rust-bin.fromRustupToolchainFile ./rust-toolchain.toml; })
          ];
          pkgs = import nixpkgs {
            inherit system overlays;
            config = {
              packageOverrides = pkgs: {
                helix = helix.packages.${system}.helix;
                vscode = nixpkgs.legacyPackages.${system}.vscode;
                brave = nixpkgs.legacyPackages.${system}.brave;
                nix = nixpkgs.legacyPackages.${system}.nix;
                rust-toolchain =
                  nixpkgs.legacyPackages.${system}.rust-bin.fromRustupToolchainFile
                    ./rust-toolchain.toml;
              };
            };
          };
        in
        {
          homeConfigurations.dz = inputs.home-manager.lib.homeManagerConfiguration {
            inherit pkgs;
            modules = [ ./home.nix ];
          };
        };
    };
}
