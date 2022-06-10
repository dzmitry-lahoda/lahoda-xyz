
curl https://github.com/prometheus/prometheus/releases/download/v2.2.1/prometheus-2.2.1.windows-amd64.tar.gz -O -L
tar xvfz .\prometheus-2.2.1.windows-amd64\
copy .\prometheus\prometheus.yml .\prometheus-2.2.1.windows-amd64\
./prometheus-2.2.1.windows-amd64/prometheus --config.file=prometheus.yml

curl http://localhost:9090/metrics

curl http://localhost:9090/graph

curl http://localhost:9090/targets

