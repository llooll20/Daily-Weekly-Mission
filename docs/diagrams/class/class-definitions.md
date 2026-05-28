# 프로그램 구조 (구조 다이어그램)

상태: 시작 전

1. 클래스 도출

도출된 클래스

Windows

- MainWindow
    
    앱 실행 시 사용자가 가장 먼저 보는 메인 화면이다. 일일 미션 영역, 주간 미션 영역, 타이틀 바를 가지며 타이틀 바에는 닫기, 미션 추가, 미션 기록 보기, 미션 삭제 기능이 배치된다.
    
- AddMissionWindow
    
    새로운 미션을 추가하는 화면이다. 사용자는 일일/주간 구분, 미션 내용, 수행 횟수, 기간 등의 정보를 입력한다.
    
- MissionHistoryWindow
    
    미션 기록을 확인하는 화면이다. 현재까지 등록된 미션의 내용, 완료율, 수행 빈도 등의 기록을 보여준다.
    

ViewModels

- MainViewModel
    
    MainWindow의 화면 상태와 동작을 관리하는 ViewModel이다. 미션 삭제, 미션 완료 처리, 미션 추가 창 열기, 미션 기록 창 열기 기능을 위해 DeleteMissionCommand, CompleteMissionCommand, OpenAddMissionCommand, OpenMissionHistoryCommand를 가진다.
    
- AddMissionViewModel
    
    AddMissionWindow의 입력 상태와 저장 동작을 관리하는 ViewModel이다. 사용자가 입력한 미션 정보를 저장하기 위해 SaveMissionCommand를 가진다.
    
- MissionHistoryViewModel
    
    MissionHistoryWindow에 표시할 미션 기록 데이터를 관리하는 ViewModel이다. MissionRepository에서 완료 기록을 조회하고 MissionProgress를 통해 미션별 진행 상태를 화면에 제공한다.
    

Models

- Mission
    
    하나의 미션을 표현하는 기본 데이터 모델이다. 일일/주간 여부, 미션 내용, 목표 횟수, 기간 등의 속성을 가진다.
    
- MissionType
    
    미션이 일일 미션인지 주간 미션인지 구분하는 열거형이다.
    
- MissionCompletionRecord
    
    MissionCompletionRecord는 미션 완료 결과를 영구적으로 저장하는 모델이다. 사용자가 미션을 완료할 때마다 완료 날짜, 해당 기간 키, 완료 횟수 등을 기록하며, 이후 MissionProgress를 계산하거나 MissionHistoryWindow에서 완료 이력을 표시할 때 사용된다.
    
- MissionProgress
    
    MissionProgress는 저장된 미션과 완료 기록을 바탕으로 계산되는 화면 표시용 진행 상태 모델이다. 현재 완료 횟수, 목표 횟수, 완료율, 완료 여부 등을 표현하며, 영구 저장되는 데이터가 아니라 MissionCompletionRecord를 통해 필요할 때 계산된다.
    

Repositories

- IMissionRepository
    
    미션 저장소의 동작을 정의하는 인터페이스이다. 저장 방식이 바뀌어도 ViewModel과 Command가 같은 방식으로 미션 데이터를 다룰 수 있게 한다.
    
- MissionRepository
    
    기본 미션 저장소 구현체이다. 앱에서 사용하는 미션과 완료 기록을 저장하고 조회한다.
    

Commands

- SaveMissionCommand
    
    AddMissionWindow에서 추가 버튼을 눌렀을 때 실행되는 Command이다. AddMissionViewModel의 입력값을 바탕으로 새로운 미션을 생성하고 저장한다.
    
- DeleteMissionCommand

    MainWindow에서 미션 삭제 버튼을 눌렀을 때 실행되는 Command이다. MainViewModel의 삭제 모드 상태를 변경하거나 선택된 미션을 삭제한다.

- CompleteMissionCommand

    MainWindow에서 미션을 완료 처리할 때 실행되는 Command이다. 선택된 미션의 완료 상태 또는 진행 횟수를 갱신하고 완료 기록을 저장한다.

- OpenAddMissionCommand

    MainWindow에서 미션 추가 버튼을 눌렀을 때 실행되는 Command이다. 새로운 미션을 입력할 수 있도록 AddMissionWindow를 연다.

- OpenMissionHistoryCommand

    MainWindow에서 미션 기록 보기 버튼을 눌렀을 때 실행되는 Command이다. 기존 미션의 완료 이력을 확인할 수 있도록 MissionHistoryWindow를 연다.

Services/Time

- IClock
    
    현재 날짜와 시간을 가져오는 방식을 정의하는 인터페이스이다. MainViewModel이나 MissionPeriodService가 DateTime.Now를 직접 사용하지 않고, 이 인터페이스를 통해 현재 시간을 받아올 수 있게 한다.
    
- SystemClock

    실제 PC의 현재 날짜와 시간을 제공하는 IClock 구현체이다. 앱이 실행 중일 때 MissionPeriodService가 현재 날짜를 계산할 수 있도록 시스템 시간을 전달한다.

- FakeClock

    테스트에서 원하는 날짜와 시간을 직접 지정하기 위한 IClock 구현체이다. 실제 PC 시간과 상관없이 특정 날짜를 기준으로 일일/주간 계산을 확인할 때 사용한다.

- MissionPeriodService

    MainViewModel이 현재 날짜를 기준으로 일일 미션과 주간 미션의 기준 기간을 파악할 때 사용하는 서비스이다. IClock을 통해 현재 날짜를 가져오고, 이를 바탕으로 오늘의 DailyKey와 이번 주의 WeeklyKey를 계산하여 MainViewModel에 제공한다.
