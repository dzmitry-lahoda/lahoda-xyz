({ pkgs, lib, config, options, specialArgs, modulesPath }: {
  imports = [ "${toString modulesPath}/virtualisation/virtualbox-image.nix" ];
  #imports = [ "${toString modulesPath}/installer/virtualbox-demo.nix" ];
  #pname = "ad";
  # boot.isContainer = false;
  # #boot.loader.systemd-boot.enable = true;
  virtualbox.memorySize = 1024;
  # virtualbox.vmDerivationName = "gods1";
  # virtualbox.vmName = "gods2";
  #virtualbox.vmFileName = "gods3";
  # virtualbox.extraDisk = 
  #   {
  #     label = "p2p";
  #     mountPoint = "/home/demo/p2p";
  #     size = 100 * 1024;
  #   }
  # ;  

  boot.growPartition = true;
  boot.loader.grub.device = "/dev/sda";

  boot.loader.grub.fsIdentifier = "provided";
  environment.systemPackages = [ pkgs.hello ];
  system.configurationRevision = "qwe" ; #lib.mkIf (self ? rev) self.rev;
  networking.firewall.allowedTCPPorts = [ 80 ];
  fileSystems = {
    "/" = {
      device = "/dev/disk/by-label/nixos";
      autoResize = true;
      fsType = "ext4";
    };
    "/boot" = {
      device = "/dev/disk/by-label/boot";
      autoResize = false;
      fsType = "fat32";
    };
  };
  services.httpd = {
    adminAddr = "root@localhost";
    enable = true;
  };
})
