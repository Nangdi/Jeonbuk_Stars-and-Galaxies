@echo off
rem ===== Settings =====
rem Full path of the built Unity exe
set "APP_PATH=C:\Build\R_Test.exe"
rem Delay in seconds
set "DELAY=30"
rem ====================

echo Starting: %APP_PATH%
echo Press any key to start now.
echo.
rem Shows countdown; any key skips the wait
timeout /t %DELAY%

for %%I in ("%APP_PATH%") do (
    cd /d "%%~dpI"
    start "" "%%~nxI"
)
exit
