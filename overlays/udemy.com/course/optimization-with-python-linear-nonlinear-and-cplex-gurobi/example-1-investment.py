import cvxpy as cp

capital = cp.Variable(3)

total = 100000

constrains = [
    sum(capital) == 100000,
    capital[1] <= 0.2*100000,
    capital[2] <= 0.1*100000,
    ]


objective = cp.Maximize(0.05*capital[0] + 0.1*capital[1] + 0.12*capital[2])

problem = cp.Problem(objective, constrains)

result = problem.solve()

print(problem.status)
print(problem.value)
print(problem)
print(capital.value)
print(constrains[0].dual_value)
print(constrains[1].dual_value)
print(constrains[2].dual_value)
print(constrains)