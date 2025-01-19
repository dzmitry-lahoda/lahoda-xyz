nix build .#nixosConfgurations.myhost.config.system.build.toplevel
nix build .#virtualbox
nix eval .#crazy