

# tree

Fully connected and has no cycles.

# spanning Tree

Given fully connected undirected graph, spanning tree is subgraph of that graph that connects all verices

## minimum

Spanning tree which sum of weights is minimal.


## Kruskal minimum spanning tree

1. Sort by edge weight from small to large
2. Find next node with which belongs to tree and not yet unified. Unify.
2. NOTE: do not include unified as it will make cycle.
3. Terminate when all edges are processed or vertices unified. 