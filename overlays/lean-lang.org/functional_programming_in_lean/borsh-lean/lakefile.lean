import Lake
open Lake DSL

package "borsh-lean" where
  -- add package configuration options here

lean_lib «BorshLean» where
  -- add library configuration options here

@[default_target]
lean_exe "borsh-lean" where
  root := `Main
