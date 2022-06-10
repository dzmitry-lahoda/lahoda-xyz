# learning-prometheus


## rate

foo_bar_

0.1s 1
0.2s 2
0.3s 3
...
1.0s 10

rate(foo_bar) - 5


```pwsh

```
./prometheus --config.file=prometheus.yml

http://localhost:9090/metrics

git clone https://github.com/prometheus/client_golang.git

cd .\client_golang\examples\random\
choco install golang

go get -d
go build

./random -listen-address=:8080 &
./random -listen-address=:8081 &
./random -listen-address=:8082 &

# Queries

http://localhost:9090/graph?g0.range_input=1h&g0.expr=prometheus_target_interval_length_seconds&g0.tab=1

prometheus_target_interval_length_seconds{quantile="0.99"}

count(prometheus_target_interval_length_seconds)

avg(rate(rpc_durations_histogram_seconds_count[5m])) by (job) 

# Meta queries

topk(10, count by (__name__) ({__name__=~".+"}))