
clone remote DB to local

modify local file tree, files became modified

stage local changes to be commited. changed get into index("staging area")

commit to local database

push to remote database 

`git pull fetch $REMOTE $remote_branch:$local_branch - pull without switcnhign

I start work from X. Worked for several days got Y. People Did Z and pushed. So X becamoe XZ. I have 2 options.

1. Rebase. It like taking all my changes to X for Y, and try apply them to current XZ.

2. Merge. It kind take all changes done to X as part of Y. And apply one by one to my Z.


- `remote` by name like `origin` or `gerrit` or any is an alieas to full server link somewhere

- you may do many changes to local files, it is like running SQL without commit, but than can make transaction in local indexed database via
```
    git commit -m "did well"
```

- you make put current branch of `git` database indexed by commit via `push`:
-- `git push` 
-- you may specify specific server e.g. `git push remote origin` where we ask to put changes into server aliesed by `origin`


- get all remote branches infromation (remote database meta information) to local index to view and act
-- `git fetch --all`

- put all local changes into multi `clipboard` stack
-- `git stash`

- remove all deleted
-- `git add -u`
    
- revert
``` 
# undo all local changes and point to latest on current branch
git reset --hard HEAD
# point to specific commit
git reset --hard fb5176586d82916bce810f953fd289814d2c20c3

git checkout -- filename
```
    
- do some work aside merge part back to main code

- add all changes to commit
```
    git add --all
```

- add create branch remotely from local branch with no changes, i.e. empty branch
-- `git push --set-upstream REMOTE_NAME YET_LOCAL_EMPTY_BRANCH_NAME`
	
- undo commit
```
git reset HEAD~         
```


- Delete remote branch when it becomes garbage
- git push REMOTE_NAME --delete USELESS_BRANCH_NAME


