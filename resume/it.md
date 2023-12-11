# About

Dzmitry is a seasoned software developer with 20 years of experience, specializing in blockchain, underlying infrastucture and protocols.

Before crypto he coded integrations, client-servers, actor system, HPC, cloud, desktop, drivers, custom software frameworks, reusable libraries, real-time networks, basic applied statistics in several domains.

Computer user since 1993, write programs since 2004, industrial tooling and practices 2007, open-source since 2009, work in companies since 2010.
Did generalized machine learning algorithms in 2010.

Dzmitry currently trying to be better at mathematics, cryptography, and compilers. 

Currently he resides in Portugal.

## Profiles

[GitHub](https://github.com/dzmitry-lahoda) [LinkedIn](https://www.linkedin.com/in/dzmitrylahoda) [HackerRank Certificate](https://www.hackerrank.com/certificates/4f1dfdbd7772) [Brilliant(many math courses)](https://brilliant.org/profile/dzmitry-rrotsd/about/)

## Toolbox

### Technical knowledge and experience

Applied knowledge of compute(embedded, cloud, blockchain, p2p, virtualization, distributed, isolation, cryptography).

Applied knowledge of storages(graph, relational, key values, blobs, documents, hierarchical).

Applied knowledge of languages and type systems (object oriented, functional, lazy, dynamic, static, strict, structural, nominal, non turing complete, DSL, modelling, documentation, query).

Applied knowledge of many engineering, infrastucture, design, construction, architecture, quality related processes and patterns, mechanism designs.

### Coding

Mostly `Rust` last years. I do `Solidity, Go, Python, Haskell` too.

Used Object Pascal, Matlab, C#/F# a lot before.

### Environment

Git, Nix, VS Code, Helix(editor), AI models, Markdown.

## Work

### 2021.03-now. Web3.0, blockchain, p2p. Software Engineer. Contracts works and full time works

Coding, audit, research, monitoring, building and deployment.

For more than 1 year working in [Composable](https://github.com/ComposableFi/composable),
mostly on cross-chain interoperability.

- Ethereum: Hosting local PoS devnet, wrote IBC cross chain call contract, wrote manual cross chain interpreter parser, reading a lot of code and specs for porting to other chains. 
- On chain: DeFi. lending, AMM, NFT, crowd funding, IDO, staking/rewards, vote escrowed, governance, DAO, identities
- Infrastructure: local devnets, forks, relayers, indexers, tools
- Virtual machines: WASM, eBPF
- P2P: chats
- Contracts and programs for: Solana, Polkadot, Ethereum, [Fluence](https://fluence.network/), Cosmos, CosmWasm.

### 2021.05-2021.11.  [The Workplace Metaverse](https://www.sowork.com/). Software Engineer. Contract works

Did several ECS based features into 2D metaverse engine like character shadow, sit down, movements fixes.
Prototyped and test 1000+ user channel based scalable server for world consisting of chunks, with WASM client.

### 2020.10-2021.03, [Elemy](https://www.elemy.com/). Backend Software Engineer. Healthcare. Contract works

Did Login, questionnaires, documents signing, marketing backend flows on CQRS/ES. 
Built templated low level PDF rendering engine.

### 2020.03 - 2022.06, Metaverse game builder startup(defunct). Backend Software Engineer and DevOps. GameDev.

- Hybrid autoscaled cloud native instance allocation, updates and matchmaking for game servers
- Referral, game event objectives, online presence and other flows into microservice architecture
- Prototype of graph database modeling and scale testing of game economics world (ownership and visibility of various assets and stuff, with roles and users).
- Distributed logging and alerting, monitoring dashboards

### 2019.11.11 - 2020.02. eCommerce Point of Sale. Wallmart. Backend Software Engineer

Implemented California Consumer Privacy Act regulation conformance(optout, access and PII delete flows).
Delivered new and modified existing microservices, with covering monitoring and alerts
Wrote distributed testing tools, debugged and patched scalability issue in graph database driver.
Proposed and successfully prototyped read-delete-test+read data deletion playbook with dry run.

### 2019.08 - 2019.10. Call and contact center, [Luware](https://luware.com/en/partners/microsoft/). Backend Software Engineer and DevOps.

Cloud-native microservice-based call and contact center.
Made call handling microservice scalable by eliminating in-memory state.
Planned and deployed logging storage for 3TB logs storage with 40GB logs per day.
  
### 2018.10 - 2019.08.09. [Trooper Shooter: Critical Assault FPS (3D mobile shooter)](https://play.google.com/store/apps/details?id=com.pocket.shooter). [AZUR INTERACTIVE GAMES][15]. Network/Backend Software Engineer and DevOps

Joined team after stage of Proof of Concept to get it into Release.
Mostly did game logic, networking middleware, backend, SRE/DevOps.
Did matchmaking and meta configuration based on distributed actor system and WebSockets.
Moved game logic to be coded as Entity Component System run over threading channels. 
Did realtime synchronization protocol with compression; convenient and fast serialization over UDP.
Automated stress tests and proved each realtime commodity server thread can game 35+ concurrent users.
Deployed all to production.

### 2017.10 - 2018.10. [ETAS Measurement data analyzer](https://www.etas.com/en/products/mda-mda_v8.php). Desktop Software Engineer. Engineering

Maintained and developed Desktop app to show and analyze measurement signals from car sensors.
Mostly fixing bugs in synchronization engine, migrating from single threaded single cursor database to multi cursor with semimutable view, add detailed signal explorer.

Made synchronous blocking mutable state with many synchronization bugs components into asynchronous reactive non-blocking immutable without bugs at same time as developing feature.
That feature was developed for a month on top of non existing yet functional of data storage, I simulated API. 
We got API only last week before release and I was able to easy integrated.

At same time there was team, who trying stabilizing  component for 6 months before me. 
So I was 6x developer.

### 06.2015 - 08.2017. [Thomson Reuters Legal Electronic discovery engine(defunct) SaaS]. Legal. Backend Software Engineer

Solution for the USA legal market aimed to decrease the volume of information within litigation to derive insights and create winning legal arguments

- Developed asynchronous actor-like data-intensive persisted engine and multi-tenant distributed file system aimed for upload/ [processing/production](http://www.edrm.net/resources/edrm-stages-explained)/analysis/search/review of millions of documents on custom cloud
- Developed orchestration with error handling of event-driven services, API, billing, audit, reporting, query provider to NoSql database, asynchronous data- and work-flows
- Optimized system scaling, data ingestion and access performance on 20TB+ of indexed data
- Used several data storages(relational, document, files, queue, cache).
- Created malicious e-Discovery hardening data set
- Document and prototyped replacement of distributed lock-based flows into lock-free event-sourced flows for performance and consistency, disentangled cases where we could use eventual consistency and approximations, and were not
- Prototyped and demoed document relationships visualization.
  
### 2014.07-2015.01. [Accounting and practice management software platform for mid and large law firms](https://www.elite.com/3e/). Legal. Software Engineer

- Maintained client-server application framework, which has technical part dated back to 2003 and domain model from 1990s
- Maintained custom database oriented [IDE](http://en.wikipedia.org/wiki/Integrated_development_environment), custom [ORM](https://en.wikipedia.org/wiki/Object-relational_mapping).
- Fixed issues in fault tolerant background task executions and notifications

### 2011.07-2013.01. Website security/forensics audit for [PKI] usage in Auctions[2]. Researcher

- Used reverse engineering to find several holes in site security from client code to DB
- Proposed alternative implementation of services
- Scripted to automate security issues

### 2010.02-2014.06. Office add-ins suite and application framework for financial professionals, [Eikon for Office](https://en.wikipedia.org/wiki/Eikon). Financial. Desktop Software Engineer

Windows software for linking data between documents, financial data charting tool, productivity tools integrated with Microsoft Office, the framework for an integrated suite of desktop applications used by distributed teams.
Solved many issues typical for fat complex multi process data enabled applications connected to cloud existed for years and coded from scratch
Designed and delivered technical integration various products for financial professionals into cohesive integrated solution.
Coded custom [IPC](16).

### 2008.10-2010.06, part time. [Music Information Retrieval, visualization and analysis](http://www.scribd.com/collections/4340277/bsu-by-2005-2010). Researcher

- Prototyped visualization to organizes music collection onto 2 dimensional scatter plot via [PCA](https://en.wikipedia.org/wiki/Principal_component_analysis) based on audio content processed by [FFT](https://en.wikipedia.org/wiki/Fast_Fourier_transform) and [MIRToolbox](https://www.jyu.fi/hum/laitokset/musiikki/en/research/coe/materials/mirtoolbox).
- Researched algorithms which understand music from audio content using Hierarchical Temporal Memory and classifies via [KNN](https://en.wikipedia.org/wiki/Nearest_neighbor_search)

### 2008.07, 2009.07, internship. Time Lapse Microscopy Capture Tool, [Institute of Physical Biology](http://www.frov.jcu.cz/en/)

App for [time-lapse microscopy](https://en.wikipedia.org/wiki/Time-lapse_microscopy) via a camera attached to the microscope. 
With continual shooting, photo quality measurement using entropy, tuning camera settings.

### 2008.08-10, part-time. Social network data analysis and visualization, [Itransition](http://www.itransition.by). Intern

Coded some server-side part to get data to and from database

### 2006.08-12, part time. Woodworking machine driver, [Stroydetali Llc](http://vi-lario.com/). Driver/Desktop Software Engineer

Created industrial realtime application connected to a woodworking machine to manage sawing plans, sawing machine setup and do real time execution of sawing process.
Custom configuration file format parser, system event loop, text window system with edit boxes/menus/labels/help, bit level communication protocol with hardware.

### 2004.09-2005.03, part time. Graduates database

Created app to input and report on graduate data.
Got 2nd place on regional competition of school student’s software

## Experiences

### 2018-2020, Hackatons

Participated in 5 hackatons(4 in Belarus, 1 in Georgia). Won in 3, two  of which were legal hackatons.

### 2012-now, Security, privacy and serverless

- Used security, anonymization, encryption, and open source to see the world from hacker and privacy-focused perspective, learned how pricy these tools are.
- Used deGoogled phone for one year and saw how ineffective a person is without a corporation.
- Using distributed networks to store and read data, and understood what is future of computing.
- Built couple of serverless sites

## Education

### 2020.06-now

Many math courses on <https://brilliant.org/profile/dzmitry-rrotsd/about/>

### 2019.12-2020.04 English, British Council

IELTS 7.0. CEFR Level C1, ID  A3-BY002-S-4101019

### 2010.04-2014.02, part time

- [Finished Machine Learning, Model Thinking, Data Science, Functional Programming Principles in Scala](https://gitlab.com/dzmitry-lahoda/dzmitry-lahoda.gitlab.io/tree/master/assets/docs/me/education/coursera) on [Coursera](https://www.coursera.org).
- Watched and worked through all lectures in course of [Stanford Programming Paradigms (CS 107)](https://see.stanford.edu/Course/CS107).

### 2005.09-2010.06. Belarus State University, [Faculty of Radiophysics and Computer Technologies](http://rfe.by/en)

Got Bachelor's degree, Major Radiophysics - 01.04.03 GPA: 6/10.

### 2008, part time. Industrial software development, .NET development courses by outsoruce companies in Belarus

### 1994-2005. Gymnasium №1, Vileyka, Minsk region, Belarus

Specialized in physics and mathematics in [dalton plan](https://en.wikipedia.org/wiki/Dalton_Plan) environment.
Participated in state olympiad in Physics.

[2]: https://en.wikipedia.org/wiki/Public_key_infrastructure
[10]: https://en.wikipedia.org/wiki/Entity_component_system
[15]: https://azurgames.com/
