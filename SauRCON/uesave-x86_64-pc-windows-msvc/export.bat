@echo off
cd /D "%~dp0"
type template.json | uesave.exe from-json > ServerInformation.sav