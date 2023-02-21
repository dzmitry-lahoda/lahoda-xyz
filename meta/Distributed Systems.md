
#Consisteny protocal
- gossip (AP)
- consensus (CP)

#Consitency models

# 2pc - two phase commit 


#Idenpotency can be used
- never for timeout got response than resend

## distributed sagas
- request
- compensating request to undo the effect
- commutative
- total queyr order number
- needs saga log (im memory absoluterly fast dirstibuted data store)
- each sender stores logs until vacuum and may be rerequested

# feral  concurrency control mechanism
- order of execution
- handling errors and edge cases

# Testing
- unit, integration, proprety, model
- fault injection,
- 
## Prod 
- canary


