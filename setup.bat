@echo off
echo ============================================
echo  TastyBites Restaurant Ordering System
echo ============================================
echo.

echo [1/2] Setting up Backend...
cd backend
dotnet restore
if %errorlevel% neq 0 (
    echo ERROR: dotnet restore failed. Make sure .NET 8 SDK is installed.
    pause
    exit /b 1
)
echo Backend dependencies installed!
echo.

echo [2/2] Setting up Frontend...
cd ..\frontend
npm install
if %errorlevel% neq 0 (
    echo ERROR: npm install failed. Make sure Node.js 18+ is installed.
    pause
    exit /b 1
)
echo Frontend dependencies installed!
echo.

echo ============================================
echo  Setup Complete!
echo ============================================
echo.
echo To start the backend:
echo   cd backend ^&^& dotnet run
echo.
echo To start the frontend (new terminal):
echo   cd frontend ^&^& npm start
echo.
echo Backend API: http://localhost:5000
echo Frontend:    http://localhost:4200
echo Swagger:     http://localhost:5000/swagger
echo.
pause
