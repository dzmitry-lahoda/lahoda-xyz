{ config, pkgs, ... }:

let
  anki-wrapper = pkgs.writeShellApplication {
    name = "anki";
    text = ''
      ${pkgs.lib.meta.getExe pkgs.nixgl.nixGLIntel} ${pkgs.lib.meta.getExe pkgs.anki} 
    '';
  };
  ledger-wrapper = pkgs.writeShellApplication {
    name = "ledger";
    text = ''
      ${pkgs.lib.meta.getExe pkgs.nixgl.nixGLIntel} ${pkgs.lib.meta.getExe pkgs.ledger-live-desktop} "$@" 
    '';
  };
  slack-wrapper = pkgs.writeShellApplication {
    name = "slack";
    text = ''
      ${pkgs.lib.meta.getExe pkgs.nixgl.nixGLIntel} ${pkgs.lib.meta.getExe pkgs.slack} 
    '';
  };

in
{

  xdg = {
    enable = true;
  };
  programs = let myname = "dzmitry"; in
    {
      go.enable = true;
      bash = {
        enable = true;
        enableCompletion = true;
        #enableLsColors = true;
        #blesh.enable = true;
        #enableAutosuggestions = true;
        #enableSyntaxHighlighting = true;
        # oh-my-bash = {
        #   enable = true;
        #   plugins = [
        #     "command-not-found"
        #     "history"
        #     "history-substring-search"
        #   ];
        #   custom = "$HOME/.config/bash/custom";
        # };
      };
      obs-studio.enable = true;

      # does not integrate to ui auto
      # but works with hardware keys :) 
      brave = {
        enable = true;
      }; # TODO: gl fix


      chromium.enable = true;
      git = {
        enable = true;
        userName = "${myname}-lahoda";
        userEmail = "${myname}@lahoda.pro";
        extraConfig = {
          user.signingkey = "B6DAD565C19E1C302B735664BF77F46FF7501BE1";
          commit.gpgsign = true;
          core.editor = "${pkgs.helix}/bin/hx";
        };
      };
      vscode = {
        enable = true;
        extensions = with pkgs.vscode-extensions; [

          #wmaurer.change-case

          matklad.rust-analyzer
          yzhang.markdown-all-in-one
          ms-azuretools.vscode-docker
          ms-vscode-remote.remote-ssh
          jnoortheen.nix-ide
          github.copilot

          mads-hartmann.bash-ide-vscode

          donjayamanne.githistory

          mhutchie.git-graph

          #janisdd.vscode-edit-csv

          mechatroner.rainbow-csv
          # nomicfoundation.hardhat-solidity

          streetsidesoftware.code-spell-checker

          serayuzgur.crates

          # yo1dog.cursor-align

          editorconfig.editorconfig

          ms-vscode.hexeditor

          # dtsvet.vscode-wasm

          # alexcvzz.vscode-sqlite
          foam.foam-vscode

          haskell.haskell
        ];
      };
    };
  # note in home-manager
  # environment.etc = {
  #   "resolv.conf".text = "nameserver ns-207.awsdns-25.com\n";
  # };

  nix = {
    package = pkgs.nix;
    settings = {
      sandbox = false;
      substitute = true;

      extra-substituters = [
        "https://nix-community.cachix.org/"
        "https://cache.nixos.org/"
        "https://composable-community.cachix.org/"
        "https://devenv.cachix.org/"
      ];


      extra-trusted-substituters = [
        "https://nix-community.cachix.org/"
        "https://cache.nixos.org/"
        "https://composable-community.cachix.org/"
        "https://devenv.cachix.org/"
      ];


      extra-trusted-public-keys = [
        "nix-community.cachix.org-1:mB9FSh9qf2dCimDSUo8Zy7bkq5CX+/rkCWyvRCYg3Fs="
        "composable-community.cachix.org-1:GG4xJNpXJ+J97I8EyJ4qI5tRTAJ4i7h+NK2Z32I8sK8="
        "helix.cachix.org-1:ejp9KQpR1FBI2onstMQ34yogDm4OgU2ru6lIwPvuCVs="
        "mitchellh-nixos-config.cachix.org-1:bjEbXJyLrL1HZZHBbO4QALnI5faYZppzkU4D2s0G8RQ="
      ];
    };
  };
  nixpkgs = {
    config = {
      extra-substituters = true;
      allowUnfreePredicate = pkg: builtins.elem (pkgs.lib.getName pkg) [
        "vscode"
        "vscode-extension-github-copilot"
        "vscode-extension-ms-vscode-remote-remote-ssh"
        "slack"
        "nvidia"
      ];
      permittedInsecurePackages = [
        "nodejs-16.20.0"
      ];
    };

  };
  # not in home
  # networking.nameservers = [ "ns-207.awsdns-25.com" ];

  # not in home
  #virtualisation.docker.enable = true;
  systemd = {
    user = {
      services = {
        github-runner = {
          Service = {
            ExecStart = "${pkgs.github-runner}";
            WorkingDirectory = "~/";
            Restart = "Always";
          };
        };
      };
    };
  };
  services = {
    gpg-agent = {
      enable = true;
      enableSshSupport = true;
    };
    syncthing = {
      enable = true;
    };


    # nginx.virtualHosts = {
    #   "_" = {
    #     addSSL = true;
    #     enableACME = true;
    #     root = "/var/www/default";
    #     locations."/" = {
    #       root = pkgs.runCommand "testdir" { } ''
    #         mkdir "$out"
    #         echo "love is all you need" > "$out/index.html"
    #       '';
    #     };
    #     locations."/substrate/client" = {
    #       proxyPass = "ws://34.88.1.226:9944";
    #       proxyWebsockets = true;
    #     };
    #   };
    # };

    # this requiest boot and kernel, so cannot do like that - need custom service
    #kubo = {
    # enable = true;
    # user = "dz";
    # group = "dz";
    # autoMount = true;
    # localDiscovery = true;
    #};
  };
  home = {
    sessionVariables = {
      LD_LIBRARY_PATH = pkgs.lib.strings.makeLibraryPath [
        pkgs.stdenv.cc.cc.lib
        pkgs.llvmPackages.libclang.lib
        #pkgs.openssl.dev        
      ];
      # PKG_CONFIG_PATH = "${pkgs.openssl.dev}/lib/pkgconfig";
      LIBCLANG_PATH = "${pkgs.llvmPackages.libclang.lib}/lib";
      PROTOC = "${pkgs.protobuf}/bin/protoc";
      ROCKSDB_LIB_DIR = "${pkgs.rocksdb}/lib";
    };
    stateVersion = "22.11";
    username = "dz";
    homeDirectory = "/home/dz";
    packages = with pkgs; [
      pkg-config
      openssl
      #openssl.dev
      #libiconv
      #pkgconfig
      anki-wrapper
      translate-shell
      # ledger-wrapper
      # ledger-live-desktop
      bottom
      helix
      hwinfo
      qbittorrent
      nixpkgs-fmt
      tdesktop
      home-manager
      tg
      telegram-cli
      slack-wrapper
      lazygit
      kubo
      rnix-lsp
      # these 2 are broken if installed into home like this 
      # podman   
      shadow
      rclone
      rclone-browser
      attr
      git-lfs
      rust-script
      nodejs
      yarn
      (rust-bin.fromRustupToolchainFile ./rust-toolchain.toml) #cargo rustc rustfmt ..
      nixgl.nixGLIntel
      vlc
      llvm
      sd
      sad
      gh
      xsv
      nginx
      github-runner
      haskell.compiler.ghcHEAD
      docker-compose
    ];
  };
}
