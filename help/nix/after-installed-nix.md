Provide description of `nix` enviroment by running:

```shell
uname -a
env
nix --version
nix show-config
```

And run for builds
```shell
nix build FLAKE#PACKAGE_NAME --show-trace --debug --print-build-logs
nix flake info FLAKE 
```

