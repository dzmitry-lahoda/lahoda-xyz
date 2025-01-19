# What I do?

HX - Helix editor

1. find file by name in console
2. find file by name in HX
3. rename file in HX
4. find string in HX in file
5. replace all words in HX in file
6. split file on left and tree on right
7. go left right from editor to tree
8. select several lines
9. select first words in several lines
10. find and replace in several files
11. text diff 2 files
12. semantic diff 2 files
13. solve git conflict using 2 files

### Accept changes in diff



type quit = `:q` | `:quit:` = `closer current view` | `close` | `ctrl+f4` | `exit`

type force_quit = (quit, `!`) with `i` = ignore unsaved changes

type open = `:o` | `:open` = open file into current view
