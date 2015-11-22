@echo off

REM This file is part of TurtleHub.
REM 
REM Copyright (C)2015 Justin Dailey <dail8859@yahoo.com>
REM 
REM TurtleHub is free software; you can redistribute it and/or
REM modify it under the terms of the GNU General Public License
REM as published by the Free Software Foundation; either
REM version 2 of the License, or (at your option) any later version.
REM 
REM This program is distributed in the hope that it will be useful,
REM but WITHOUT ANY WARRANTY; without even the implied warranty of
REM MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
REM GNU General Public License for more details.
REM 
REM You should have received a copy of the GNU General Public License
REM along with this program; if not, write to the Free Software
REM Foundation, Inc., 675 Mass Ave, Cambridge, MA 02139, USA.

pushd "%~dp0"
SetLocal EnableDelayedExpansion

:: load the version numbers into variables
for /F "delims=, tokens=1,2,3" %%i in (src\version.txt) do (
	set majorversion=%%i
	set minorversion=%%j
	set microversion=%%k
	REM set wcversion=%%l
)
:: write the AssemblyInfoVersion.cs file with the version info
:: echo using System.Reflection; > src\Gurtle\Properties\AssemblyInfoVersion.cs
:: echo [assembly: AssemblyVersion("%majorversion%.%minorversion%.%microversion%.%wcversion%")] >> src\Gurtle\Properties\AssemblyInfoVersion.cs
:: echo [assembly: AssemblyFileVersion("%majorversion%.%minorversion%.%microversion%.%wcversion%")] >> src\Gurtle\Properties\AssemblyInfoVersion.cs

:: write the VersionNumberInclude.wxi file
echo ^<?xml version="1.0" encoding="utf-8"?^> > src\setup\VersionNumberInclude.wxi
echo ^<Include Id="VersionNumberInclude"^> >> src\setup\VersionNumberInclude.wxi
echo 	^<?define MajorVersion="%majorversion%" ?^> >> src\setup\VersionNumberInclude.wxi
echo 	^<?define MinorVersion="%minorversion%" ?^> >> src\setup\VersionNumberInclude.wxi
echo 	^<?define MicroVersion="%microversion%" ?^> >> src\setup\VersionNumberInclude.wxi
REM echo 	^<?define BuildVersion="%wcversion%" ?^> >> src\setup\VersionNumberInclude.wxi
echo ^</Include^> >> src\setup\VersionNumberInclude.wxi

for %%i in (Debug Release) do (
    "%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\msbuild" /p:Configuration=%%i /p:Platform=x86 /t:Clean src\TurtleHub.sln
    "%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\msbuild" /p:Configuration=%%i /p:Platform=x64 /t:Clean src\TurtleHub.sln
    "%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\msbuild" /p:Configuration=%%i /p:Platform=x86 src\TurtleHub.sln
    "%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\msbuild" /p:Configuration=%%i /p:Platform=x64 src\TurtleHub.sln
)

:: build the installer
del bin\*.msi
pushd src\Setup
for %%a in (x86 x64) do (
    echo Building setup for %%a platform
    set Platform=%%a
    ..\..\tools\WiX\candle -nologo -out ..\..\bin\Setup-%%a.wixobj Setup.wxs 
    ..\..\tools\WiX\light -nologo -sice:ICE08 -sice:ICE09 -sice:ICE32 -sice:ICE61 -out ..\..\bin\TurtleHub-%majorversion%.%minorversion%.%microversion%-%%a.msi ..\..\bin\Setup-%%a.wixobj -ext WixUIExtension -cultures:en-us
    REM ..\..\tools\WiX\candle -nologo -out ..\..\bin\MergeModule-%%a.wixobj MergeModule.wxs 
    REM ..\..\tools\WiX\light -nologo -sice:ICE08 -sice:ICE09 -sice:ICE32 -sice:ICE61 -out ..\..\bin\TurtleHub-%majorversion%.%minorversion%.%microversion%-%%a.msm ..\..\bin\MergeModule-%%a.wixobj
)
popd
del bin\*.wixobj
del bin\*.wixpdb

:end

popd
