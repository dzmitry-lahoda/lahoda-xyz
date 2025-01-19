

Is general purpose strict pure functional(with currying) programming language with dependant types.

Types are expressions.

Allows to overload number literals to allow represent different branches of mathematics.

Definitions which are unfolded are called `reducible`.

Stuctures updated with lenses and support dot notation.

Has inductive(recursive) and sum(tagged enumeration) types.

Recursion can be only well-founded, non well founded errors.
And requires manual proof of termination.

Lean has special notation for lists, like `[..]` and `::`.

Can use some symbols in identifiers.

Lean does not have throwing exception as part of language.

`do` is used to model interaction with the world.

Lean evaluates `IO` function, and returns them from main for execution.

Class is implemented by instances. Instances are stuctures. 
To define class instance requried, use `[...]` syntax.
Things within square brackets are called `instance implciits`
.
