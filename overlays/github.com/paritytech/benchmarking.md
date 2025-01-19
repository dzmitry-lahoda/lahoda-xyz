# How to benchmark

When using 3rd party pallet, use their measured weights from their runtimes as initial approximation.

When there are no benchmarks, write down big enough weights, dozen times more than expecting.
If hard to guess, measure locally. 

Make you node machines beefy. So all weights as small as they can be still fit.

And finally do the right things to bench on target hardware.
Follow Parity official guidelines and explanation on why and how to bench.

To safe wasted codespace and speed up checking benchamrks, make sure these run with pallet tests okey.

