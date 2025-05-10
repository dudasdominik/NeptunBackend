@echo off
setlocal enabledelayedexpansion

:: Config - container name and volume
set CONTAINER_NAME=postgres_container
set VOLUME_NAME=neptunbackend_postgres_data


echo ----------------------------------------
echo [STEP 0] Cleaning up existing container...

docker rm -f %CONTAINER_NAME% >nul 2>&1
if %ERRORLEVEL% EQU 0 (
    echo Removed existing container: %CONTAINER_NAME%
) else (
    echo No existing container to remove.
)

docker volume rm %VOLUME_NAME% >nul 2>&1
if %ERRORLEVEL% EQU 0 (
    echo Removed volume: %VOLUME_NAME%
) else (
    echo No volume to remove or already gone.
)

ping -n 2 127.0.0.1 >nul

echo ----------------------------------------
echo [STEP 1] Loading environment variables from .env...

for /f "usebackq tokens=1,* delims==" %%A in (".env") do (
    echo %%A| findstr /b "#" >nul
    if errorlevel 1 (
        set %%A=%%B
    )
)

echo PG_USER = %PG_USER%
echo PG_PASSWORD = %PG_PASSWORD%
echo PG_DATABASE = %PG_DATABASE%
echo PG_PORT = %PG_PORT%
echo.

ping -n 2 127.0.0.1 >nul

echo ----------------------------------------
echo [STEP 2] Starting Docker Compose...

docker-compose --env-file .env up -d
IF %ERRORLEVEL% NEQ 0 (
    echo ❌ Failed to start Docker Compose.
    pause
    exit /b 1
)

echo Docker Compose started.
ping -n 6 127.0.0.1 >nul

echo ----------------------------------------
echo [STEP 3] Running EF Core migration...

dotnet ef database update
   IF %ERRORLEVEL% NEQ 0 (
     echo ❌ EF Core migration failed.
     pause
     exit /b 1
 )

 
echo.
echo ✅ All done. PostgreSQL is running and seeded!
pause
endlocal
