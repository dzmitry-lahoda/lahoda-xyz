# Runtime Upgrades and Versioning

This diagram shows our compile time and runtime dependencies to guide the upgrade process.

We have to follow Substrate/Cumulus/Polkadot version and relevant ORML closely.

Runtime WASM version of Picasso is bumped automatically. Compile time version of TheParachain to be defined in release process document.


## Guidances

Check if any chain updated to version you want to update. Until you blocked by lack of features on current version, consider no upgrade.
Suggest chain to check is Acala if you depend on ORML.

Check versions:
- Substrate
- Polkadot
- Cumulus
- ORML
- Centauri (uses ORML and Simnode)
 

And if using Rust remote clients
- Subxt
- Jsonspree 

And if using for testing:
- `sc-simnode`(polytope-labs).
- - xcm-simulator (ACala)

In case of dependency on Smolldot, check its version too.

All should be updated to same version at best. Or as close as possible.

Do not update your chain directly. Put patch your forks of all dependencies with relevant commit and branch.
Because anyway at some point you will have to do backport, hotfix or patch.

On update, use commit hash instead of branch name as more determinism dependency. 
One may break branch by commits into, but not commit. 


```plantuml
@startuml

frame "GitHub" as github {
    folder "Substrate v0.9.*" as substrate
    folder "Cumulus v0.9.*" as cumulus
    folder "Polkadot v0.9.*" as polkadot
    folder "ORML v0.9.*" as orml 
    folder "TheParachain v.a.b" as TheParachain 
    folder "Subxt v.a.b" as subxt 
    folder "Jsonspree v.a.b" as jsonspree 
    folder "Simnode v.a.b" as simnode 
}

cloud "Shared security (and versioning)" {
    node "Picasso Node tested with v0.9.23" as picasso {
        artifact "Upgradable runtime (WASM) Picasso tested with v0.9.24" as picasso_runtime {
            component "Runtime Configuration" as runtime_configuration {
                    component "Pallet A" as pallet_a
                    component "Pallet B" as pallet_b
            }
        }
    }

    node "Kusama Node v0.9.33" as kusama {
        artifact "Upgradable runtime (WASM) Kusama v0.9.33" as kusama_runtime
    } 
}

picasso -.-> kusama : Upgraded Picasso runtime
picasso -.-> kusama : XCM
picasso -.-> kusama : Parachain protocol messages
pallet_a -0)- runtime_configuration
pallet_b -0)- runtime_configuration
runtime_configuration -0)- picasso_runtime

cumulus --^ substrate
polkadot --^ cumulus
polkadot --^ substrate
orml --^ substrate
orml --^ cumulus
orml --^ polkadot
TheParachain --^ substrate
TheParachain --^ cumulus
TheParachain --^ polkadot
TheParachain --^ orml

picasso --:|> TheParachain: Build from
kusama --:|> polkadot: Build from


@enduml
```