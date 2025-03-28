/* world still likes pdfs... */
{ pkgs ? import <nixpkgs> {} }: 
with pkgs;
[
  qpdf
]

