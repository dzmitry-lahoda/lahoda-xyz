++ How to learn

How to learn
---

New
1. Acquire strict understanding of terminology


# Algorithms

0. State the problem and solution
1. Understand in natural language
2. Understand through visualization 
3. Understand through math 
4. Find numeric example and work through manually

5. Recall and repeat in natural language
6. Recall and draw visualization
7. Recall and write down in math 
8. Think up numeric example and work through manually

9. Write pseudo code
10. Write code, write intentional comments, write tests

How to write recursive
---

Start from end. Write function which receives one parameter like it would be on produced before last call. And current function is last call.

```
my_function (collection, previous, current_step):
  new_item = create(previous, current_step)
  update_collection = collection.add(new_item)
  if current_step > last:
    return update_collection
  else
    return my_function(update_collection,new_iten,current_step+1)
```

# Solving

- Variation of problem heuristic
-- Vary of change your problem to solve new problem which will help to solve your problem
