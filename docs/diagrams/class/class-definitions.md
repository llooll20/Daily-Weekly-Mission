# 프로그램 구조 (구조 다이어그램)

상태: 시작 전

1. 클래스 도출

도출된 클래스

Windows

- MainWindow
    
    앱 실행 시 사용자가 가장 먼저 보는 메인 화면이다. 일일 미션 영역, 주간 미션 영역, 타이틀 바를 가지며 타이틀 바에는 닫기, 미션 추가, 미션 기록 보기, 미션 삭제 기능이 배치된다.  
    속성: MainViewModel DataContext  
    Close()  
    		- 설명: 현재 메인 창을 닫는다.  
    		- 입력: 없음  
    		- 출력: 없음  
    
- AddMissionWindow
    
    새로운 미션을 추가하는 화면이다. 사용자는 일일/주간 구분, 미션 내용, 수행 횟수, 기간 등의 정보를 입력한다.  
    속성: AddMissionViewModel DataContext  
    Close()  
    		- 설명: 현재 미션 추가 창을 닫는다.  
    		- 입력: 없음  
    		- 출력: 없음  
    
- MissionHistoryWindow
    
    미션 기록을 확인하는 화면이다. 현재까지 등록된 미션의 내용, 완료율, 수행 빈도 등의 기록을 보여준다.  
    속성: MissionHistoryViewModel DataContext  
    Close()  
    		- 설명: 현재 미션 기록 창을 닫는다.  
    		- 입력: 없음  
    		- 출력: 없음  
    

ViewModels

- MainViewModel
    
    MainWindow의 화면 상태와 동작을 관리하는 ViewModel이다. 미션 삭제, 미션 완료 처리, 미션 추가 창 열기, 미션 기록 창 열기 기능을 위해 DeleteMissionCommand, CompleteMissionCommand, OpenAddMissionCommand, OpenMissionHistoryCommand를 가진다.  
    속성: DailyMissions, WeeklyMissions, IsDeleteMode, DeleteMissionCommand, CompleteMissionCommand, OpenAddMissionCommand, OpenMissionHistoryCommand  
    LoadMissions()  
    		- 설명: 저장소에서 미션 목록을 불러와 일일 미션과 주간 미션으로 나눈다.  
    		- 입력: 없음  
    		- 출력: 없음  

    EnterDeleteMode()  
    		- 설명: 메인 화면을 미션 삭제 모드로 전환한다.  
    		- 입력: 없음  
    		- 출력: 없음  

    CompleteMission(Mission mission)  
    		- 설명: 선택된 미션의 완료 기록을 추가하고 진행 상태를 갱신한다.  
    		- 입력: 완료 처리할 Mission  
    		- 출력: 없음  

    DeleteMission(Mission mission)  
    		- 설명: 선택된 미션을 저장소에서 삭제하고 화면 목록을 갱신한다.  
    		- 입력: 삭제할 Mission  
    		- 출력: 없음  
    
- AddMissionViewModel
    
    AddMissionWindow의 입력 상태와 저장 동작을 관리하는 ViewModel이다. 사용자가 입력한 미션 정보를 저장하기 위해 SaveMissionCommand를 가진다.  
    속성: Title, Content, SelectedMissionType, TargetCount, StartDate, EndDate, SelectedDays, SaveMissionCommand  
    CreateMission()  
    		- 설명: 입력된 값을 바탕으로 저장 가능한 Mission 객체를 만든다.  
    		- 입력: 없음  
    		- 출력: Mission  

    CanSave()  
    		- 설명: 현재 입력값으로 미션을 저장할 수 있는지 검사한다.  
    		- 입력: 없음  
    		- 출력: bool  

    SaveMission()  
    		- 설명: 생성한 미션을 저장소에 저장한다.  
    		- 입력: 없음  
    		- 출력: 없음  
    
