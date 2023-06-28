#!/usr/bin/env nix-shell
#!nix-shell --pure -i runghc -p "haskellPackages.ghcWithPackages (pkgs: [ pkgs.turtle ])"

-- Buy
data Bid a = Bid { maximum_amount :: a } deriving (Show)
-- Sell/Ask
data Offer a = Offer { man_amount :: a } deriving (Show)

main = do
  putStrLn "Hello world from a distributable Haskell script!"