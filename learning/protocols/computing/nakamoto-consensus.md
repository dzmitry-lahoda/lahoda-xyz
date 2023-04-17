

```python
data_0 = [];
previous_hash = "00001111111";


data_1 = [42];
root_hash = hash(data_1)
new_hash = [previous_hash,  data_1,  nonce, root_hash]  
assert(new_hash >> len - complexity == 0) 
```
