# Dzmitry Lahoda, Systems Software Engineer

I specialize in distributed systems, infrastructure, blockchain and protocols.

Previously did enterprise, client-servers, actor systems, cloud, desktop, drivers, reusable frameworks and libraries, real-time, and basic applied statistics in several domains, since 2004.

I grow scalable, fast, secure and maintainable code one light day at a time.

Live in Portugal since 2022.

Continuously improving at mathematics, finance, cryptography and engineering.

## Profiles

[GitHub](https://github.com/dzmitry-lahoda)  
[LinkedIn](https://www.linkedin.com/in/dzmitrylahoda)  
[HackerRank Certificate](https://www.hackerrank.com/certificates/4f1dfdbd7772)  
## Skills and tools

### Knowledge and experience

Applied knowledge of:  
heterogeneous computations; storage; computer languages and type systems;  
engineering, infrastructure, design, construction, architecture, and quality-related processes and patterns;  
mechanism design, algorithms.

### Languages

Mostly `Rust` (since 2020). Learning `Lean4`.  
For special cases do `Solidity 0.9, TS 5.9, Python 3.14` as needed too, but not so much.  
Used Go, Object Pascal, Matlab, C#/F#, C before.

### Tools

Git, *Nix, VS Code (with Copilot+Codex), Typst, Markdown (with Mermaid/PlantUML/Graphviz), Terraform, Brave Browser.  
Learning Helix editor, Jujutsu, Unix internals.

Used Kubernetes, long time beofre Windows, Visual Studio, Dos and ZX Spectrum.

## Work

### 2024.03.04 - now, [N1XYZ](https://github.com/n1xyz/)

- Maintained and enhanced the codebase of a **LMAX based central limit order book**
- Designed and implemented **perpetuals funding index**, **reduce orders**, **liquidator mechanics** (with a **sans-io based bot**).
- Reduced bugs and maintenance overhead by introducing several **typestate patterns**
- Executed efficient proptesting strategy (wide coverage with inverses and invariants, and yet fast and low code), bugs found
- Developed a `borsh` schema-based storage diff utility and TS API client generator.
- Designeded **manual offline** and **automatic live upgrades**; unified messaging for L1/L2s, storage replication protocol for an **optimistic ZK fraud-proof rollup**, partially implemented
- "Discovered" few Rust patterns which not documented in internet to make code fast, convenient and safe

### 2022.01-2024.03, Composable Foundation

Design, development, maintenance and audit of
Polkadot parachain, Cosmos CosmWasm (WASM) VM for Polkadot, cross-chain interoperability, intent-based super app and cross-chain trading/routing infrastructure and middleware (CoW like),
IBC cross-chain contracts infrastructure in Ethereum Solidity, lending, AMM, staking/rewards, vote escrowed, governance, indexers, relayers and devtools.

### 2021.03-2022.03, Web3.0, blockchain, p2p. Software Engineer. Contract works (Fluence Lab, Boosty Labs, MLabsCity, Velora)

Design, coding, audit, research, and deployment of AMM, NFT, crowdfunding, IDO, governance, DAO, identities, indexers, tools, chats for Solana and [Fluence](https://fluence.network/).

### 2021.05-2021.11.  [The Workplace Metaverse](https://www.sowork.com/). Software Engineer. Contract works

Coded 2D graphics metaverse engine features.  
Prototyped 1000+ user event-driven scalable server for world consisting of dynamic allocation of its parts, with WASM client.

### 2020.10-2021.03, [Elemy](http://web.archive.org/web/20230616112716/https://www.elemy.com/). Software Engineer. Contract works

Did a lot of longterm healthcare business flows via CQRS/ES and templated low-level PDF rendering engine for patients to sign.

### 2020.03 - 2022.06, Metaverse game builder startup ([Crey Games](https://vbstudio.hu/en/blog/20230710-The-End-of-CREY-Games)). Software Engineer.

Did hybrid global autoscaled cloud native instance allocation, upgrades and matchmaking for game servers;  
referral, game event objectives, online presence and other flows into microservice architecture.  
Prototyped and scale tested graph database for game economics world (ownership and visibility of various assets and stuff, with roles and users).  
Introduced distributed logging and alerting, monitoring dashboards.

### 2019.11.11–2020.02. eCommerce Point of Sale. Walmart. Software Engineer

Implemented California Consumer Privacy Act conformance (opt-out, access and PII delete flows)  
with new and within existing microservices and delivered (with ops, monitoring, alerts).  
Wrote distributed testing tools, debugged and patched scalability issue in graph database driver.  
Made `read-delete-test+read` pattern PII delete playbook with dry run.

### 2019.08–2019.10. Cloud-native microservice-based call and contact center, [Luware](https://luware.com/products/nimbus). Software Engineer.

Scaled and persisted call handling microservice.  
Executed logging storage for 3TB logs storage with 40GB logs per day.

### 2018.10 - 2019.08.09. [Trooper Shooter: Critical Assault FPS (3D mobile shooter)](https://play.google.com/store/apps/details?id=com.pocket.shooter). [AZUR INTERACTIVE GAMES][15]. Software Engineer

Coded middleware and server parts of game from PoC to Published state.  
Did fast real-time networking over UDP, game mechanics middleware via ECS, backend, infra, matchmaking and configuration based meta, actors.  
Stress tested my real-time part to prove commodity server thread can game 35+ concurrent users.

### 2017.10 - 2018.10. [ETAS Measurement data analyzer](https://www.etas.com/en/products/mda-mda_v8.php). Software Engineer.

Developed parts of app to show and analyze measurement signals from car sensors,  
with real-time data synchronization.  
Upgraded single-threaded to multithreaded database usage, synchronous blocking buggy code to bug-free reactive, real-time data views.

### 06.2015 - 08.2017. Thomson Reuters Legal Electronic discovery SaaS. Software Engineer

Legal product aimed to decrease the volume of information within litigation to derive insights and create legal arguments

- Developed asynchronous actor-like data-intensive multi-tenant distributed persisted engine and file system aimed for upload/[processing/production](http://www.edrm.net/resources/edrm-stages-explained)/analysis/search/review of millions of documents.
- Developed orchestration with error handling of event-driven services, API, billing, audit, reporting, query provider to NoSql database, asynchronous data- and work-flows
- Optimized system scaling, data ingestion and access performance on 20TB+ of multi storage indexed data
- Created malicious e-Discovery hardening data set and document relationships visualization

### 2014.07-2015.01. [Accounting and practice management software platform for mid and large law firms](https://www.elite.com/3e/). Software Engineer

- Maintained custom client-server application framework, database oriented [IDE](http://en.wikipedia.org/wiki/Integrated_development_environment), [ORM](https://en.wikipedia.org/wiki/Object-relational_mapping).
- Fixed in fault tolerant background task executions and notifications

### 2011.07-2013.01. Website security/forensics audit for [PKI] usage in Auctions[2]. Researcher

Used reverse engineering to find several holes in site security from client code to DB.

### 2010.02-2014.06. Office add-ins suite and application framework for financial professionals, [Eikon for Office](https://en.wikipedia.org/wiki/Eikon). Financial. Desktop Software Engineer

Design and development of linking data between documents, financial charting tool, productivity enhancements for Microsoft Office, framework for an integrated suite of desktop applications used by distributed teams, multi-process data enabled applications connected to several cloud data sources, automatic upgrades, integration of various products into cohesive solution.

### 2008.10-2010.06, part time. [Music Information Retrieval, visualization and analysis](http://www.scribd.com/collections/4340277/bsu-by-2005-2010). Researcher

- Prototyped visualization to organize music collection onto 2-dimensional scatter plot via [PCA](https://en.wikipedia.org/wiki/Principal_component_analysis) based on audio content processed by [FFT](https://en.wikipedia.org/wiki/Fast_Fourier_transform) and [MIRToolbox](https://www.jyu.fi/hum/laitokset/musiikki/en/research/coe/materials/mirtoolbox).
- Researched algorithms which understand music from audio content using Hierarchical Temporal Memory and classify via [KNN](https://en.wikipedia.org/wiki/Nearest_neighbor_search)

### 2008.07, 2009.07, internship. Time Lapse Microscopy Capture Tool, [Institute of Physical Biology](http://www.frov.jcu.cz/en/)

Time-lapse microscopy app
to control camera attached to the microscope for continual shooting, 
entropy-based quality measurement, tuning camera settings.

### 2008.08-10, part-time. Social network data analysis and visualization, [Itransition](http://www.itransition.by). Intern

Coded some server-side part to get data to and from database

### 2006.08-12, part time. Woodworking machine driver, [Stroydetali Llc](http://vi-lario.com/). Driver/Desktop Software Engineer

Created real-time application for woodworking machine to manage sawing plan, configuration and control of sawing process.  
Domain specific configuration file format, system event loop, TUI with edit boxes/menus/labels/help, bit level communication protocol with hardware.

### 2004.09-2005.03, part time. Graduates database

Coded graduates app, 2nd place in regional competition of school student’s software

## Experiences

### 2018-2020, Hackathons

Participated in a few in Belarus and Georgia. Won in legal hackathons.

### 2012-now, Security, privacy and decentralization

Used distributed networks for data, deplatformed phone, security, anonymization, encryption, OSS  
to see the world from hacker and privacy-focused perspective.  
Built a couple of serverless sites.

## Education

### 2010.04-2014.02, part time

- [Brilliant (many math courses)](https://brilliant.org/profile/dzmitry-rrotsd/about/)
- [Machine Learning, Model Thinking, Data Science, Functional Programming Principles](https://gitlab.com/dzmitry-lahoda/dzmitry-lahoda.gitlab.io/tree/master/assets/docs/me/education/coursera) on [Coursera](https://www.coursera.org).
- Watched and worked through all lectures in course of [Stanford Programming Paradigms (CS 107)](https://see.stanford.edu/Course/CS107).

### 2005.09-2010.06. Belarus State University, [Faculty of Radiophysics and Computer Technologies](http://rfe.by/en)

Bachelor's degree, Major Radiophysics - 01.04.03 GPA: 6/10.

### 2008, part time. Industrial/.NET software development courses

### 1994-2005. Gymnasium №1, Vileyka, Belarus

Physics and mathematics, dalton plan, state olympiad in Physics. GPA: 9/10.

[2]: https://en.wikipedia.org/wiki/Public_key_infrastructure  
[10]: https://en.wikipedia.org/wiki/Entity_component_system  
[15]: https://azurgames.com/
