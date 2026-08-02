using DailyWeeklyMission.Models;
using DailyWeeklyMission.Properties;
using DailyWeeklyMission.Services.Time;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;

namespace DailyWeeklyMission.Repositories
{
    // 프로그램 내 저장 방식을 담당하는 MissionRepository 클래스
    public class MissionRepository : IMissionRepository
    {
        public MissionPeriodService missionPeriodService { get; } = new MissionPeriodService(new SystemClock());

        private readonly JsonSerializerOptions jsonOptions = new()
        {
            WriteIndented = true
        };
            
        public IEnumerable<Mission> GetAllMissions() => GetMissions();

        public Mission GetMissionById(Guid missionId)
        {
            Mission? mission = GetMissions().FirstOrDefault(m => m.Id == missionId);

            if (mission == null)
            {
                throw new InvalidOperationException("Mission not found.");
            }

            return mission;
        }

        public void SaveMission(Mission mission)
        {
            List<Mission> missions = GetMissions();
            int missionIndex = missions.FindIndex(m => m.Id == mission.Id);

            if (missionIndex >= 0)
            {
                missions[missionIndex] = mission;
            }
            else
            {
                missions.Add(mission);
            }

            SaveMissions(missions);
        }

        public void DeleteMission(Guid missionId)
        {
            List<Mission> missions = GetMissions();
            Mission? mission = missions.FirstOrDefault(m => m.Id == missionId);

            if (mission == null)
            {
                return;
            }

            missions.Remove(mission);
            SaveMissions(missions);
        }

        public IEnumerable<MissionCompletionRecord> GetCompletionRecords(Guid missionId)
        {
            return GetCompletionRecords()
                .Where(record => record.MissionId == missionId);
        }

        public void SaveCompletionRecord(MissionCompletionRecord record)
        {
            List<MissionCompletionRecord> records = GetCompletionRecords();
            int recordIndex = records.FindIndex(r => r.Id == record.Id);

            if (recordIndex >= 0)
            {
                records[recordIndex] = record;
            }
            else
            {
                records.Add(record);
            }

            SaveCompletionRecords(records);
        }

        private List<Mission> GetMissions()
        {
            string jsonData = new Settings().MissionsJson;
            return DeserializeList<Mission>(jsonData);
        }

        private void SaveMissions(IEnumerable<Mission> missions)
        {
            Settings settings = new();
            settings.MissionsJson = JsonSerializer.Serialize(missions, jsonOptions);
            settings.Save();
        }

        private List<MissionCompletionRecord> GetCompletionRecords()
        {
            string jsonData = new Settings().CompletionRecordsJson;
            return DeserializeList<MissionCompletionRecord>(jsonData);
        }

        private void SaveCompletionRecords(IEnumerable<MissionCompletionRecord> records)
        {
            Settings settings = new();
            settings.CompletionRecordsJson = JsonSerializer.Serialize(records, jsonOptions);
            settings.Save();
        }

        private static List<T> DeserializeList<T>(string jsonData)
        {
            if (string.IsNullOrWhiteSpace(jsonData))
            {
                return new List<T>();
            }

            return JsonSerializer.Deserialize<List<T>>(jsonData) ?? new List<T>();
        }
        public void ClearMissions()
        {
            SaveMissions(new List<Mission>());
        }

        public void IncrementCurrentCount(Mission mission)
        {
            Debug.WriteLine("IncrementCurrentCount 호출");
            Debug.WriteLine($"Before : {mission.CurrentCount}");
            if (mission.CurrentCount >= mission.TargetCount)
                {
                    return;
                }

                mission.CurrentCount++;
            Debug.WriteLine($"After : {mission.CurrentCount}");

            if (mission.CurrentCount == mission.TargetCount)
                {
                    mission.CompletedDates.Add(DateTime.Now);
                    mission.IsCompleted = true;
                }
            mission.LastProgressDate = DateTime.Today;

            SaveMission(mission);
        }

        public void IncrementWeeklyDays(Mission mission)
        {
            if (!mission.HasScheduleOn(DateTime.Today.DayOfWeek))
                return;

            if (mission.CompletedDates.Any(d => d.Date == DateTime.Today))
                return;

      
             mission.CurrentCount++;
             mission.CompletedDates.Add(DateTime.Now);
            if (mission.CurrentCount == mission.TargetCount)
            {
                mission.IsCompleted = true;
            }
            mission.LastProgressDate = DateTime.Today;

            SaveMission(mission);
        }

        public void ActivateMission(Mission mission)
        {
            mission.IsActive = true;
            SaveMission(mission);
        }

        public void DisActivateMission(Mission mission)
        {
            mission.IsActive = false;
            SaveMission(mission);
        }

        //일,주간 미션의 날짜가 넘어갈 때, TargetCount=0 으로 초기화 
        public void RefreshMission(Mission mission)
        {
            switch (mission.Type)
            {
                case MissionType.Daily:
                    if (mission.LastProgressDate.Date != DateTime.Today)
                    {
                        mission.CurrentCount = 0;
                        mission.IsCompleted = false;
                    }
                    break;

                case MissionType.Weekly:
                    if (!missionPeriodService.IsWeeklyMissionDate(DateOnly.FromDateTime(mission.LastProgressDate)))
                    {
                        mission.CurrentCount = 0;
                        mission.IsCompleted = false;
                    }
                    if (mission.WeeklyMode == WeeklyMissionMode.Days)
                    {
                        if(mission.LastProgressDate.Date != DateTime.Today)
                        {
                            mission.IsCompleted= false;
                        }
                    }
                    break;
            }
        }

    }
  
}
