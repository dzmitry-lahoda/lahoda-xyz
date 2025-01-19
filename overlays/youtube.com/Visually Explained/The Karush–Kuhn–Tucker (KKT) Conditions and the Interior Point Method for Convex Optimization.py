import cvxpy as cp
import numpy as np
import math

x  = cp.Variable()
y = cp.Variable()

# using it directly just fails with DCP rules violation
# constraints = [x*x + y*y - 1 <= 0]
# objective = cp.Minimize(x+y)


# whatever with round does not work <=
def round(x,y):
    min(x*x + y*y - 1, 0)
    
objective = cp.Minimize(x+y + x*x + y*y - 1)

problem = cp.Problem(objective)    

problem.solve()

print(problem.status)
print("optimal value", problem.value)
print("optimal variables", x.value,y.value)