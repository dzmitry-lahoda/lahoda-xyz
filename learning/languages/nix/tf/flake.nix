{
  description = "A very basic flake";
  inputs = {
    nixpkgs.url = "github:NixOS/nixpkgs/nixos-22.11";
    flake-utils.url = "github:numtide/flake-utils";
    composable.url = "github:ComposableFi/composable";
    nixos-generators = {
      url = "github:nix-community/nixos-generators";
      inputs.nixpkgs.follows = "nixpkgs";
    };
    terranix = {
      url = "github:terranix/terranix";
      inputs.nixpkgs.follows = "nixpkgs";
    };
  };
  outputs =
    { self, nixpkgs, nixos-generators, terranix, flake-utils, composable }:
    let

      pkgs = nixpkgs.legacyPackages.x86_64-linux;

      system = "x86_64-linux";

    in rec {

      packages.x86_64-linux = {

        tfconfig = terranix.lib.terranixConfiguration {
          inherit system;
          modules = [ ./terranix/05.nix ];
        };

        tfconfig-01 = terranix.lib.terranixConfiguration {
          inherit system;
          modules = [ ./terranix/01.nix ];
        };

        hello = nixpkgs.legacyPackages.x86_64-linux.hello;

        virtualbox = nixos-generators.nixosGenerate {
          system = "x86_64-linux";

          modules = [
            ./modules/virtualbox.nix
            #"${toString modulesPath}/installer/virtualbox-demo.nix" 
          ];

          format = "virtualbox";
        };

        gce = nixos-generators.nixosGenerate {
          system = "x86_64-linux";
          modules = [
            {
              nixpkgs.overlays = [
                (_: _: {
                  devnet-dali = composable.packages.x86_64-linux.devnet-dali;
                })
              ];
            }

            ./modules/gce.nix

          ];

          format = "gce";
        };

        init = pkgs.writeShellApplication {
          name = "init";
          text = ''
            gcloud services enable compute.googleapis.com dns.googleapis.com certificatemanager.googleapis.com
          '';
        };

        apply = pkgs.writeShellApplication {
          name = "apply";
          text = ''
            TF_VAR_IMAGE_FILE="$(find ${self.packages.x86_64-linux.gce} -type f)"
            TF_VAR_PROJECT="composablefi"
            TF_VAR_NODE_NAME="composablefi"
            export TF_VAR_IMAGE_FILE
            export TF_VAR_NODE_NAME
            export TF_VAR_PROJECT
            cd terraform/layers/05
            if [[ -e config.tf.json ]]; then rm -f config.tf.json; fi
            cp ${self.packages.${system}.tfconfig} config.tf.json \
              && ${pkgs.terraform}/bin/terraform init \
              && ${pkgs.terraform}/bin/terraform apply -auto-approve
          '';
        };

        default = self.packages.x86_64-linux.hello;
      };

      # shell with nixos-container
      nixosConfgurations.myhost = pkgs.lib.nixosSystem {
        system = "x86_64-linux";
        modules = [ ./modules/virtualbox.nix ];
      };

      apps.${system} = {
        apply = self.inputs.flake-utils.lib.mkApp {
          drv = self.packages.${system}.apply;
        };
      };

      devShell.x86_64-linux = pkgs.mkShell {
        buildInputs = with pkgs; [
          nixos-generators.packages.x86_64-linux.nixos-generate
          hello
          direnv
          nix-direnv
          awscli2
          google-cloud-sdk
          terraform
          terranix.defaultPackage.${system}
          self.packages.${system}.tfconfig
        ];
        shellHook = ''
          (
            cd gce-nixos/terraform/
            terraform init --upgrade
          )
        '';
      };

      # hook to run `complete -C aws_completer aws`

      crazy = "yes";
    };
}
