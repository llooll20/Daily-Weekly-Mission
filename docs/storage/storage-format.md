# 저장 형식

## 저장 방식

초기 구현에서는 WPF의 `Properties.Settings`를 사용해 미션 데이터를 저장한다.

저장소 역할은 `MissionRepository`가 담당하며, ViewModel과 Command는 저장 방식이 Settings인지 JSON 파일인지 알지 못한다. 이후 저장 방식이 JSON 파일로 변경되더라도 `IMissionRepository` 인터페이스를 유지하면 외부 구조 변경을 줄일 수 있다.

## Settings 저장 항목

Settings에는 미션 목록과 완료 기록 목록을 JSON 문자열로 저장한다.

```text
MissionsJson
CompletionRecordsJson
```

## MissionsJson

`MissionsJson`은 `Mission` 목록을 JSON 배열 형태로 직렬화한 문자열이다. 미션의 기본 정보와 일일/주간 구분, 목표 횟수, 기간, 요일 조건 등을 저장한다.

```json
[
  {
    "id": "b3a0e1d4-3a4f-4c7a-9d21-2f3b9a0f2e11",
    "title": "운동하기",
    "content": "하루 30분 운동",
    "type": "Daily",
    "targetCount": 1,
    "startDate": "2026-05-31",
    "endDate": "2026-06-30",
    "scheduledDays": [],
    "createdAt": "2026-05-31T10:00:00+09:00",
    "isActive": true
  },
  {
    "id": "7a1c0f29-31f3-4a2d-a4b5-87f2cc51aa90",
    "title": "방 청소",
    "content": "주 2회 방 청소하기",
    "type": "Weekly",
    "targetCount": 2,
    "startDate": "2026-05-25",
    "endDate": "2026-06-30",
    "scheduledDays": ["Monday", "Thursday"],
    "createdAt": "2026-05-31T10:05:00+09:00",
    "isActive": true
  }
]
```

## CompletionRecordsJson

`CompletionRecordsJson`은 `MissionCompletionRecord` 목록을 JSON 배열 형태로 직렬화한 문자열이다. 사용자가 미션을 완료할 때마다 완료 날짜, 기간 키, 완료 횟수를 기록한다.

```json
[
  {
    "id": "72e3b36b-1c32-4b3b-b9fd-fde1d3f1f001",
    "missionId": "b3a0e1d4-3a4f-4c7a-9d21-2f3b9a0f2e11",
    "completedDate": "2026-05-31",
    "periodKey": "2026-05-31",
    "completedCount": 1,
    "createdAt": "2026-05-31T20:30:00+09:00"
  },
  {
    "id": "88a24d5e-d2a1-4c75-ae01-9cf313d8e002",
    "missionId": "7a1c0f29-31f3-4a2d-a4b5-87f2cc51aa90",
    "completedDate": "2026-05-31",
    "periodKey": "2026-05-25",
    "completedCount": 1,
    "createdAt": "2026-05-31T20:35:00+09:00"
  }
]
```

## 기간 키

`periodKey`는 미션 완료 기록이 어느 일일/주간 기간에 속하는지 나타내는 값이다.

일일 미션은 완료한 날짜가 그대로 기간 키가 된다.

```text
completedDate = 2026-05-31
periodKey = 2026-05-31
```

주간 미션은 해당 날짜가 속한 주의 시작일을 기간 키로 사용한다.

```text
completedDate = 2026-05-31
periodKey = 2026-05-25
```

## 저장 대상과 계산 대상

영구 저장 대상은 `Mission`과 `MissionCompletionRecord`이다.

```text
Mission
MissionCompletionRecord
```

`MissionProgress`는 저장하지 않는다. 저장된 `Mission`과 `MissionCompletionRecord`를 바탕으로 화면에 표시할 때 계산한다.
