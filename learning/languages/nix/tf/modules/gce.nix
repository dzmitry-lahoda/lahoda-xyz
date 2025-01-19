({ pkgs, lib, config, options, specialArgs, modulesPath }:
  let size = 100 * 1024;

# {
#   nix = {
#     settings = {
#       substituters = [
#         "https://composable-community.cachix.org"
#       ];
#       trusted-public-keys = [
#         "composable-community.cachix.org-1:GG4xJNpXJ+J97I8EyJ4qI5tRTAJ4i7h+NK2Z32I8sK8="
#       ];
#     };
#   };
# }


# { pkgs, lib, ... }:

# let
#   folder = ./cachix;
#   toImport = name: value: folder + ("/" + name);
#   filterCaches = key: value: value == "regular" && lib.hasSuffix ".nix" key;
#   imports = lib.mapAttrsToList toImport (lib.filterAttrs filterCaches (builtins.readDir folder));
# in {
#   inherit imports;
#   nix.settings.substituters = ["https://cache.nixos.org/"];
# }

# imports = [ ./cachix.nix ];

  in {
    virtualisation.googleComputeImage.configFile =
      builtins.toFile "configuration.nix"
      (builtins.readFile ./google-compute-config.nix);
    virtualisation.googleComputeImage.diskSize = size;

    nix = {
      package = pkgs.nixFlakes;
      extraOptions = ''
        experimental-features = nix-command flakes
        sandbox = relaxed
      '';

      trustedUsers = [ "root" "dzmitry_lahoda_gmail_com" "admin" ];
    };

    imports =
      [ "${toString modulesPath}/virtualisation/google-compute-image.nix" ];
    services.openssh.passwordAuthentication = lib.mkForce true;
    services.openssh.permitRootLogin = lib.mkForce "yes";

    security.acme.defaults.email = "dzmitry@lahoda.pro";
    security.acme.acceptTerms = true;
    services.nginx.enable = true;
    services.nginx.virtualHosts."composablefi.tech" = {
      addSSL = true;
      enableACME = true;
      #root = "/var/www/composablefi.tech";
      root = pkgs.runCommand "testdir" { } ''
        mkdir "$out"
        echo hello world > "$out/index.html"
      '';
      locations."/" = {
        proxyPass = "http://127.0.0.1:9988";
        proxyWebsockets = true;
      };
    };
    services.nginx.virtualHosts.localhost = {
      root = pkgs.runCommand "testdir" { } ''
        mkdir "$out"
        echo hello world > "$out/index.html"
      '';
    };
    services.nginx.logError = "stderr debug";

    networking.firewall = {
      enable = true;
      allowedTCPPorts = [ 80 443 8080 8443 5000 5001 3000 ];
      allowedTCPPortRanges = [{
        from = 9000;
        to = 40000;
      }];
    };

    environment.systemPackages = [ pkgs.git pkgs.devnet-dali ];
  })
