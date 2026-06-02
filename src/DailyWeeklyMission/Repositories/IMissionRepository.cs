using System;
using System.Collections.Generic;
using DailyWeeklyMission.Models;

namespace DailyWeeklyMission.Repositories
{
    public interface IMissionRepository
    {
        IEnumerable<Mission> GetAllMissions();
        Mission GetMissionById(Guid missionId);
        void SaveMission(Mission mission);
        void DeleteMission(Guid missionId);
        IEnumerable<MissionCompletionRecord> GetCompletionRecords(Guid missionId);
        void SaveCompletionRecord(MissionCompletionRecord record);
    }
}
