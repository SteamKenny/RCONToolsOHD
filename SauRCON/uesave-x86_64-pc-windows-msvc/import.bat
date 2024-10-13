@echo off
cd /D "%~dp0"
type "%1" | uesave.exe to-json