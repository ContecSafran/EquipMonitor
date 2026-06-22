@echo off
start "CORS Proxy" /min powershell -ExecutionPolicy Bypass -File "%~dp0proxy.ps1"
timeout /t 1 /nobreak > nul
start "" "%~dp0api_tester.html"