- MissionHistoryViewModel
    
    MissionHistoryWindow에 표시할 미션 기록 데이터를 관리하는 ViewModel이다. MissionRepository에서 완료 기록을 조회하고 MissionProgress를 통해 미션별 진행 상태를 화면에 제공한다.  
    속성: Missions, MissionProgresses, SelectedMission  
    LoadHistory()  
    		- 설명: 저장소에서 미션과 완료 기록을 불러와 기록 화면 데이터를 준비한다.  
    		- 입력: 없음  
    		- 출력: 없음  

    CalculateProgress()  
    		- 설명: 미션과 완료 기록을 바탕으로 진행 상태 목록을 계산한다.  
    		- 입력: 없음  
    		- 출력: IEnumerable<MissionProgress>  
    

Models

- Mission
    
    하나의 미션을 표현하는 기본 데이터 모델이다. 일일/주간 여부, 미션 내용, 목표 횟수, 기간 등의 속성을 가진다.  
    속성: Id, Title, Content, Type, TargetCount, StartDate, EndDate, ScheduledDays, CreatedAt, IsActive  
    IsWeekly()  
    		- 설명: 현재 미션이 주간 미션인지 확인한다.  
    		- 입력: 없음  
    		- 출력: bool  

    HasScheduleOn(DayOfWeek dayOfWeek)  
    		- 설명: 지정한 요일이 미션 수행 요일에 포함되는지 확인한다.  
    		- 입력: 확인할 DayOfWeek  
    		- 출력: bool  
    
- MissionType
    
    미션이 일일 미션인지 주간 미션인지 구분하는 열거형이다.  
    속성: Daily, Weekly  
    
- MissionCompletionRecord
    
    MissionCompletionRecord는 미션 완료 결과를 영구적으로 저장하는 모델이다. 사용자가 미션을 완료할 때마다 완료 날짜, 해당 기간 키, 완료 횟수 등을 기록하며, 이후 MissionProgress를 계산하거나 MissionHistoryWindow에서 완료 이력을 표시할 때 사용된다.  
    속성: Id, MissionId, CompletedDate, PeriodKey, CompletedCount, CreatedAt  
    IsInPeriod(DateOnly periodKey)  
    		- 설명: 완료 기록이 지정한 기간 키에 속하는지 확인한다.  
    		- 입력: 비교할 DateOnly periodKey  
    		- 출력: bool  
    
- MissionProgress
    
    MissionProgress는 저장된 미션과 완료 기록을 바탕으로 계산되는 화면 표시용 진행 상태 모델이다. 현재 완료 횟수, 목표 횟수, 완료율, 완료 여부 등을 표현하며, 영구 저장되는 데이터가 아니라 MissionCompletionRecord를 통해 필요할 때 계산된다.  
    속성: MissionId, CurrentCount, TargetCount, CompletionRate, IsCompleted  
    GetRemainingCount()  
    		- 설명: 목표 횟수까지 남은 수행 횟수를 계산한다.  
    		- 입력: 없음  
    		- 출력: int  

    HasPartialProgress()  
    		- 설명: 완료 전이지만 일부 진행된 상태인지 확인한다.  
    		- 입력: 없음  
    		- 출력: bool  
    

Repositories

- IMissionRepository
    
    미션 저장소의 동작을 정의하는 인터페이스이다. 저장 방식이 바뀌어도 ViewModel과 Command가 같은 방식으로 미션 데이터를 다룰 수 있게 한다.  
    속성: 없음  
    GetAllMissions()  
    		- 설명: 저장된 모든 미션을 조회한다.  
    		- 입력: 없음  
    		- 출력: IEnumerable<Mission>  

    GetMissionById(Guid missionId)  
    		- 설명: 지정한 식별자를 가진 미션을 조회한다.  
    		- 입력: 조회할 Guid missionId  
    		- 출력: Mission  

    SaveMission(Mission mission)  
    		- 설명: 미션을 새로 저장하거나 기존 미션을 갱신한다.  
    		- 입력: 저장할 Mission  
    		- 출력: 없음  

    DeleteMission(Guid missionId)  
    		- 설명: 지정한 식별자의 미션을 삭제한다.  
    		- 입력: 삭제할 Guid missionId  
    		- 출력: 없음  

    GetCompletionRecords(Guid missionId)  
    		- 설명: 지정한 미션의 완료 기록 목록을 조회한다.  
    		- 입력: 조회할 Guid missionId  
    		- 출력: IEnumerable<MissionCompletionRecord>  

    SaveCompletionRecord(MissionCompletionRecord record)  
    		- 설명: 미션 완료 기록을 저장한다.  
    		- 입력: 저장할 MissionCompletionRecord  
    		- 출력: 없음  
    
