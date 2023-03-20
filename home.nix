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
      ${pkgs.lib.meta.getExe pkgs.nixgl.nixGLIntel} ${pkgs.lib.meta.getExe pkgs.ledger-live-desktop} 
    '';
  };
in
{
  programs = {
    bash = {
      enable = true;
    };
    obs-studio.enable = true;
    # does not integrate to ui auto
    # but works with hardware keys :) 
    brave.enable = true; # TODO: gl fix

    chromium.enable = true;
    git = {
      enable = true;
      userName = "dzmitry-lahoda";
      userEmail = "dzmiry@lahoda.pro";
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
      ];
    };
  };
  nix = {
    package = pkgs.nix;
    settings = {
      sandbox = false;
      substitute = true;

      substituters = [
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


      # extra-trusted-public-keys = [
      #   "nix-community.cachix.org-1:mB9FSh9qf2dCimDSUo8Zy7bkq5CX+/rkCWyvRCYg3Fs="
      #   "composable-community.cachix.org-1:GG4xJNpXJ+J97I8EyJ4qI5tRTAJ4i7h+NK2Z32I8sK8="
      # ];
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
    };

  };

  # not in home
  #virtualisation.docker.enable = true;
  services = {
    gpg-agent = {
      enable = true;
      enableSshSupport = true;
    };
    syncthing = {
      enable = true;
    };
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
    stateVersion = "22.11";
    username = "dz";
    homeDirectory = "/home/dz";
    packages = with pkgs; [
      anki-wrapper
      translate-shell
      ledger-wrapper
      bottom
      helix
      nixpkgs-fmt
      tdesktop
      home-manager
      tg
      telegram-cli
      slack
      lazygit
      kubo
      rnix-lsp
      # these 2 are broken if installed into home like this 
      # podman   
      shadow
      attr
      rust-script
      nodejs
      yarn
      (rust-bin.fromRustupToolchainFile ./rust-toolchain.toml) #cargo rustc rustfmt ..
      nixgl.nixGLIntel
    ];
  };
}
