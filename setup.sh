#!/bin/bash

echo "============================================"
echo " TastyBites Restaurant Ordering System"
echo "============================================"
echo ""

echo "[1/2] Setting up Backend..."
cd backend
dotnet restore
if [ $? -ne 0 ]; then
    echo "ERROR: dotnet restore failed. Make sure .NET 8 SDK is installed."
    exit 1
fi
echo "Backend dependencies installed!"
echo ""

echo "[2/2] Setting up Frontend..."
cd ../frontend
npm install
if [ $? -ne 0 ]; then
    echo "ERROR: npm install failed. Make sure Node.js 18+ is installed."
    exit 1
fi
echo "Frontend dependencies installed!"
echo ""

echo "============================================"
echo " Setup Complete!"
echo "============================================"
echo ""
echo "To start the backend:"
echo "  cd backend && dotnet run"
echo ""
echo "To start the frontend (new terminal):"
echo "  cd frontend && npm start"
echo ""
echo "Backend API: http://localhost:5000"
echo "Frontend:    http://localhost:4200"
echo "Swagger:     http://localhost:5000/swagger"
