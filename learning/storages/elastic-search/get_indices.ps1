$queryString = "*/_aliases"

$node = "http://localhost:9200"

Invoke-WebRequest -Uri "$node/_cluster/health"

$indecesResponse = Invoke-WebRequest -Uri "$node/*"
$indeces = ConvertFrom-Json $indecesResponse.Content
($indecesResponse.Content) | Out-File -FilePath ./indeces.json