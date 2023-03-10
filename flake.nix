{
  description = "Description for the project";

  inputs = {
  nixpkgs.url = "github:NixOS/nixpkgs/nixos-22.11"; 
  
  home-manager =  {
     url = "github:nix-community/home-manager/release-22.11";
     inputs.nixpkgs.follows = "nixpkgs";
     
  };
  };

  outputs = { self, flake-parts, home-manager, nixpkgs }:
    flake-parts.lib.mkFlake { inherit self; } {
      systems = [ "x86_64-linux"];
      perSystem = { config, self', inputs', pkgs, system, ... }: { 
        devShells.default = pkgs.mkShell {
          nativeBuildInputs = [ pkgs.act pkgs.helix pkgs.home-manager];
        };
        # apps
        #  home-manager switch --flake .
      };
      flake = let pkgs = nixpkgs.legacyPackages.x86_64-linux; in {
        homeConfigurations.dz = home-manager.lib.homeManagerConfiguration {
            inherit pkgs;
            modules = [./home.nix ];             
        };
 };
    };
}
