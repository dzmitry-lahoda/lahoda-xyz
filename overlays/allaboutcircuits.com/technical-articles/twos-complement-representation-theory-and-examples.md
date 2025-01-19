$$
\begin{align}
S = a - b
\\ S = a + M - b - M
\\ k = bits(M) = bits(a) + 1
\\ M = 2^k
\\ B = M - b
\\ (M - b) mod \text k + b = M
\\ B = (M - 1) - b + 1
\\ B = (M - 1) \oplus b + 1  (4)
\\ B = \neg b + 1
\\ S = (a + B)  \wedge  (\neg M)
\end{align}
$$
