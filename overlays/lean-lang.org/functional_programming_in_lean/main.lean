
def tt(act: IO Unit) : IO Unit := do
  act
  act
  pure ()

def nTimes (action : IO Unit) : Nat → IO Unit
  | 0 => pure ()
  | n + 1 => do
    action
    nTimes action n

def main: IO Unit := do
  let stdin ← IO.getStdin
  let input ← stdin.getLine
  let stdout ← IO.getStdout
  nTimes (stdout.putStrLn "aaa") 2
  tt (let sa :=  stdin.getLine; pure ())
  let line := input.dropRightWhile Char.isWhitespace
  stdout.putStrLn s!"Hello, world {line}!"
