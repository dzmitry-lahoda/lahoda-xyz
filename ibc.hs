#!/usr/bin/env nix-shell
#!nix-shell --pure -i runghc -p "haskellPackages.ghcWithPackages (pkgs: [ pkgs.turtle ])"

data ConsensusState = ConsensusState { height:: Integer } deriving (Show)

data Connection = Connection { id :: String } deriving (Show)

data Port = Port Integer

-- authenticateCapability
-- hash

-- Identifier, get, set, delete, getCurrentHeight, 

data ChannelState = ChannelState String
data ChannelOrder = Ordered | Unordered | OrderedAllowTimeout
data Identifier = Identifier String
data ChannelEnd = ChannelEnd ChannelState Identifier Identifier [Identifier] String

main = do
  putStrLn "Hello world from a distributable Haskell script!"