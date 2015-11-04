@echo off

set name=All
set logDir=logs_%name%
call ..\Build_Core.bat Build_%name%.txt %logDir% error_projects_default.txt

pause