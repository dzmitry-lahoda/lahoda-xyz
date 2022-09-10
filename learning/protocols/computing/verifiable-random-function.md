

data_i = [42];
function(secret_key, data_i) -> value
prove(secret_key, data_i, value) -> proof
public_key = secret_key.derive();
drop(secret_key);
verify(public_key, data_i, value, proof) -> boolean 
