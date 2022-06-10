Sometimes MS Office applications started as "embedded". 

Sometimes "embedding" is real, means that no ribbon and/or task panes visible and/or office application window is hosted in other application and/or document is not editable(view only).   In this case loading Add-in leads to longer time loading (what user does not expects) . 

Sometimes MS Office was started as "embedded", but really it is not just simple view (like from Internet Explorer or through VBA automation), e.g. some parts of MS Office UI and API are usable. 

Sometimes "embedded" version of MS Office process reused for full blown document opening, e.g. process of Excel used for preview reused when document double clicked to open full for editing.

Some MS Office UI parts or APIs can be or not be available in different "embeddings".

Some behaviors of MS Office changed, and combined with Add-in hosting lead to issues like zombies or crashes or no Add-in loaded.

Different MS Office application and versions and hosting environments behave differently in terms of same process reuse and issues(crashes, ghosts). 

Developing for "embedded" mode means testing full range of cases.


# Cases

 Next cases were obtained empirically and describe kinds of "embedding" exists. MS Office 2003-2010, Windows XP-7 where used for tests.


[VBA automation created Office application]

  Sub CreateExcel()

  Set excel = CreateObject("Excel.Application")

     excel.Workbooks.Add

    excel.Visible = True

  End Sub

  In this case MS Office launched with /automation -Embedding arguments.


[Opened from IE as separate window]

  In this case command line is -Embedding and svchost.exe -k DcomLaunch as parent process.

  Can try to detect active window and if it is IE then to decide if to initialize Add-in, but it can fail.

  System settings should be considered http://support.microsoft.com/kb/927009

  Search files to open via https://www.google.com/?gws_rd=cr&ei=jFLqUtL1M8Sk4gTlk4HABQ#q=site:planetaexcel.ru+filetype:xls

   Run script to make IE embedded  https://gitorious.org/asdandrizzo/windows/raw/b27a908a040b7e167fffab7a05adba0c719ac0ec:excel-integration/make_ie_excel_embeed.reg


[Opened from Outlook as separate window]

  Command line is /dde and parent is Outlook.


[Edit data of Excel chart in PowerPoint]

  New separate window opened, command line is /e /automation and parent is PowerPoint


[Embedded Excel spread sheet in PowerPoint]

  Command line is -Embedding and parent svchost.exe -k DcomLaunch


[Preview in Outlook]

  Command line is /Embedding and add-in is not loaded by MS Office.


[Preview in Explorer]

  Command line is -Embedding and svchost.exe -k DcomLaunch as parent process.



# How to identify real embedding 

In some cases it is needed to idenfiy "level of embedding" and command line args are not enough. Then possible fixes could be applied:

1. Get difference based on office window and parent process.

Trying to find any difference in window of Excel 2003 fails before any Add-in initialization happened. The parent process is not enough. 

2. Defer initialization for later.

It this case OnConnection should do nothing which leads to Add-in loading last of any other 3rd party Add-in's.

3. Get current active window and decided. Can fail.






