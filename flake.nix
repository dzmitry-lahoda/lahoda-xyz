{
  description = "lahoda.xyz";

  inputs = {
    nixpkgs.url = "github:NixOS/nixpkgs/nixos-22.11";

    home-manager = {
      url = "github:nix-community/home-manager/release-22.11";
      inputs.nixpkgs.follows = "nixpkgs";
    };
    rust-overlay = {
      url = "github:oxalica/rust-overlay";
      inputs.nixpkgs.follows = "nixpkgs";
    };
    nixgl.url = "github:guibou/nixGL";
  };

  outputs = inputs@{ self, flake-parts, home-manager, nixpkgs, rust-overlay, nixgl }:
    flake-parts.lib.mkFlake { inherit inputs; } {
      systems = [ "x86_64-linux" ];
      perSystem = { config, self', inputs', pkgs, system, ... }: {
        devShells.default = pkgs.mkShell {
          nativeBuildInputs = [ pkgs.act pkgs.helix pkgs.home-manager ];
        };
        # apps
        #  home-manager switch --flake .
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
