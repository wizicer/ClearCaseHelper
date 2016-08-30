@echo off
set x86xsd="C:\Program Files\Microsoft SDKs\Windows\v8.1A\bin\NETFX 4.5.1 Tools\x64\xsd.exe"
set x64xsd="C:\Program Files (x86)\Microsoft SDKs\Windows\v8.1A\bin\NETFX 4.5.1 Tools\x64\xsd.exe"
if exist %x86xsd% (
    %x86xsd% SynchronizerSessions.xsd /c /l:cs /n:IcerCCHelper
) else (
    %x64xsd% SynchronizerSessions.xsd /c /l:cs /n:IcerCCHelper
)
if %errorlevel% neq 0 pause
