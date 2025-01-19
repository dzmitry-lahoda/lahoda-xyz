import cvxpy as cp
import numpy as np

x  = cp.Variable()
y = cp.Variable()
# non solvable
constraints = [y >= 2*x + 1, 
               y <= 2*x - 5,
               x >= 1]

objective = cp.Minimize(x)

problem = cp.Problem(objective, constraints)    

problem.solve()

print(problem.status)


constraints = [y >= x - 8, 
               y <= 5 - x]

objective = cp.Maximize(x)

problem = cp.Problem(objective, constraints)    

problem.solve()

print(problem.status)
print(problem.value)