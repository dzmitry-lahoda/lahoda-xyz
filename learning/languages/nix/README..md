
## evaluate

```sh
tee tmp.nix <<EOF
    rec {
        a = 1;
        b = "c";
        c = {
            lazy = "42";
        };
    }
EOF
# strict is needed to force evaluation of all nodes so they can be made json from lazy something
nix-instantiate --eval --strict --json tmp.nix
```

## flake

```sh
# shallow flake evaluation
nix flake show --allow-import-from-derivation  --show-trace --fallback --debug --print-build-logs --keep-failed
```

```sh
# deep flake evaluation
nix flake check --no-build --keep-going --allow-import-from-derivation  --show-trace --no-update-lock-file --fallback --debug --print-build-logs --keep-failed
```

```sh
# shell with package
nix shell --command hello
```
