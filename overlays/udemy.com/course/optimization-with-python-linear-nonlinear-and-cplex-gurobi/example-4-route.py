import cvxpy as cp
import numpy as np

points = 5 # A P1 P2 P3 B

max = 1000000000000000

D  = np.full((points, points), max)

D[0,1] = 2
D[0,2] = 7
D[1,2] = 10
D[2,1] = 10
D[1,4] = 30
D[2,3] = 8
D[3,4] = 5

X = cp.Variable((points, points), boolean=True)

expression = cp.multiply(X, D)

print(expression)

objective = cp.Minimize(cp.sum(expression))
print(objective)

constrains = [
     sum (X[0,:]) == 1,
     sum(X[:,4]) == 1,
     ]

for i in range(points):
        constrains.append(sum(X[i,:]) == sum(X[:,i]))   

problem = cp.Problem(objective, constrains)

result = problem.solve()

print(problem)
print("problem.status: ", problem.status)
print("result: ", result)
print("problem.value:",  problem.value)
print(X.value)