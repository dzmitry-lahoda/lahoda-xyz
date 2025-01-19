
import cvxpy as cp
import numpy as np
m = 4
n = 2
np.random.seed(1)
A = np.random.randn(m, n)
b = np.random.randn(m)
x = cp.Variable(n)
objective = cp.Minimize(cp.sum_squares(A @ x - b))
problem = cp.Problem(objective, [])
result = problem.solve()

objective = cp.Minimize(cp.sum_squares(A @ x - b))
constraints = [] # [ 0 <= x, x <= 1]
problem = cp.Problem(objective, constraints)
result = problem.solve()


print(A)
print(b)
print("status", problem.status)
print("optimal value", problem.value)
print("x == " , x.value)
print(A@x.value - b)

