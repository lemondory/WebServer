## 📑 주요 기술 스택
| 영역            | 기술                                      |
|-----------------|-------------------------------------------|
| Language        | C# (.NET 8)                               |
| Web API         | ASP.NET Core                              |
| Database        | MySQL + Dapper.FastCrud                   |
| Cache           | Redis                                     |
| Authentication  | JWT (Access Token / Refresh Token)        |
| Logging         | Serilog (File, Console, Seq 지원)          |
| WebSocket       | ASP.NET Core WebSocket                    |
| Architecture    | Clean Architecture (Domain, Application, Infrastructure, Persistence, API) |
| Mediation       | MediatR (Command/Query 분리)              |
| Mapping         | AutoMapper                               |

## 📑 주요 기능
| 기능            | 설명                                      |
|-----------------|-------------------------------------------|
| JWT 인증        | Access Token + Refresh Token 구조 지원    |
| WebSocket 통신  | 채팅, 파티, 실시간 동기화 처리             |
| Redis 캐시      | 빠른 데이터 조회 및 세션 관리              |
| CQRS 적용       | Command/Query 구조 완전 분리               |
| Named Lock      | 데이터베이스 락을 통한 동시성 제어         |
| Serilog 로깅    | Console, 파일, Seq로 로깅 출력             |
| 자동 매핑       | DTO ↔ Entity 변환 (AutoMapper 사용)        |
| Dapper.FastCrud | 빠르고 간결한 DB CRUD 처리                |
| 에러 핸들링     | 미들웨어를 통한 예외 통합 처리             |

## 📁 폴더 구조

```plaintext
/WebServer
 ├── WebServer.Api           # Web API 엔트리 포인트 (컨트롤러, Swagger)
 ├── WebServer.Application   # CQRS(Command, Query), DTO, 서비스 로직
 ├── WebServer.Contracts     # Domain, Application, Infrastructure 간의 의존성을 분리하기 위한 공통 인터페이스 모음
 ├── WebServer.Domain        # 엔티티, 인터페이스, Enum 등 핵심 도메인
 ├── WebServer.Infrastructure # 외부 연동: JWT, Redis, Serilog, WebSocket 핸들러
 ├── WebServer.Persistance    # Dapper 기반 DB 레포지토리 구현
 ├── WebServer.Shared         # 클라이언트와 공유할 DTO, Enum, Response 모델 등
 └── README.md                 # 프로젝트 설명 문서

## 🚀 빌드 및 실행 (Mac)

### 필수 요구사항
- .NET 8.0 SDK 이상
- MySQL (또는 다른 데이터베이스)
- Redis (선택사항)

### 빌드 방법

#### 1. 명령어로 빌드 및 실행
```bash
# 프로젝트 복원
dotnet restore WebServer.sln

# 빌드
dotnet build WebServer.sln

# 실행
cd WebServer.Api
dotnet run
```

#### 2. 실행 스크립트 사용
```bash
./run.sh
```

#### 3. VS Code에서 디버깅
1. VS Code에서 프로젝트 폴더 열기
2. `F5` 키를 누르거나 디버그 패널에서 ".NET Core Launch (WebServer.Api)" 선택
3. 브라우저가 자동으로 열리며 Swagger UI가 표시됩니다

### 접속 URL
- HTTP: `http://localhost:5290`
- HTTPS: `https://localhost:7179`
- Swagger UI: `http://localhost:5290/swagger`

### 디버깅 설정
- `.vscode/launch.json`: VS Code 디버깅 설정
- `.vscode/tasks.json`: 빌드 작업 설정
- `WebServer.Api/Properties/launchSettings.json`: 실행 프로파일 설정

## 📑 확장 가능한 기능
이 프로젝트는 초기 샘플 프로젝트입니다. 다음 기능들을 필요에 따라 추가할 수 있습니다:
- JWT 인증 (Access Token + Refresh Token)
- WebSocket 통신
- Redis 캐시
- 데이터베이스 연동 (MySQL, PostgreSQL 등)
- 소셜 로그인 (Google, Apple, Kakao 등)
- Docker 배포
- Kubernetes 대응
