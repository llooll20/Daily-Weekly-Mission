using System;
using System.Collections.Generic;
using DailyWeeklyMission.Models;

namespace DailyWeeklyMission.Repositories
{
    // 미션 데이터를 관리하는 인터페이스
    public interface IMissionRepository
    {
        IEnumerable<Mission> GetAllMissions();
        Mission GetMissionById(Guid missionId);
        void SaveMission(Mission mission);
        void DeleteMission(Guid missionId);
        IEnumerable<MissionCompletionRecord> GetCompletionRecords(Guid missionId);
        void SaveCompletionRecord(MissionCompletionRecord record);

        void ClearMissions();

        public void IncrementCurrentCount(Mission mission);
    }
}
