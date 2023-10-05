#!/usr/bin/env nix-shell
#!nix-shell --pure -i runghc -p "haskellPackages.ghcWithPackages (pkgs: [ ])"

data Partition = Partition {
    id :: String,
    start :: Integer,
    size:: Integer
} deriving (Show, Eq)

-- Logical disk
data PartitionTable = PartitionTable {
    partitions:: [Partition],
    device_id :: String
} deriving (Show, Eq) 

-- crate and manipulate partition tables
fdisk device = PartitionTable [] device


-- list block
lsblk = 
    error "list block devices"

-- https://man7.org/linux/man-pages/man1/dmesg.1.html
dmesg =
    error "show kernel rink buffer log"

main = do
    putStrLn "Hello, World!"