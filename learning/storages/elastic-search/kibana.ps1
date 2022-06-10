# runs kibana for local elastic or elastic port forwared from kubernetes
docker run --env ELASTICSEARCH_HOSTS="http://docker.for.win.localhost:9200" --env SERVER_HOST="0.0.0.0" --publish 5601:5601 docker.elastic.co/kibana/kibana:6.8.3
curl http://localhost:5601;

                  