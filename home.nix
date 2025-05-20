{
  config,
  pkgs,
  ...
}:

let

oculante-wrapper = pkgs.writeShellApplication {
 name = "oculante";
 text = ''
   ${pkgs.lib.meta.getExe pkgs.nixgl.nixGLIntel} ${pkgs.lib.meta.getExe pkgs.oculante}
 '';
};


ledger-wrapper = pkgs.writeShellApplication {
 name = "ledger";
 text = ''
   ${pkgs.lib.meta.getExe pkgs.nixgl.nixGLIntel} ${pkgs.lib.meta.getExe pkgs.ledger-live-desktop} "$@"
 '';
};

in
#anki-wrapper = pkgs.writeShellApplication {
#  name = "anki";
#  text = ''
#    ${pkgs.lib.meta.getExe pkgs.nixgl.nixGLIntel} ${pkgs.lib.meta.getExe pkgs.anki}
#  '';
#};
#slack-wrapper = pkgs.writeShellApplication {
#  name = "slack";
#  text = ''
#    ${pkgs.lib.meta.getExe pkgs.nixgl.nixGLIntel} ${pkgs.lib.meta.getExe pkgs.slack}
#  '';
#};
#brave-wrapper = pkgs.writeShellApplication {
#  name = "brave";
#  text = ''
#    ${pkgs.lib.meta.getExe pkgs.nixgl.nixGLIntel} ${pkgs.lib.meta.getExe pkgs.brave}
#  '';
#};
{

  #xdg = {
  #  enable = true;
  #};
  programs =
    let
      myname = "dz";
    in
    {
      go.enable = true;

      #bash = {
      #enable = true;
      #enableCompletion = true;
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
      #};
      #obs-studio.enable = true;

      #brave = {
      #  enable = true;
      #};

      #chromium.enable = true;
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
      # vscode = {
      #   enable = true;
      #   extensions = with pkgs.vscode-extensions; [

      #     #wmaurer.change-case

      #     matklad.rust-analyzer
      #     yzhang.markdown-all-in-one
      #     #ms-azuretools.vscode-docker
      #     ms-vscode-remote.remote-ssh
      #     jnoortheen.nix-ide
      #     github.copilot
      #     vscode-extensions.reditorsupport.r

      #     mads-hartmann.bash-ide-vscode

      #     donjayamanne.githistory

      #     mhutchie.git-graph

      #     #janisdd.vscode-edit-csv

      #     mechatroner.rainbow-csv
      #     # nomicfoundation.hardhat-solidity

      #     streetsidesoftware.code-spell-checker

      #     serayuzgur.crates

      #     # yo1dog.cursor-align

      #     editorconfig.editorconfig

      #     ms-vscode.hexeditor

      #     # dtsvet.vscode-wasm

      #     # alexcvzz.vscode-sqlite
      #     foam.foam-vscode
      #     golang.go
      #     haskell.haskell
      #     ziglang.vscode-zig
      #   ];
      # };
    }
    // (import ./workstation.nix);
  # note in home-manager
  # environment.etc = {
  #   "resolv.conf".text = "nameserver ns-207.awsdns-25.com\n";
  # };

  nix = {
    package = pkgs.nix;
    settings = {
      sandbox = "relaxed";
      experimental-features = [
        "flakes"
        "nix-command"
      ];
      narinfo-cache-negative-ttl = 0;
      system-features = [ "kvm" ];
      max-jobs = 2;
      cores = 24;
      auto-optimise-store = true;
      allow-import-from-derivation = true;
      gc-reserved-space = 18388608;
      http-connections = 32;
      http2 = true;
      min-free = 100000000000;
      max-free = 1000000000000;
      # keep-outputs = true;
      # keep-derivations = true;

      substitute = true;
      trusted-users = [
        "dz"
        "root"
        "dzmitry-lahoda"
        "dzmitry"
      ];

      substituters = [
        "https://nix-community.cachix.org/"
        "https://cache.nixos.org/"
        "https://nixpkgs-update.cachix.org"
      ];

      trusted-public-keys = [
        "nixpkgs-update.cachix.org-1:6y6Z2JdoL3APdu6/+Iy8eZX2ajf09e4EE9SnxSML1W8="
        "cache.nixos.org-1:6NCHdD59X431o0gWypbMrAURkbJ16ZPMQFGspcDShjY="
        "nix-community.cachix.org-1:mB9FSh9qf2dCimDSUo8Zy7bkq5CX+/rkCWyvRCYg3Fs="
        "helix.cachix.org-1:ejp9KQpR1FBI2onstMQ34yogDm4OgU2ru6lIwPvuCVs="
      ];
    };
  };
  # nixpkgs = {
  #   config = {
  #     allowUnfree = true;
  #     extra-substituters = true;

  #     allowUnfreePredicate =
  #       pkg:
  #       builtins.elem (pkgs.lib.getName pkg) [
  #         "vscode"
  #         "vscode-extension-github-copilot"
  #         "vscode-extension-ms-vscode-remote-remote-ssh"
  #         "slack"
  #         "gh"
  #         "nvidia"
  #         "vscode-extensions.reditorsupport.r"
  #         "vscode-extensions.davidlday.languagetool-linter"
  #         "claude-code"
  #       ];
  #   };
  # };
  # not in home
  # networking.nameservers = [ "ns-207.awsdns-25.com" ];

  # not in home
  #virtualisation.docker.enable = true;
  #systemd = {
  #  user = {
  #    services = {
  #      github-runner = {
  #        Service = {
  #          ExecStart = "${pkgs.github-runner}";
  #          WorkingDirectory = "~/";
  #          Restart = "Always";
  #        };
  #     };
  #    };
  #  };
  #};
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
    # :      '';
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
    # sessionVariables = {
    #   NIXPKGS_ALLOW_UNFREE = 1;
    # };
    stateVersion = "24.05";
    username = "dz";
    homeDirectory = "/home/dz";
    packages =
      let

        netboot = [
          pkgs.ipxe
          pkgs.cdrtools
          pkgs.qemu
          pkgs.mtools
        ];
        python-packages =
          ps: with ps; [
            numpy
            cvxpy
            wheel
            virtualenv
          ];
        python = pkgs.python3.withPackages python-packages;
      in
      with pkgs;
      netboot
      ++ [
        python3
        elan
        # lean4
        poetry
        pyo3-pack
        typst
        uv
        (rWrapper.override {
          packages = with rPackages; [
            devtools
            OpenRepGrid
            rgl
          ];
        })
        # dust
        ledger-wrapper
        # rmesg
        #glib.dev
        #libiconv
        #tp-note
        # xz
        act
        alejandra
        attr
        bandwhich
        bat
        bottom
        cachix
        cargo-limit
        cmake
        dasel
        delta
        direnv
        eza
        fd
        fzf
        ripgrep-all
        fsearch
        gh
        claude-code
        codex
        oculante-wrapper
        nasm
        git-lfs
        gopls
        grpcurl
        haskell.compiler.ghcHEAD
        languagetool
        languagetool-rust
        fasttext
        opentofu
        photoqt
        digikam
        qpdf
        dolphin
        helix
        home-manager
        hwinfo
        nurl
        poppler_utils
        hyperfine
        jq
        pandoc
        jujutsu
        tectonic # pdf
        kubo
        languagetool
        lazygit
        llvm
        nginx
        nix
        nix-tree
        nixd
        nixfmt-rfc-style
        nixgl.nixGLIntel
        nodejs
        openssl
        openssl.dev
        pijul
        pkg-config
        procs
        protobuf
        radianWrapper
        rclone
        rclone-browser
        ripgrep
        rocksdb
        rust-script
        rust-toolchain
        ryujinx
        sad
        sd
        shadow
        sqlfluff
        starship
        translate-shell
        watchexec
        websocat
        xsv
        yarn
        yt-dlp
        zoxide
      ];
  };
}

# [Desktop Entry]
# Name=ledger
# Exec=/usr/bin/ledger %u
# NoDisplay=true
# Type=Application
# MimeType=x-scheme-handler/ledgerlive;
