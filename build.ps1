# This file is part of TurtleHub.
# 
# Copyright (C)2018 Justin Dailey <dail8859@yahoo.com>
# 
# TurtleHub is free software; you can redistribute it and/or
# modify it under the terms of the GNU General Public License
# as published by the Free Software Foundation; either
# version 2 of the License, or (at your option) any later version.
# 
# This program is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
# GNU General Public License for more details.
# 
# You should have received a copy of the GNU General Public License
# along with this program; if not, write to the Free Software
# Foundation, Inc., 675 Mass Ave, Cambridge, MA 02139, USA.


$plugins = "TurtleHub", "TurtleLab"
$platforms = "x86", "x64"

# Create the version info for Wix
$version = cat src\version.txt | %{$_.Split(",")}
$version.join
"<?xml version=""1.0"" encoding=""utf-8""?>" | Set-Content -Path src\setup\VersionNumberInclude.wxi
"<Include Id=""VersionNumberInclude"">" | Add-Content -Path src\setup\VersionNumberInclude.wxi
"`t<?define MajorVersion=""$($version[0])"" ?>" | Add-Content -Path src\setup\VersionNumberInclude.wxi
"`t<?define MinorVersion=""$($version[1])"" ?>" | Add-Content -Path src\setup\VersionNumberInclude.wxi
"`t<?define MicroVersion=""$($version[2])"" ?>" | Add-Content -Path src\setup\VersionNumberInclude.wxi
"</Include>" | Add-Content -Path src\setup\VersionNumberInclude.wxi

# Build the project
Foreach ($platform in $platforms) {
    & "C:\Program Files (x86)\MSBuild\14.0\Bin\msbuild" /p:Configuration=Release /p:Platform=$platform /t:Clean src\TurtleHub.sln
    & "C:\Program Files (x86)\MSBuild\14.0\Bin\msbuild" /p:Configuration=Release /p:Platform=$platform src\TurtleHub.sln /p:PostBuildEvent=""
}

# Remove any old installers
Remove-Item .\bin\*.msi

pushd .\src\Setup

Foreach ($plugin in $plugins) {
    Foreach ($platform in $platforms) {
        Write-Host Building setup for $plugin-$($version[0]).$($version[1]).$($version[2])-$platform
        $env:Platform = $platform
        & "..\..\tools\WiX\candle" -nologo -out "..\..\bin\$($plugin)Setup-$platform.wixobj" "$plugin\Setup.wxs"
        & "..\..\tools\WiX\light" -nologo -sice:ICE08 -sice:ICE09 -sice:ICE32 -sice:ICE61 -out "..\..\bin\$plugin-$($version[0]).$($version[1]).$($version[2])-$platform.msi" "..\..\bin\$($plugin)Setup-$platform.wixobj" -ext WixUIExtension -cultures:en-us
    }
}

popd

# Remove intermediary files
Remove-Item .\bin\*.wixobj
Remove-Item .\bin\*.wixpdb
