


- Elastic as audit, raw fields, not search oldest latest queries, auto index, tophists 1, parse tree.


Goal
----

Can we use `from`/`size` without `scroll` for deep pagination.

Evidence:
---

No sorting ("sort":"_doc" or DocumentIndexOrder) is slower on approx. 30% then without this parameter on 3 shards and 1 replica index on `from` approx 9m and `size` 50.

`From` 9m took 9s ("took": 9409) for elastic to calculate

`From` 900k took 0.9s, so time grows linear with `from`.

Conlusion
---

We can use batching via `from`/`size` without scroll for 10m items.

Out of scope
---

Assuming that without `_doc` sort option we get all files eventually as with it.

Scroll not used yet. We found API (via NEST) changed for `scroll` 1.7 to 2.x and we have tune `scroll` lifetime to get robust report generation. We decided to be safe and not use it yet.

Used calls and queries
---

https://es-aaudittrail-qeda.int.aas.com/audit_qedshared_54ca338544841ee0e10080000acd0255_f4bf4c8eb37e4280b66983f7d3c32627/_settings

{"audit_qedshared_54ca338544841ee0e10080000acd0255_f4bf4c8eb37e4280b66983f7d3c32627":{"settings":{"index":{"creation_date":"1458625165437","number_of_shards":"3","number_of_replicas":"1","version":{"created":"1070399"},"uuid":"2omwjJFtTI6tsNrjSxJzbA"}}}}



https://es-aaudittrail-qeda.int.aas.com/audit_qedshared_54ca338544841ee0e10080000acd0255_f4bf4c8eb37e4280b66983f7d3c32627/DocumentField/

{
    "from" : 9990000, "size" : 50
}

{
    "from" : 990000, "size" : 50, "sort" : "_doc"
}

{
    "from" : 990000, "size" : 5000,
}


{
  "from": 990000,
  "size": 5000,
  "_source": {
    "include": [
      "documentId",
      "operationtimestamp",
      "fileSize",
      "custodian"
    ]
  },
  "query": {
    "filtered": {
      "filter": {
        "bool": {
          "must": [
            {
              "term": {
                "matterId.raw": "f4bf4c8eb37e4280b66983f7d3c32627"
              }
            },
            {
              "terms": {
                "action.raw": [
                  "Create"
                ]
              }
            }
          ]
        }
      }
    }
  }
}

NotGoodElasticAudit
-----


We put MatterId into all things, but we already know matter by index, except for some cases like matter itself or user.

I want to count number of documents by state. I need to group by state latest version of documents and count. Elastic does not have API to calculate it on, unline SQL db. 

I tried top_hits and aggs. More I cannot joun other entity to count size in same pass like in SQL.

Thise relates to any counds and sizes. If audit can be more then once for specific field and in big date range (and severa time in some range date) we has no generic way to limit time range.

There is no easy way to write SQL like '%ab%' wild card. 

NEST api is very complex for audit and reporting.


Cannot get all latest but not deleted.
