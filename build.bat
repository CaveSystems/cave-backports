@echo off
chcp 1252
if "%VisualStudioVersion%"=="" call "%ProgramFiles%\Microsoft Visual Studio\2022\BuildTools\Common7\Tools\VsDevCmd.bat"
if "%VisualStudioVersion%"=="" call "%ProgramFiles%\Microsoft Visual Studio\2022\Enterprise\Common7\Tools\VsDevCmd.bat"
if "%VisualStudioVersion%"=="" call "%ProgramFiles%\Microsoft Visual Studio\2022\Professional\Common7\Tools\VsDevCmd.bat"

msbuild /t:Clean
if %errorlevel% neq 0 exit /b %errorlevel%

dotnet restore
if %errorlevel% neq 0 exit /b %errorlevel%

msbuild /p:Configuration=Release /p:Platform="Any CPU"
if %errorlevel% neq 0 exit /b %errorlevel%

msbuild /p:Configuration=Debug /p:Platform="Any CPU"
if %errorlevel% neq 0 exit /b %errorlevel%

rem msbuild /p:Configuration=Release /p:Platform="Any CPU" documentation.shfbproj
rem if %errorlevel% neq 0 exit /b %errorlevel%

rem this will test netcore 3.1 and >net46
dotnet test

dotnet Tests\bin\Release\netcoreapp1.0\ManualTest.dll
dotnet Tests\bin\Release\netcoreapp1.1\ManualTest.dll
dotnet Tests\bin\Release\netcoreapp2.0\ManualTest.dll
dotnet Tests\bin\Release\netcoreapp2.1\ManualTest.dll
Tests\bin\Release\net20\Test.exe
Tests\bin\Release\net35\Test.exe
Tests\bin\Release\net40\Test.exe
Tests\bin\Release\net45\Test.exe
Tests\bin\Release\net46\Test.exe

