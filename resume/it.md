# About 

Dzmitry Lahoda is coding software since 2004. 

## Overview

These days enjoys coding Rust, Zig, Haskell. 

Dzmitry has C1/Advanced English, especially specifically while reading and writing.

He likes product/quality/lean/transparency/openness/effectiveness oriented approaches. Dzmitry really enjoys real-time collaboration via messaging in chats.

Dzmitry created integration solutions, client-servers, actor system, threading, cloud, desktop, drivers, custom software frameworks, reusable libraries, real-time networks. Worked in many domains. Dzmitry did not yet code graphic-intensive solutions or math stuff. 

Worked with all possible storage mediums and query languages.

Computer user since 1993, write programs since 2004, code version control and automated tests since 2007, increase my velocity by open-source since 2009, work in companies since 2010, self-learner since childhood, do [active learning](https://en.wikipedia.org/wiki/Active_learning) and [incremental reading](https://en.wikipedia.org/wiki/Incremental_reading).  Did generalized machine learning algorithms in 2010.

Dzmitry enjoys type systems and automated quality assurance of any kind, and writing in English. He always finds how to improve coding at hand at different levels.

For the last year or so, he learns applied mathematics in his free time.

## Profiles

[Github](https://github.com/dzmitry-lahoda) [LinkedIn](https://www.linkedin.com/in/dzmitrylahoda) [Alternativeto.net Software Encyclopedist](https://alternativeto.net/user/dzmitrylahoda/) [Triplebyte Generalist Certificate](https://triplebyte.com/tb/dzmitry-lahoda-pijxwdl/certificate/track/generalist) [HackerRank Certificate](https://www.hackerrank.com/certificates/4f1dfdbd7772) [Brilliant(many math courses)](https://brilliant.org/profile/dzmitry-rrotsd/about/)

## Using (in random order)

Rust, F#, automated testing, clouds, virtualization, Kubernetes, Windows, Prometheus, ELK, Hashicorp, Git, networks, Visual Studio Code, any query langauge or storage, microservices, distributed computing, ElasticSearch, Entity Component System, API description languages

## Products

### 2021.03-2021.04, 2021.07. [Solana](https://solana.com/). Software Engineer. Blockchain.

- [NFT, chats](https://github.com/Satellite-im/satellite-solana-contracts), [DeFi, crowd funding contracts](https://solstarter.org/), staking/rewards on Solana chain (ported Solidity or coded by specifications)
- Rust 

### 2020.10-2021.03, [Sprout Therapy](https://www.joinsprouttherapy.com/), Parents (Demand). Healthcare.

- coded some flows for long term therapy from login, from questionnaires to documents signing to marketing
- functional and type driven coding 
- low level PDF rendering
- Rust, CQRS ES

### 2020.03 - now, [Crey Games](playcrey.com). Software Engineer. GameDev

- Ownership on hybrid instance allocation for game servers
- Created persisted distributed state machine for game versions upgrades
- Found possible holes regarding security and scalability 
- Design and coding referral, game event objectives, online presence and other flows into mircoservice architecture
- Prototype of graph db modeling and scale testing of game economics world (ownership and visibility of various assets and stuff, with roles and users). 
- Researched alternative and implemented migration from one game servers orchestration engine to other  
- Setup distributed logging and alerting,  built statistically sound alerts and monitoring dashboards
- Found a unique way to port some F# features onto C# 9 (no alternatives yet found on github)
- Custom in memory matchmaking allocation loop via cashing threading channels (in memory actors)
- Agones, .NET 5, Go

### 2019.11.11 - 2020.02. eCommerce Point of Sale. [Jet](https://en.wikipedia.org/wiki/Jet.com). 

* Jet lagged after best leads to make [CCPA](https://en.wikipedia.org/wiki/California_Consumer_Privacy_Act) regulation (short term project)
* Debugged and patched scalability issue in graph database driver
* Did substantial work in delivering several microservices (optout, access and delete flows) covered by monitoring and alerts
* Wrote tool to test flows via queue
* Proposed and successfully prototyped read-delete-test+read data deletion playbook with dry run 
* Visited USA for 3 weeks trip.
* Decided not to participating in integrating Jet into Walmart corporation via old version of Java.
* Event sourcing

### 2019.08 - 2019.10. Call and contact center, [Luware](https://luware.com/en/partners/microsoft/).

Cloud-native microservice-based call and contact center.

* Made call handling microservice scalable by eliminating in-memory state.
* Proposed and communicated improvements to changed build and deployment to be cloud-native and continuos with gates.
* Planned and deployed logging storage for 3TB logs storage with 40GB logs per day.
  
### 2018.10 - 2019.08.09. [Trooper Shooter: Critical Assault FPS (3D mobile shooter)](https://play.google.com/store/apps/details?id=com.pocket.shooter). [AZUR INTERACTIVE GAMES][15]. Software Engineer

* Bootstrapped actor-based solution with WebSocket frontend for meta part of the game.
* Guided migration from [OOP][1] to [DOD][5] [ECS][10]-like design to increase performance of realtime part
* Coded realtime synchronization protocol and compression; convenient and fast serialization
* Used channels with cooperative multithreading and concurrent queue to implement HPC realtime game server
* Automated stress tests and proved each realtime commodity server thread can game 35+ concurrent users
* Automated distributed deployment
* Facilitated improvement of code organization at large, introduced full-stack low cost structured logging and vertical semantic namespacing, cross IDE builds
* Coded matchmaking and some battle logic.
* Made sure all 
* Facilities migration from paper-based workflow to digital one with the proper hierarchy of work items.
* Learned from the flat cross-functional team of developers and designers of approx. 10 people in delivering game from Proof of Concept into Release.
* Delivering game from Proof of Concept into Release.

### 2017.10 - 2018.10. [ETAS Measurement data analyzer](https://www.etas.com/en/products/mda-mda_v8.php). Software Engineer. Engineering.

[Windows desktop application](https://www.youtube.com/watch?v=Iuqq1RdgY0A&list=PLdK8AlEjocsVVJfXTdzvzHBNrR31DNS8n) to show and analyze measurement data from car sensors.

* Improved code practices and performance, reduced technical debt
* Made synchronous blocking mutable state components into asynchronous reactive non-blocking immutable. 
* Did for 1 month as side effect of doing other feature what the whole team was doing for half year.
* Shared practices with the team about testing, process, collaboration, documentation.
* Report reviews of components for performance, usability, stability, concurrency, and scalability, reported in various forms
* Fixed bugs of in-memory data synchronization within engineering instruments and in the custom plugin system
* Worked with teams from Germany and visited them regularly. Worked hard to make all collaboration online and transparent
* Was in the stream of 5 developers, within 40 people project.
  
### 06.2015 - 08.2017. [Thomson Reuters Legal eDiscovery server engine](https://legal.thomsonreuters.com/en/products/ediscovery-point). Legal.

[e-Discovery](https://en.wikipedia.org/wiki/Electronic_discovery) is end-to-end [SaaS](https://en.wikipedia.org/wiki/Software_as_a_service) solution for the USA legal market aimed to decrease the volume of information within litigation to derive insights and create winning legal arguments

* Developed asynchronous actor-like data-intensive persisted engine and intelligent multi-tenant distributed file system aimed for upload/ [processing/production](http://www.edrm.net/resources/edrm-stages-explained)/analysis/search/review of millions of documents on custom cloud
* Maintained intelligent multi-tenant distributed file system aimed for handling millions of documents
* Developed orchestration with error handling of event-driven services, HTTP API, billing, audit, reporting, query provider to NoSql database, reusable infrastructure components, asynchronous data- and work-flows
* Optimized system scaling, data ingestion and access performance on 20TB+ of indexed data
* Used several data storages(relational, document, files, queue, cache).
* Improved teams capability to produce better software faster by setting up practices of agile lean documentation and knowledge sharing
* Created malicious e-Discovery hardening data set
* Document and prototyped replacement of distributed lock-based flows into lock-free event-sourced flows for performance and consistency, disentangled cases where we could use eventual consistency and approximations, and were not
* Thought up, elaborated and managed prototyping of document relationships visualization presented to the customer
* Articulated several cross-team inefficiencies up to resolutions, helped the customer to improve skill-position fit of some employers
* Trained my writing English to be properly structured, clear and action inducing
* Worked within the distributed team of 30 developers
  
### 2014.07-2015.01. [Accounting and practice management software platform for mid and large law firms](https://www.elite.com/3e/). Legal

* Maintained client-server application framework, which has technical part dated back to 2003 and domain model from 1990s
* Maintained custom database oriented [IDE](http://en.wikipedia.org/wiki/Integrated_development_environment), custom [ORM](https://en.wikipedia.org/wiki/Object-relational_mapping), [XML based][19] [GUI](https://en.wikipedia.org/wiki/Graphical_user_interface)
* Fixed issues in fault tolerant background task executions and notifications

### 2010.02-2014.06. Office add-ins suite and application framework for financial professionals, [Eikon for Office](https://en.wikipedia.org/wiki/Eikon). Financial

Windows software for linking data between documents, financial data charting tool, productivity tools integrated with Microsoft Office, the framework for an integrated suite of desktop applications used by distributed teams.

* I delivered and lead technical integration various products for financial professionals into cohesive integrated solution. Some parts of my works are open-sourced on GitHub.
* Engaged in deliver cross teams and cross technologies(native and managed) integration framework to get larger business value.
* Using deep understanding of technologies to deliver business results and enablers
* Improved knowledge sharing and collaboration in distributed teams
* Refactoring existing code into components with proper variability engineering
* Coded custom [IPC](16) and asynchronous modules systems
* Solved data, authentication, multiple login prevention, auto-upgrade on client.
* Made internal tools for productivity, profiling, optimizations and performance analysis.
* Debugged multithreaded and multiprocess issues.
* Effectively introduced [TDD][17], [DI][18], [BDD][11], achieved near-zero bug development.
* Aggressively eliminated technical debt using modern code practices
* Used virtual machines to test in different setups and fix multiuser environments
* Tripped to France to do talks on the customer side
* Made internal tools for productivity, profiling, optimizations and performance analysis
* Worked in coordination with 50+ developers from several countries, started from small end-user functional team, become distributed framework team of changing size with many functional teams

### 2008.10-2010.06, part time. [Music Information Retrieval, visualization and analysis](http://www.scribd.com/collections/4340277/bsu-by-2005-2010)

* [Prototyped](https://gitlab.com/dzmitry-lahoda/learning-machine-learning/tree/master/mir) visualization to organizes music collection onto 2 dimensional scatter plot via [PCA](https://en.wikipedia.org/wiki/Principal_component_analysis) based on audio content processed by [FFT](https://en.wikipedia.org/wiki/Fast_Fourier_transform) and [MIRToolbox](https://www.jyu.fi/hum/laitokset/musiikki/en/research/coe/materials/mirtoolbox). 
* Researched algorithms which understand music from audio content using Hierarchical Temporal Memory and classifies via [KNN](https://en.wikipedia.org/wiki/Nearest_neighbor_search)
* With zero supervision, idea and whole implementation are mine.


### 2008.07, 2009.07, internship. [Time Lapse Microscopy Capture Tool](https://www.youtube.com/watch?v=t1TauTaMb6Y), [Institute of Physical Biology](http://www.frov.jcu.cz/en/)

* Scientific domain and hands-on experience in collaboration [with scientists on English](https://www.youtube.com/watch?v=t1TauTaMb6Y).
* Developed Windows application for [time-lapse microscopy](https://en.wikipedia.org/wiki/Time-lapse_microscopy) during [summer school](https://gitlab.com/dzmitry-lahoda/dzmitry-lahoda.gitlab.io/tree/master/assets/docs/me/doc/summer_school_cz)
* Featured continual shooting of photos via a camera attached to the microscope, measuring photo quality, tuning camera settings, plotting entropy calculation, logging results
* Was under scientific guidance of 2 Ph.D. students

### 2008.08-10, part-time. Social network data analysis and visualization, [Itransition](http://www.itransition.by)

* Coded some server-side part to get data to and from database
* Worked with 3 junior developers and manager

### 2006.08-12, part time. Woodworking machine driver, [Stroydetali Llc](http://vi-lario.com/)

Created industrial [MS-DOS](https://en.wikipedia.org/wiki/MS-DOS) application connected to a woodworking machine.

* Coded tools to edit/store/execute sawing plans, change sawing machine setup and monitor sawing process.
* Programmed configuration file format parser, custom system event loop, custom text window system with edit boxes/menus/labels/help, communication protocol with hardware [LPT](https://en.wikipedia.org/wiki/Parallel_port) port.
* Worked and communicated with microcontroller programmer.

### 2004.09-2005.03, part time. Graduates database

* Created installable Windows application to input and report on graduate data.
* Got 2nd place on regional competition of school student’s software

## Experiences

### 2018-2020, Hackatons

Participated in 5 hackatons(4 in Belarus, 1 in Georgia). Won in 3, two  of which were legal hackatons.  So nothing was born out these...

### 2012-2019.10, Security and privacy and serverless

* Used security, anonymization, encryption, and open source to see the world from hacker and privacy-focused perspective, learned how pricy these tools are.
* Used deGoogled phone for one year and saw how ineffective a person is without a corporation.
* Using distributed networks to store and read data, and understood what is future of computing.
* Built couple of jamstack sites

### 2011.07-2013.01, fun time. Website security/forensics audit for [PKI][2]

* Used reverse engineering to find several holes in site security from client code to DB
* Proposed alternative implementation of services
* Provided scripts to business to do security evaluation continuously

## Education

### 2020.06-now

Many math courses on https://brilliant.org/profile/dzmitry-rrotsd/about/

### 2019.12-2020.04 English, British Council

IELTS 7.0. CEFR Level C1

Issued date and, if applicable, expiration date of the certification or license Issued May 2020No Expiration Date

Credential Identifier Credential ID A3-BY002-S-4101019


### 2010.04-2014.02, part time

* [Finished Machine Learning, Model Thinking, Data Science, Functional Programming Principles in Scala](https://gitlab.com/dzmitry-lahoda/dzmitry-lahoda.gitlab.io/tree/master/assets/docs/me/education/coursera) on [Coursera](https://www.coursera.org). 
* Watched and worked through all lectures in course of [Stanford Programming Paradigms (CS 107)](https://see.stanford.edu/Course/CS107).

### 2005.09-2010.06. Belarus State University, [Faculty of Radiophysics and Computer Technologies](http://rfe.by/en)

* [Graduated university with Bachelor's degree, Major Radiophysics - 01.04.03 GPA: 6/10.](https://gitlab.com/dzmitry-lahoda/dzmitry-lahoda.gitlab.io/tree/master/assets/docs/me/education/bsu_by).

### 2008.04, 2008.11, part time. Software engineering courses at [ScienceSoft .NET Framework course](http://www.scnsoft.com/) and [Itransition Industrial software development course](http://www.itransition.by/)

### 1994-2005. Gymnasium №1, Vileyka, Minsk region, Belarus

* Specialization in physics and mathematics
* Employed [dalton plan](https://en.wikipedia.org/wiki/Dalton_Plan) for self-education
* Participated in republic olympiad in Physics at 11th grade

[1]: https://en.wikipedia.org/wiki/Object-oriented_programming
[2]: https://en.wikipedia.org/wiki/Public_key_infrastructure
[3]: https://en.wikipedia.org/wiki/Internet_Information_Services
[4]: https://en.wikipedia.org/wiki/Windows_Presentation_Foundation
[5]: https://en.wikipedia.org/wiki/Data-oriented_design
[6]: https://en.wikipedia.org/wiki/Dependency_injection
[7]: https://gitlab.com/
[9]: http://www.thomsonreuters.com
[10]: https://en.wikipedia.org/wiki/Entity_component_system
[11]: http://en.wikipedia.org/wiki/Behavior-driven_development
[12]: http://en.wikipedia.org/wiki/Object-oriented_analysis_and_design
[13]: https://en.wikipedia.org/wiki/Search_engine_optimization
[14]: https://en.wikipedia.org/wiki/Component_Object_Model
[15]: https://azurgames.com/
[16]: https://en.wikipedia.org/wiki/Inter-process_communication
[17]: https://en.wikipedia.org/wiki/Test-driven_development
[18]: https://en.m.wikipedia.org/wiki/Dependency_injection
[19]: https://en.wikipedia.org/wiki/XML
[21]: https://en.wikipedia.org/wiki/Extensible_Application_Markup_Language
[23]: https://www.gerritcodereview.com/