- MissionRepository
    
    기본 미션 저장소 구현체이다. 앱에서 사용하는 미션과 완료 기록을 저장하고 조회한다.  
    속성: 없음  
    GetAllMissions()  
    		- 설명: 저장된 모든 미션을 조회한다.  
    		- 입력: 없음  
    		- 출력: IEnumerable<Mission>  

    GetMissionById(Guid missionId)  
    		- 설명: 지정한 식별자를 가진 미션을 조회한다.  
    		- 입력: 조회할 Guid missionId  
    		- 출력: Mission  

    SaveMission(Mission mission)  
    		- 설명: 미션을 새로 저장하거나 기존 미션을 갱신한다.  
    		- 입력: 저장할 Mission  
    		- 출력: 없음  

    DeleteMission(Guid missionId)  
    		- 설명: 지정한 식별자의 미션을 삭제한다.  
    		- 입력: 삭제할 Guid missionId  
    		- 출력: 없음  

    GetCompletionRecords(Guid missionId)  
    		- 설명: 지정한 미션의 완료 기록 목록을 조회한다.  
    		- 입력: 조회할 Guid missionId  
    		- 출력: IEnumerable<MissionCompletionRecord>  

    SaveCompletionRecord(MissionCompletionRecord record)  
    		- 설명: 미션 완료 기록을 저장한다.  
    		- 입력: 저장할 MissionCompletionRecord  
    		- 출력: 없음  
    

Commands

- SaveMissionCommand
    
    AddMissionWindow에서 추가 버튼을 눌렀을 때 실행되는 Command이다. AddMissionViewModel의 입력값을 바탕으로 새로운 미션을 생성하고 저장한다.  
    속성: 없음  
    CanExecute(object parameter)  
    		- 설명: 현재 상태에서 미션 저장 명령을 실행할 수 있는지 확인한다.  
    		- 입력: Command parameter  
    		- 출력: bool  

    Execute(object parameter)  
    		- 설명: AddMissionViewModel의 입력값을 바탕으로 미션을 저장한다.  
    		- 입력: Command parameter  
    		- 출력: 없음  
    
- DeleteMissionCommand

    MainWindow에서 미션 삭제 버튼을 눌렀을 때 실행되는 Command이다. MainViewModel의 삭제 모드 상태를 변경하거나 선택된 미션을 삭제한다.  
    속성: 없음  
    CanExecute(object parameter)  
    		- 설명: 현재 상태에서 미션 삭제 명령을 실행할 수 있는지 확인한다.  
    		- 입력: Command parameter  
    		- 출력: bool  

    Execute(object parameter)  
    		- 설명: 삭제 모드로 전환하거나 선택된 미션을 삭제한다.  
    		- 입력: Command parameter  
    		- 출력: 없음  

- CompleteMissionCommand

    MainWindow에서 미션을 완료 처리할 때 실행되는 Command이다. 선택된 미션의 완료 상태 또는 진행 횟수를 갱신하고 완료 기록을 저장한다.  
    속성: 없음  
    CanExecute(object parameter)  
    		- 설명: 현재 상태에서 미션 완료 명령을 실행할 수 있는지 확인한다.  
    		- 입력: Command parameter  
    		- 출력: bool  

    Execute(object parameter)  
    		- 설명: 선택된 미션의 완료 기록을 저장하고 진행 상태를 갱신한다.  
    		- 입력: Command parameter  
    		- 출력: 없음  

