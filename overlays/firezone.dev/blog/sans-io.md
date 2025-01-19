
- any async function makes all callers async, coliring them
- async function is suspended and proceed later, so it is part of function contract
- in the end only last final fuction is async, not all function in stack
- Like custom enum relates to option, that sans-io even data relates to future
- Poll approximately per state, not single all for all
- Future is 'static lifetime meaning it must be no reference to it (so need clone Arc<Mutext<>>)
- there is stuctured concurrency to try to 
- each await adds state to state machine and allow split function.