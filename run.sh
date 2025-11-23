#!/bin/bash

# WebServer 실행 스크립트 (Mac/Linux)

echo "🚀 WebServer 시작 중..."

# 프로젝트 복원
echo "📦 NuGet 패키지 복원 중..."
dotnet restore WebServer.sln

# 빌드
echo "🔨 프로젝트 빌드 중..."
dotnet build WebServer.sln

# 실행
echo "▶️  WebServer.Api 실행 중..."
cd WebServer.Api
dotnet run

