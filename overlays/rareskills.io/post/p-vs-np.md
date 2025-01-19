

# P vs NP vs PSPACE, time vs space, solve vs verify.



|          | Solve time | Verify time | Solve space | Verify Space | Examples |
|----------|------------|-------------|-------------|--------------|----------|
| P        | <=poly     | <=poly      |             |              |          |
| NP       | any        | <=poly      |             |              |          |
| PSPACE   | any        | any         | <=poly      | <=poly       |          |
| EXPSPACE | ∅          | ∅           | any         | any          |          |

Exponential space leads to exponential time.

P problems are easy to solver and verify in O(n^c) time.

NP problems are not easy to solve O(c^n) time, c > 1, "exponential time".

Witness is proof that solution was solver correctly.


PSPACE problems take ETIME to solve and verify, so not ESPACE to solve.

We can construct boolean formula to verify solution.

Boolean formula constuctable in polynomial time is useful.

ZKP cannot verify problems which cannot be verified in polynomial time.

All problelms which can be P veirifed can be converted into boolean formula, called circuit.

Creating zero knowledge proof for a problem consist of transalting problem into circuit.

