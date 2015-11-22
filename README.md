# Help desk VNext#

Build status : [![Build Status](https://travis-ci.org/hjgraca/HelpDeskVNext.svg)](https://travis-ci.org/hjgraca/HelpDeskVNext)

Created using asp.net 5 rc1
Uses entity framework 7 and Sql Lite
Travis to build
Docker enabled

## Runbook

    Add environment variable:
    SET <variable>=<path>
    
    Add db migrations:
    dnx ef migrations add <name>
    
    Update/create database:
    dnx ef database update
    
    Install Dnx Watch
    dnu commands install Microsoft.Dnx.Watcher