@echo off
rem parameters

rem initial environment
rem / ___)(  __)(_  _)(_  _)(  )(  ( \ / __) ___ / )( \(  _ \
rem \___ \ ) _)   )(    )(   )( /    /( (_ \(___)) \/ ( ) __/
rem (____/(____) (__)  (__) (__)\_)__) \___/     \____/(__)  
echo Setting Up Environment
path %PATH%;C:\Program Files (x86)\MSBuild\14.0\Bin\;
path %PATH%;%CD%\tools\7z

rem build
rem (  _ \/ )( \(  )(  )  (    \(  )(  ( \ / __)
rem  ) _ () \/ ( )( / (_/\ ) D ( )( /    /( (_ \
rem (____/\____/(__)\____/(____/(__)\_)__) \___/
echo Building

.nuget\nuget.exe restore
if %errorlevel% neq 0 goto :error

MSBuild.exe IcerCCHelper.sln /t:rebuild /p:configuration=Release
set BUILD_STATUS=%ERRORLEVEL%
if not %BUILD_STATUS%==0 goto :error

rem make package
rem (  _ \ / _\  / __)(  / ) / _\  / __)(  )(  ( \ / __)
rem  ) __//    \( (__  )  ( /    \( (_ \ )( /    /( (_ \
rem (__)  \_/\_/ \___)(__\_)\_/\_/ \___/(__)\_)__) \___/

cd IcerCCHelper\bin\Release

echo Zipping file
echo.>filelist
echo IcerCCHelper.exe>>filelist
echo IcerCCHelper.exe.config>>filelist
echo log4net.dll>>filelist
echo System.IO.Abstractions.dll>>filelist
echo server.xml>>filelist
echo GuidePage.html>>filelist

if exist ..\..\..\IcerCCHelper.zip (del ..\..\..\IcerCCHelper.zip)
7z a ..\..\..\IcerCCHelper.zip @filelist
if %errorlevel% neq 0 goto :error

del filelist
if %errorlevel% neq 0 goto :error

cd ..\..\..

echo  ____  __  __ _  __  ____  _  _  ____  ____ 
echo (  __)(  )(  ( \(  )/ ___)/ )( \(  __)(    \
echo  ) _)  )( /    / )( \___ \) __ ( ) _)  ) D (
echo (__)  (__)\_)__)(__)(____/\_)(_/(____)(____/
goto :EOF

:error
pause Failed with error #%errorlevel%.
exit /b %errorlevel%




