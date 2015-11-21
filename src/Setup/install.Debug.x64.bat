@ECHO OFF
REM Set local environmental variable for this script.
SETLOCAL

REM Log file.
TITLE TurtleHub

SET LOGFILE="%CD%\TurtleHub.log"
ECHO Installation of TurtleHub issue tracker plugin. > %LOGFILE% 2>&1
ECHO Installation of TurtleHub issue tracker plugin.

ECHO Change to ..\TurtleHub\bin\x64\Debug directory. >> %LOGFILE% 2>&1
ECHO Change to ..\TurtleHub\bin\x64\Debug directory.

SET ROOTDIR=%CD%
REM remove "install" or "INSTALL" from path
SET ROOTDIR=%ROOTDIR:\install=%
SET ROOTDIR=%ROOTDIR:\INSTALL=%
CD "%ROOTDIR%\..\TurtleHub\bin\x64\Debug" >> %LOGFILE% 2>&1

ECHO Register COM interface for plugin. >> %LOGFILE% 2>&1
ECHO Register COM interface for plugin.

C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\RegAsm TurtleHub.dll /codebase /regfile:TurtleHub.reg >> %LOGFILE% 2>&1

ECHO Add Implemented Categories key. >> %LOGFILE% 2>&1
ECHO Add Implemented Categories key.

SET NEWKEY=[HKEY_CLASSES_ROOT\CLSID\{B2C6EC0F-8742-4792-9FDC-10635D2C118B}\Implemented Categories\{3494FA92-B139-4730-9591-01135D5E7831}]

ECHO %NEWKEY% >> TurtleHub.reg

ECHO Backing up registry... >> %LOGFILE% 2>&1
ECHO Backing up registry...

SET RGBKFILE=today-regbkfile.reg  >> %LOGFILE% 2>&1
REGEDIT /E "%ROOTDIR%\..\TurtleHub\bin\x64\Debug\%RGBKFILE%" >> %LOGFILE% 2>&1

ECHO Merge registry keys. >> %LOGFILE% 2>&1
ECHO Merge registry keys.
REGEDIT /S "%ROOTDIR%\..\TurtleHub\bin\x64\Debug\TurtleHub.reg" >> %LOGFILE% 2>&1


ECHO Installation complete: %DATE%. >> %LOGFILE% 2>&1
PAUSE

REM End local environmental variable for this script.
ENDLOCAL