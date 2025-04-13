
If we compound each unit of time, we get:

$ (1 + 1/N)^N = \e$ where $N = infinity $

If we have  $ lim_{n->infinity} (1 + r / n ) ^ (n * t)   $, 
where $r$ is real number amid [0..1) the  
and $t$ is constant positive integer
and $n = infinity$,

if we replace $x = n / r$, we get
$ lim_x (1 + 1 / x ) ^ (x * r * t)$,

and move $r * t$ to the exponent, we get
$ (lim_{x->infinity} (1 + 1 / x ) ^ x) ^ (r * t) $,
and we know that $lim_x (1 + 1 / x ) ^ x = e$,

so we get $e ^ (r * t)$.

So we have `continuous compound`.