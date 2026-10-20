@echo off
title Grifindo Toys - System Launcher
color 0A
cls

echo ================================================
echo        GRIFINDO TOYS MANAGEMENT SYSTEM
echo ================================================
echo.

:: -----------------------------------------------
:: Step 1: Find MSBuild
:: -----------------------------------------------
echo [1/4] Locating MSBuild...
set MSBUILD="C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"

if not exist %MSBUILD% (
    set MSBUILD="C:\Program Files\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe"
)
if not exist %MSBUILD% (
    set MSBUILD="C:\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe"
)
if not exist %MSBUILD% (
    echo ERROR: MSBuild not found. Please install Visual Studio 2022.
    pause
    exit /b 1
)
echo      Found MSBuild OK
echo.

:: -----------------------------------------------
:: Step 2: Start SQL LocalDB
:: -----------------------------------------------
echo [2/4] Starting SQL LocalDB...
sqllocaldb start MSSQLLocalDB >nul 2>&1
echo      Database ready
echo.

:: -----------------------------------------------
:: Step 3: Check / Restore Krypton Packages
:: -----------------------------------------------
echo [3/4] Checking UI components...
if not exist "packages\Krypton.Components.Suite.4.5.9\lib\net20\ComponentFactory.Krypton.Toolkit.dll" (
    echo      Krypton packages missing. Downloading nuget.exe to restore...
    powershell -Command "Invoke-WebRequest -Uri https://dist.nuget.org/win-x86-commandline/latest/nuget.exe -OutFile nuget.exe"
    nuget.exe restore "Grifindo Toys.sln"
    if exist nuget.exe del nuget.exe
)
echo      UI components ready
echo.

:: -----------------------------------------------
:: Step 4: Build and Launch
:: -----------------------------------------------
echo [4/4] Building Grifindo Toys...
%MSBUILD% "Grifindo Toys.sln" /t:Build /p:Configuration=Debug /v:minimal /nologo

if errorlevel 1 (
    echo.
    echo ================================================
    echo   BUILD FAILED! Check errors above.
    echo ================================================
    pause
    exit /b 1
)

echo.
echo ================================================
echo   Build successful! Launching application...
echo ================================================
echo.

start "" "Grifindo Toys\bin\Debug\Grifindo Toys.exe"

exit /b 0
