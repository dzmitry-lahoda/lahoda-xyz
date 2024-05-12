# Runtime Testing

Runtime testing involves running parachain code to ensure quality.
Unlike type system or static analysis (excluding basic linting).
Here is hierarchy of testing you may find in this parachain

| Name          | Runtime   | Warp |Node(RPC)| Start from real state | Serde |
| ------------- | --------- | ---- |_| --------------------- | ----- | |
| Unit          | no        | yes  |no| no                    | no    |
| Runtime Unit  | mock      | yes  |no| yes                   |
| Property test | mock      | yes  |no|
| Benchmark     | mock/real | yes  |mock|
| Visualization | no/mock   | yes  |no|
| Simulation    | real      | yes  |no| yes                   |
| Simnode       | real      | yes  |no| yes                   |
| Local Relay   | real      | no   |real|                       |
| Deployment    | real      | no   |real|                       |
| Chopstick    | real      | no   | partially real |                      |
