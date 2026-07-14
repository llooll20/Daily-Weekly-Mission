using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using DailyWeeklyMission.Models;
using DailyWeeklyMission.Properties;

namespace DailyWeeklyMission.Repositories
{
    // 프로그램 내 저장 방식을 담당하는 MissionRepository 클래스
    public class MissionRepository : IMissionRepository
    {
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
            if (mission.CurrentCount >= mission.TargetCount)
            {
                mission.IsActive = false;
                return;
            }

            mission.CurrentCount++;

            if (mission.CurrentCount >= mission.TargetCount)
            {
                mission.IsActive = false;
            }

            SaveMission(mission);
        }
    }
  
}
