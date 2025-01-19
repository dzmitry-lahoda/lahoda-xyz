# SNARK

$$

\\ NARK - Non interactive argument of knowledge
\\ C(x, w) \rightarrow F
\\ x \in F^n \text { is public statement }
\\ w \in F^m \text { is secret witness }
\\ C \text { is arithmetic circuit}
\\ F \text { is finite field}
\\ S \text { is preprocessor function}
\\ S(C, r) \rightarrow (prover\_parameters, verifier\_parameters) = (pp, pv)
\\ r \text { is random}
\\ Prover(prover\_parameters, x, w) -> \Pi \text { is proof so that} C(x, w) = 0
\\ Verifer(verifier\_parameters, x, \Pi) -> \{0,1\}
\\ \text{Complete } \forall x, w \  C(x,w) = 0 => Pr(Verifier(verifier\_parameters, x, Prover(prover\_parameters, x, w)) = 1) = 1
\\ \text{Knowledge sound: } Verifier(..)=1 \rightarrow Prover \text{ "knows" } w s.t. C(x,w)=0  
\\ \text{optional Zero Knowledge} (C, x, prover\_parameters, verifier\_parameters, \Pi) \text {does not reveal } w
\\ \text{is trivial}, C(x, w) = C (x, \Pi)
\\ KT is knowledge sound
\\ T - time
\\ T_{poly} - polynomial time
\\ A - adversary \ tries \  to \ prove \ x \ without \  knowledge \ of \ w 
\\ \epsilon - neglegible small number
\\ E - extractor
\\ r - random
\\ m - message
\\ d - degree
$$

$$
\\ (S,P,V) is KT for C if for every T_{poly} aversary A = (A_0, A_1),
\\ (C, x, st) <- A_0(gp)
\\ \Pi <- A_1(pp, x, st)
\\ Pr[V(vp, x, pi)] = 1 > \epsilon
\\ if true, =>
\\ \exist E such that
\\ w <- E(gp, C, x)
\\ Pr[C(x, w) = 0] > \epsilon
$$


## Succinct

$$
\\ len(\Pi) = sublinear(|w|)
\\ time(V) = O_{lambda}(|x|, sublinear(C))
$$

### Strongly

$$
\\ len(\pi) = O_{lambda}(log(C))
\\ time(V) = O_{lambda}(|x|, log(C))
$$

S compresses C, so that `log` is possible.

## Setups

### Trusted Setup per circuit

$r$ kept secret from $Prover$


### Trusted Universal 

$$
\\ S = ( S_{init}, S_{index})
\\ gp - \text{global parameters}
\\ S_{init}(\lambda,r) \rightarrow gp
\\ S_{index}(gp, C) \rightarrow parameters
$$

### Transparent (non trusted)


| name         | prover time | size of proof $Pi$ | verifier T | setup | post quantum |
|--------------|-------------|--------------------|------------|-------|--------------|
| bulletproofs |             |                    |            |       | no           |
| STARK        | O(\|C\|)    | $O({log}^2 \|C\|)$ |            |       | yes          |
|              |                     |            |       |              |


# Knowledge soundess

if V accepts then P knows w such that C(x,w) = 0.

P knows w if w can be extracted from P


## Functional Commitment Scheme

Cryptographic object


## Compatible interactrive oracle proof (IOP)

Information theoric object


$$
commit(m, r) -> commmitment
verify(m, commitment, r) -> {accept, reject}
binding property is bijection (m,r) <-> commitment
hidning means commitment does not revelas anything about (m,r)

\\ H: M x R -> T is stadard consuctions
\\ commitment = H(m,r)
\\ verify(m, commitment, r) = accept if H(m,r)
\\ F = {f: X - > Y}
$$

## Functional commitment

Verirfier does not knows commited function f.

  tion                                                             |
|-----------------------|----------------------------------------------------------------------|
| univariate polynomial | $f(X) \ in \ F^{(<=d)}_p [X]$                                        |
| multilinear           | $f in F^{(<=1)}_p [X_1, .. X_k]$                                     |
| vector                | $for \  \vec{u} \in F^d_p \ commit \  to \ f_{\vec{u}}(i) = u_i  $   |
| inner product         | $\vec{u} \in F^d_p$ open $f_{\vec{u}}(\vec{v}) = (\vec{u}, \vec{v})$ |


Equality Test protocol 

Fiat-Shamir transfpor makes interactive public coint protocol to be non interactive,
via making randomes of verifier public