({ pkgs, lib, config, options, specialArgs, modulesPath }:
  let size = 100 * 1024;

  in {


    nix = {
      package = pkgs.nixFlakes;
      extraOptions = ''
        experimental-features = nix-command flakes
        sandbox = relaxed
      '';
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
      locations."/dali" = {
        proxyPass = "http://127.0.0.1:9988";
        proxyWebsockets = true;
      };
      locations."/rococo" = {
        proxyPass = "http://127.0.0.1:9944";
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
      allowedTCPPorts = [ 80 443 8080 8443 5000 5001 3000 9988 9944 31200 32200 ];
      allowedTCPPortRanges = [{
        from = 9000;
        to = 40000;
      }];
    };

    environment.systemPackages = [ pkgs.git pkgs.devnet-dali ];
  })
