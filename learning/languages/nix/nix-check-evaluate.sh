# `compiles` nix
nix flake metadata && \
nix flake show --allow-import-from-derivation  --show-trace --fallback --debug --print-build-logs --keep-failed --option sandbox relaxed && \
nix flake check --no-build --keep-going --allow-import-from-derivation  --show-trace --no-update-lock-file --fallback --debug --print-build-logs --keep-failed --impure --option sandbox relaxed