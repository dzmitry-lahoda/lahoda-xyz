with import <nixpkgs> {};

mkShell {
  name  = "dotnet-env";
  packages = [
    (with dotnetCorePackages; combinePackages [
      dotnet-sdk_6
    ]) 
  ];
}