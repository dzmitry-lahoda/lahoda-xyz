# Overview

Allow to index and search files as https://www.voidtools.com/ (works in service as admin mode only) instantly.

Written in Rust for safety and low resources consumtion.

I will allow to bookmark files using # by creating folder with links with files renamed to include #
https://schinagl.priv.at/nt/hardlinkshellext/hardlinkshellext.html

It will allow to search # tags like chrome-extension://edpeidcfjfepdgdjnodefckgdjbigem/index.html

Also it will allow to tag via https://github.com/Dijji/FileMeta, so no name changes neeed.

Bookmarked links are possible to sync via syncthing. It will give each file unique global location and mapping.

Hooks into file Explorer Open menu via Win32API so that when any application ask to open file, it is immediately searchable. For hook example look into https://www.dcsoft.com/Products/RegEditX  .

Windows Explorer plugin to do all operation possible in native UI.

Supports wildcards, linear time regexes, exact matches.

There is general guidance on bookmarks usability which can be applied to.

If files found is same as file by link, allow to see both links. In ReFs is cas of integrity for data is ON - show one to one Diffs.



# Why it different?

- written in safe and fast language to get best of speed and security languages.
- does not stores metadata file along existing file, neither changes existing file, so hash of file is same and sync tools are not polluted
- It does not have any sync or content indexing mechanisms buildin.
- does not slows down system in most cases
- optimized for windows, not xplat
