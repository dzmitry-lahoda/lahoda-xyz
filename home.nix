{ config, pkgs, ... }: {
  programs = {
    bash = {
      enable = true;
    };
    brave.enable = true;
    chromium.enable = true;

    git = {
      enable = true;
      userName = "dzmitry-lahoda";
      userEmail = "dzmiry@lahoda.pro";
    };
    vscode = {
      enable = true;
      extensions = with pkgs.vscode-extensions; [
        matklad.rust-analyzer
        yzhang.markdown-all-in-one
        ms-azuretools.vscode-docker
        ms-vscode-remote.remote-ssh
        jnoortheen.nix-ide
        github.copilot

        mads-hartmann.bash-ide-vscode

        donjayamanne.githistory

        mhutchie.git-graph

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
    settings = { sandbox = false; };
  };
  nixpkgs = {
    config = {
      #allowUnfree = true;
      extra-substituters = true;
      allowUnfreePredicate = pkg: builtins.elem (pkgs.lib.getName pkg) [
        "vscode"
        "vscode-extension-github-copilot"
        "vscode-extension-ms-vscode-remote-remote-ssh"
        "slack"
      ];
    };

  };

  services = {
    gpg-agent = {
      enable = true;
      enableSshSupport = true;
    };


  };
  home = {
    stateVersion = "22.11";
    username = "dz";
    homeDirectory = "/home/dz";
    packages = with pkgs; [
      pkgs.bottom
      pkgs.helix
      pkgs.cargo
      pkgs.rustfmt
      pkgs.rust-script
      pkgs.rustc
      pkgs.nixpkgs-fmt
      pkgs.tdesktop
      pkgs.home-manager
      tg
      telegram-cli
      slack
      lazygit
    ];
  };
}