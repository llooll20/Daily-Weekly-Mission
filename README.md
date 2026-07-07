# Daily Weekly Mission

Daily Weekly Mission은 사용자가 매일 또는 매주 반복해서 수행할 미션을 등록하고, 완료 상태와 진행 기록을 관리하기 위한 WPF 기반 데스크톱 애플리케이션입니다.

현재 프로젝트는 기존 WPF 학습용 예제 구조에서 Mission 관리 앱 구조로 전환하는 단계입니다. UI 목업, 프로젝트 개요, 유스케이스/클래스 다이어그램, 저장 형식 문서를 먼저 정리했고, 코드도 Mission 도메인 중심의 Model, Repository, ViewModel, Command 구조로 재구성하고 있습니다.

## 주요 기능 목표

- 일일 미션과 주간 미션 구분 등록
- 미션 목록 조회
- 미션 완료 상태 체크
- 미션 삭제
- 미션별 완료 기록과 진행 상태 확인
- 일일/주간 기준에 따른 진행률 계산

## 현재 진행 상태

- WPF 프로젝트 폴더를 `src/DailyWeeklyMission`으로 정리
- `DesignPattern/MVVM` 중간 폴더 제거
- `Models`, `ViewModels`, `Views`, `Commands`, `Repositories`, `Services`를 프로젝트 루트 하위로 이동
- WPF 프로젝트 생성 및 기본 화면 구성
- 메인 화면 목업 작성
- 일일 미션 추가 화면 목업 작성
- 주간 미션 추가 화면 목업 작성
- 프로젝트 개요 문서 작성
- 유스케이스 다이어그램 작성
- 클래스 다이어그램 및 클래스 정의 문서 작성
- Mission 중심의 Model 계층 추가
- `IMissionRepository`, `MissionRepository` 기반 저장소 구조 추가
- `Properties.Settings` 기반 JSON 문자열 저장 구조 추가
- Mission 관련 Command/ViewModel 파일 추가
- 기존 Person 예제 기반 MVVM 파일 정리

## 프로젝트 구조

```text
src/
  DailyWeeklyMission/
    Commands/
    Models/
    Repositories/
    Services/
      Time/
    ViewModels/
    Views/
    App.xaml
    MainWindow.xaml
    DailyWeeklyMission.csproj
  DailyWeeklyMission.slnx
docs/
  overview/
  diagrams/
  mockups/
  storage/
```

## 핵심 도메인 모델

- `Mission`: 미션의 제목, 내용, 유형, 목표 횟수, 기간, 요일 조건, 활성 상태를 표현합니다.
- `MissionType`: 일일 미션과 주간 미션을 구분합니다.
- `MissionCompletionRecord`: 미션 완료 날짜, 기간 키, 완료 횟수를 기록합니다.
- `MissionProgress`: 저장된 미션과 완료 기록을 바탕으로 화면에 표시할 진행 상태를 표현합니다.

## 저장 방식

초기 구현은 WPF의 `Properties.Settings`에 JSON 문자열을 저장하는 방식입니다.

- `MissionsJson`: 미션 목록 저장
- `CompletionRecordsJson`: 미션 완료 기록 저장

저장소 접근은 `IMissionRepository` 인터페이스를 통해 분리되어 있어, 이후 JSON 파일이나 SQLite 같은 다른 저장 방식으로 변경할 수 있도록 확장 여지를 남겨두었습니다.

자세한 저장 구조는 [storage-format.md](docs/storage/storage-format.md)를 참고합니다.

## 문서

- [프로젝트 개요](docs/overview/project-overview.md)
- [저장 형식](docs/storage/storage-format.md)
- [클래스 다이어그램](docs/diagrams/class/class-diagram.md)
- [클래스 정의](docs/diagrams/class/class-definitions.md)
- [유스케이스 다이어그램](docs/diagrams/usecase/usecase_diagram.md)
- [화면 목업](docs/mockups/)

## 기술 스택

- C#
- WPF
- .NET 10
- MVVM Pattern
- `System.Text.Json`
- `Properties.Settings`

## 실행 방법

1. Visual Studio 또는 .NET SDK가 설치된 환경에서 프로젝트를 엽니다.
2. `src/DailyWeeklyMission.slnx` 솔루션을 실행합니다.
3. 시작 프로젝트가 `DailyWeeklyMission`인지 확인합니다.
4. 빌드 후 실행합니다.

```powershell
dotnet build src\DailyWeeklyMission.slnx
```

## 다음 작업 후보

- `MainViewModel`에 미션 목록 바인딩 추가
- Command와 실제 화면 버튼 연결
- 미션 추가 창에서 입력값 검증 및 저장 처리
- 완료 체크/삭제 기능 구현
- 미션 기록 화면 구현
- 목업 데이터를 실제 Repository 데이터로 교체
- 빌드 오류 및 바인딩 오류 점검