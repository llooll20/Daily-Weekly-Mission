using DailyWeeklyMission.Models;
using DailyWeeklyMission.Services.Time;
using System;
using System.Collections.Generic;

namespace DailyWeeklyMission.Repositories
{
    // 미션 데이터를 관리하는 인터페이스
    public interface IMissionRepository
    {
        public MissionPeriodService missionPeriodService { get; }
        IEnumerable <Mission> GetAllMissions();
        Mission GetMissionById(Guid missionId);
        void SaveMission(Mission mission);
        void DeleteMission(Guid missionId);
        IEnumerable<MissionCompletionRecord> GetCompletionRecords(Guid missionId);
        void SaveCompletionRecord(MissionCompletionRecord record);

        void ClearMissions();

        
        void IncrementCurrentCount(Mission mission);
        void IncrementWeeklyDays(Mission mission);

        void ActivateMission(Mission mission);
        void DisActivateMission(Mission mission);
        public void RefreshMission(Mission mission);



    }
}
