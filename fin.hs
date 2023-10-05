#!/usr/bin/env nix-shell
#!nix-shell --pure -i runghc -p "haskellPackages.ghcWithPackages (pkgs: [ pkgs.turtle ])"

-- Buy with maximal `price`. Maximum I am ready to pay, either absolute about or relative
data Bid a = Bid { price :: a } deriving (Show)
-- Sell/Ask with minimal `price`
data Offer a = Offer { price :: a } deriving (Show)

is_match offer bid = offer.price <= bid.price 

main = do
  
  putStrLn "Hello world from a distributable Haskell script!"