- OpenAddMissionCommand

    MainWindow에서 미션 추가 버튼을 눌렀을 때 실행되는 Command이다. 새로운 미션을 입력할 수 있도록 AddMissionWindow를 연다.  
    속성: 없음  
    CanExecute(object parameter)  
    		- 설명: 현재 상태에서 미션 추가 창 열기 명령을 실행할 수 있는지 확인한다.  
    		- 입력: Command parameter  
    		- 출력: bool  

    Execute(object parameter)  
    		- 설명: AddMissionWindow를 열어 새 미션을 입력할 수 있게 한다.  
    		- 입력: Command parameter  
    		- 출력: 없음  

- OpenMissionHistoryCommand

    MainWindow에서 미션 기록 보기 버튼을 눌렀을 때 실행되는 Command이다. 기존 미션의 완료 이력을 확인할 수 있도록 MissionHistoryWindow를 연다.  
    속성: 없음  
    CanExecute(object parameter)  
    		- 설명: 현재 상태에서 미션 기록 창 열기 명령을 실행할 수 있는지 확인한다.  
    		- 입력: Command parameter  
    		- 출력: bool  

    Execute(object parameter)  
    		- 설명: MissionHistoryWindow를 열어 완료 이력을 확인할 수 있게 한다.  
    		- 입력: Command parameter  
    		- 출력: 없음  

Services/Time

- IClock
    
    현재 날짜와 시간을 가져오는 방식을 정의하는 인터페이스이다. MainViewModel이나 MissionPeriodService가 DateTime.Now를 직접 사용하지 않고, 이 인터페이스를 통해 현재 시간을 받아올 수 있게 한다.  
    속성: Now, Today  
    
- SystemClock

    실제 PC의 현재 날짜와 시간을 제공하는 IClock 구현체이다. 앱이 실행 중일 때 MissionPeriodService가 현재 날짜를 계산할 수 있도록 시스템 시간을 전달한다.  
    속성: Now, Today  

- FakeClock

    테스트에서 원하는 날짜와 시간을 직접 지정하기 위한 IClock 구현체이다. 실제 PC 시간과 상관없이 특정 날짜를 기준으로 일일/주간 계산을 확인할 때 사용한다.  
    속성: Now, Today  
    SetNow(DateTimeOffset now)  
    		- 설명: 테스트에서 사용할 현재 시간을 지정한 값으로 변경한다.  
    		- 입력: 설정할 DateTimeOffset now  
    		- 출력: 없음  

- MissionPeriodService

    MainViewModel이 현재 날짜를 기준으로 일일 미션과 주간 미션의 기준 기간을 파악할 때 사용하는 서비스이다. IClock을 통해 현재 날짜를 가져오고, 이를 바탕으로 오늘의 DailyKey와 이번 주의 WeeklyKey를 계산하여 MainViewModel에 제공한다.  
    속성: 없음  
    GetDailyKey()  
    		- 설명: 현재 날짜를 기준으로 일일 미션의 기간 키를 반환한다.  
    		- 입력: 없음  
    		- 출력: DateOnly  

    GetWeeklyKey()  
    		- 설명: 현재 날짜를 기준으로 주간 미션의 기간 키를 반환한다.  
    		- 입력: 없음  
    		- 출력: DateOnly  

    GetWeeklyKey(DateOnly date)  
    		- 설명: 지정한 날짜가 속한 주간 미션의 기간 키를 반환한다.  
    		- 입력: 기준 DateOnly date  
    		- 출력: DateOnly  

    IsDailyMissionDate(DateOnly missionDate)  
    		- 설명: 지정한 날짜가 오늘의 일일 미션 기간에 해당하는지 확인한다.  
    		- 입력: 확인할 DateOnly missionDate  
    		- 출력: bool  

    IsWeeklyMissionDate(DateOnly missionDate)  
    		- 설명: 지정한 날짜가 현재 주간 미션 기간에 해당하는지 확인한다.  
    		- 입력: 확인할 DateOnly missionDate  
    		- 출력: bool  

