
  security = {
    acme = {
      acceptTerms = true;
    };
  };
  services.nginx.enable = true;
  services.nginx.virtualHosts = {
    "_" = {
      # addSSL = true;
      #enableACME = true;
      root = "/var/www/default";
      # just stub for root page, can route to any usefull info or landing
      locations."/" = {
        root = pkgs.runCommand "testdir" { } ''
          mkdir "$out"
          echo "golden gate base image" > "$out/index.html"
        '';
      };
      locations."/substrate/client" = {
        # any all to external servers is routed to node
        proxyPass = "http://127.0.0.1:${builtins.toString ws_port}";
        proxyWebsockets = true;
      };
    };
  };
  services.nginx.logError = "stderr debug";

  networking.firewall = {
    enable = true;
    inherit allowedTCPPorts;
    # not secure
    allowedTCPPortRanges = [{
      from = 80;
      to = 40000;
    }];
  };


({ pkgs, lib, config, options, specialArgs, modulesPath }:
let
in
{
  system.stateVersion = "23.05";
  nix = {
    package = pkgs.nixFlakes;
    extraOptions = ''
      experimental-features = nix-command flakes
      sandbox = relaxed
    '';
    settings = {
      trusted-users = [ "root" "admin" ];
      extra-substituters = [ "https://cache.nixos.org"  ];
      extra-trusted-public-keys = [ "cache.nixos.org-1:6NCHdD59X431o0gWypbMrAURkbJ16ZPMQFGspcDShjY=" ];
    };
  };

  imports =
    [ "${toString modulesPath}/virtualisation/amazon-image.nix" ];
  services.openssh.settings.PasswordAuthentication = lib.mkForce true;
  services.openssh.settings.PermitRootLogin = lib.mkForce "yes";

  security = {
    acme = {
      acceptTerms = true;
    };
  };

  environment.systemPackages = with pkgs; [ git helix curl websocat jq];
})


{ self, ... }: {
  perSystem =
    { config, self', inputs', pkgs, system, crane, systemCommonRust, ... }:
    let
      ssh_key = "${pkgs.pass}/bin/pass github.com/ComposableFi/composable/mainnet/mantis-solver/ssh_key";
      terraformbase = terraformattrs // {
        TF_VAR_NODE_IMAGE = "\"$(find ${node-image} -type f -name '*.vhd')\"";
      };
      tf-config-base = self.inputs.terranix.lib.terranixConfiguration {
        inherit system;
        modules = [ ./base.nix ];
      };
      amazon-base-image = self.inputs.nixos-generators.nixosGenerate {
        system = "x86_64-linux";
        modules = [
          ./nixos-amazon.nix
        ] ++ [ ({ ... }: { amazonImage.sizeMB = 16 * 1024; }) ]
        ;
        format = "amazon";
      };
    in
    {
      packages =
        (pkgs.lib.optionalAttrs (system == "x86_64-linux") {
          inherit amazon-base-image;

        })
        //
        {


          mainnet = pkgs.writeShellApplication {
            name = "build-ts-schema";
            runtimeInputs = with pkgs; [
              opentofu
              terranix
            ];
            text = ''
              ${pkgs.opentofu} "$@"
            '';
          };
        };
    };
}
