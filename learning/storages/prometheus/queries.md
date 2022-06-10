

```promql
metrics_tests_average_five_BC{C="c"} > 0 and
resets(metrics_tests_average_five_BC{C="c"}[6m]) == 0 and
on (B) rate(metrics_tests_requests_AB_total{A="a"}[6m]) > 0
```

```promql
metrics_tests_average_five_BC{C="c"} > 0
AND
avg_over_time(metrics_tests_average_five_BC{C="c"}[6m]) > 0
AND
resets(metrics_tests_average_five_BC{C="c"}[6m]) == 0
```

```promql
sum by (B, C) (metrics_tests_average_five_BC{C="c"})
/ on (B) group_left
sum by (A, B) (rate(metrics_tests_requests_AB_total{A="a"}[30s]))
```

```promql
sum (metrics_tests_average_five)
/ on group_left
sum rate(metrics_tests_requests_total[1m])
```

```promql
changes(metrics_tests_15_seconds_total[1m])
```

```promql
deriv(metrics_tests_15_seconds_total[1m])
```
