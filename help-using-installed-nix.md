Provide description of `nix` enviroment by running:

```shell
uname -a
env
nix --version
nix show-config
```

And run for builds
```shell
nix build .#PACKAGE_NAME --show-trace --fallback --debug --print-build-logs
```
