-- TODO: make next but for borsh here
-- An HTTP request begins with an identification of a HTTP method,
-- such as GET or POST, along with a URI and an HTTP version.
-- Define an inductive type that represents an interesting subset of the HTTP methods,
-- and a structure that represents HTTP responses.
-- Responses should have a ToString instance that makes it possible to debug them.
-- Use a type class to associate different IO actions with each HTTP method,
-- and write a test harness as an IO action that calls each method and prints the result.



import BorshLean

def main : IO Unit :=
  IO.println s!"Hello, {hello}!"
