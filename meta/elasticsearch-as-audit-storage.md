Use case
===
Elastic is used for auditing and reporting, answer what is going on historically, and for search.

Master nodes knows where data(metadata) is and gets data from several Data Nodes.
-----

Using ES to audit changes of data stored in DB has its complexities and design desicion influcnecint performance of system as all and using ES data for reporting. 

Terminology:
---
DB - relational ACID SQL database.

ES - ElasticSearch.

Domain
---
Let pretend I store documents with some properties which can be updated.

Given created new row in DB  with next data in `documents` table:
```
id = 1, size = "1", length = "2"
```

Naive behavior:
---



Then ES grabs this data from DB and writes
```
_type = "document", _id = 1, id = 1, size = "1", length = "2"
```

When DB updates thousands of rows in bulk with:
```
length = "3"
```

1. Copy and paste whole data from DB
Then ES grabs all data from DB(including not changed) and writes:
```
_type = "document", _id = 1, id = 1, size = "1", length = "3"
```

2. Copy and paste only changes from DB
With naive implementaitons there is no way to grab only `length = "3"` and log because audit will write:
```
_type = "document", _id = 2, length = "3"
```
- i.e. we append new row but data was `lost` and need to look previous record for missed data.

Improved:
---
When DB updates thousands of rows in bulk with:
b = "3"

a. Option a:
Then ES grabs only updated data from DB  and writes:
type = "mytype" b = "3"

Some other code (report generator, workflow task or ES plugin) grab all data from ES and creates diff of
type = "mytype" a = "1" b = "2"
type = "mytype" b = "3"

merged into:
type = "mytype_diff" a = "1" b = "3" diff_time = "2015-10-11T11:11:11.123123"

This reproach handles real updates of a = null (need to check that ES differens no value and null value storage and query [1]) :
type = "mytype" a = "1" b = "2"
type = "mytype" b = "3"
type = "mytype" a = null

then diff:
type = "mytype_diff" a = null b = "3" diff_time = "2015-10-11T11:11:11.123123"

New diff can use previous diff

b. Then ES grabs only updated data from DB  and uses ES to take previous unchanged data to write all values:
type = "mytype" a="1" b = "3"

more if previous values was
type = "mytype" a="1" b = "3"
can skip value somehow to audit (mark as false update)

Get:
---

**Pros:**

1. Audit produces less stress on relational DB and eats less resourses.
a. less data stored in ElasticSearch during audit 
2. queries are faster (do not need to use `top_hits` everywhere).  If audit logs are not too deep then better performance.
3. If A part of progam has access to some data related to object but other not - then we do not need to propagate data into other part only for audit.
 no call at all
 
**Cons:**

1. Need know more about time to undrestand activities done which change DB to identify specific data changes. 
a. 1. Single document is not self describing by default, need to make diff.

Analytics algorithm for historical data
---

- show changes for specific period
- show daily totals for specific period

1. Algorithm runs from start of some concrete idex type to end and dumps all low level data doing deltas on the go. Cannot use sammries as need to hold back of already seend entites. In worth case all entities created retained up to end of last day of algorithm.

2. We get all data as created and get latest for specific date. We may still use sumammries precalcualted, but to get latesr we still need to take all creaed and match with latest. Needs join on many ids sent over the wire.

3. Get summaries. Get latest day. Get all last updates to today. Calculate what was changed. Diff with previous summary. Store new sammary.


Event sourcing
---
3. Event sourcing - each audit should append activity and context of audit what was. I will provide case which is unresolbabe in current. Given I do not see any documentaion on how audit should be done I accept that state of audite I started to see in DB is correct - and it was not build around event/acitivy/context auditing. Only recenly we started to indrodice such kind of thins to fix issues. I see more issues with current audit as is done today.

Queries:
---

I want to count number of documents with specific field set to mystate = "State1". I need to group by `mystate` latest version of documents and count. ES does not have API to calculate it on, unlike SQL db. 

I tried `top_hits` and aggs. More I cannot join other entity to count size in same pass like in SQL.

In case we store data as audits events in time and store almost same data several times same id, but different `_id`. Elastic has no aggregation we can use to aggregate only latest. We have to get all latest data and summarize (read Application side join).


This relates to any counts and sizes. If audit can be more then once for specific field and in big date range (and severa times in some range date) we has no generic way to limit time range.

SQL DISTINC analog `cardinality` is approximate.

If I want ES to aggregate and sort data by timestamp and retrun data source - I cannot write such quere. `top_hits` support sorting and sourcing, but is very slow and fails when big SIZE number passed. Pure aggregation does not returns sorurce under aggregation element.

There is no easy way to write DB like '%\ab\%' wild card.  NEST API does has bug for this when using fluent. Need to escape `\` with `\\\\` in JS, `\\\\\\\\` in C# code.

NEST api is very complex for audit and quering for reporing. Need to build custom library above with custom querey provider.

Having audit stage field like {New, Change, Delete, View}, cannot get all latest but not deleted with query. Gets all data to quering machine and filter on it.

Given this ES should have several replicats connected by good network to data consuming client which mitigates bad support queries in ES. Ideally it should be ES pluging providing data out of elastic in binary form.


Code
---
```
        class doc
        {
            public string Id;
        }
        static doc[] ad = new[]
            {
                new doc{ Id = "1"},
                new doc{ Id = "2"},
                new doc{ Id = "2"},
                new doc{ Id = "2"},
                new doc{ Id = "3"},
                new doc{ Id = "3"},
            };

        static void Main(string[] args)
        {
            Do<doc>(ad, x => x.Id);
        }

        public static void Do<T>(IEnumerable<T> tt, Expression<Func<T, object>> a) where T:class
        {
            Func<T,object> asd = a.Compile();
            var qwe = tt.GroupBy(asd);
            var zxc = qwe.ToDictionary(x => x.Key, x => x.AsEnumerable()).ToArray();
        }		
		```
Notes
===

ES is suitable for queries for audit better staring 2.X version. Better to work with >= 2.3 [2].

Usefill settings
===

https://www.elastic.co/guide/en/elasticsearch/reference/current/mapping-all-field.html#disabling-all-field

https://www.elastic.co/guide/en/elasticsearch/reference/current/doc-values.html

[1]: https://www.elastic.co/guide/en/elasticsearch/reference/master/query-dsl-exists-query.html
[2]: https://www.elastic.co/blog/better-query-execution-coming-elasticsearch-2-0


How would you query ElasticSearch when it is used as NoSQL data storage?


1. JSON

2. NEST

3. Our case of audit.

4. LINQ to NEST. Screenshots of before after.

5. Custom LINQ for problem. Logs?

6. Links to LINQ proposals.

