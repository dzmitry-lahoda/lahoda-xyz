# How it works?

Overestimate path length from origin node to target node.

And iteratively relax after finding shorter paths.

Also can track previous of of each node so can return path.

In the end if adding to any vertex new weight path becomes shorter, means negative cycle exists