#!/usr/bin/env nix-shell
#!nix-shell --pure -i runghc -p "haskellPackages.ghcWithPackages (pkgs: [ pkgs.turtle ])"

data KeysAsOnKeyboard = Ctrl | Alt | Shift | Super deriving (Show, Read, Eq)



data AndSameTime = 
    | { a :: KeysAsOnKeyboard, b :: KeysAsOnKeyboard} 

data OpenKeyboardShortCuts = Ctrl + K + S

data CreateNewFile = Ctrl + Alt + Super + N

FormatDocument = [KeysAsOnKeyboard::Ctrl,  KeysAsOnKeyboard::Shift, KeysAsOnKeyboard::I]

main = do
  
  putStrLn "Hello world from a distributable Haskell script!"