{
  description = "lahoda.xyz";
  inputs = {
    nixpkgs.url = "github:NixOS/nixpkgs/release-24.11";
    nixpkgs-unstable.url = "github:NixOS/nixpkgs/nixos-unstable";
    home-manager = {
      url = "github:nix-community/home-manager/release-24.11";
      inputs.nixpkgs.follows = "nixpkgs";
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
      nixpkgs-unstable,
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
              jujutsu = inputs.nixpkgs-unstable.legacyPackages.${system}.jujutsu;
              #home-manager = home-manager.packages.${system}.home-manager;
            })
          ];
          pkgs = import nixpkgs {
            inherit system overlays;
            # config = {
            #     allowUnfree = true;
            #     allowUnfreePredicate =
            #       pkg:
            #       builtins.elem (pkgs.lib.getName pkg) [
            #         "claude-code"
            #       ];
            #   };
          };
        in
        {
          _module.args.pkgs = import nixpkgs {
            inherit system;
            # config = {
            #   allowUnfree = true;
            #   allowUnfreePredicate =
            #     pkg:
            #     builtins.elem (pkgs.lib.getName pkg) [
            #       "claude-code"
            #     ];
            # };
          };
          formatter = pkgs.nixfmt-rfc-style;

          packages = {
            activate = pkgs.writeShellApplication {
              name = "activate";
              runtimeInputs = with pkgs; [ home-manager ];
              text = builtins.readFile ./nix/activate.sh;
            };

            pdf = pkgs.writeShellApplication {
              name = "pdf";
              runtimeInputs = with pkgs; [ home-manager ];
              text = builtins.readFile ./nix/pdf.sh;
            };
          };
        };

      flake =
        let
          unstable-pkgs = import nixpkgs-unstable {
            inherit system;
            config = {
              allowUnfree = true; # must set this because these has own config, not shared with nixpkgs.
              allowUnfreePredicate =
                pkg:
                builtins.elem (pkgs.lib.getName pkg) [
                  "claude-code"
                ];
            };
          };
          system = "x86_64-linux";
          overlays = [
            (import rust-overlay)
            nixgl.overlay
            (final: prev: {
              rust-toolchain = pkgs.rust-bin.fromRustupToolchainFile ./rust-toolchain.toml;
              jujutsu = inputs.nixpkgs-unstable.legacyPackages.${system}.jujutsu;
              claude-code = unstable-pkgs.claude-code;
            })
          ];
          pkgs = import nixpkgs {
            inherit system overlays;
            config = {
              allowUnfree = true;
              allowUnfreePredicate =
                pkg:
                builtins.elem (pkgs.lib.getName pkg) [
                  "vscode"
                  "vscode-extension-github-copilot"
                  "vscode-extension-ms-vscode-remote-remote-ssh"
                  "slack"
                  "gh"
                  "nvidia"
                  "vscode-extensions.reditorsupport.r"
                  "vscode-extensions.davidlday.languagetool-linter"
                  "claude-code"
                ];
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
            # add nixos-instable to this module
            # so that it can be used in the home-manager module

            modules = [ ./home.nix ];
          };
        };
    };
}
