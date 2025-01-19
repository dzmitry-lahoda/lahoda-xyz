import cvxpy as cp
import numpy as np

x  = cp.Variable()
y = cp.Variable()

constraints = [x + y == 1, 
               x - y >= 1]

objective = cp.Minimize((x-y)**2)

problem = cp.Problem(objective, constraints)    

problem.solve()

print(problem.status)
print("optimal value", problem.value)
print("optimal variables", x.value,y.value)