import { useState } from 'react';
import { History, Trash2, X } from 'lucide-react';
import { useNavigate } from 'react-router';
import { TitleBarButton } from '../components/TitleBarButton';
import { MissionSection } from '../components/MissionSection';
import { MissionCard } from '../components/MissionCard';

export function MainWindow() {
  const navigate = useNavigate();
  const [deleteMode, setDeleteMode] = useState(false);
  const [selectedMission, setSelectedMission] = useState<string | null>(null);

  const [weeklyMissionList, setWeeklyMissionList] = useState([
    {
      id: 'w1',
      title: 'Exercise',
      content: 'Workout at the gym',
      current: 1,
      target: 3,
      completed: false,
      type: 'weekly-count' as const
    },
    {
      id: 'w2',
      title: 'Cleaning',
      content: 'Clean the house',
      current: 1,
      target: 2,
      completed: false,
      type: 'weekly-days' as const,
      scheduledDays: [1, 4],
      currentDay: 2
    },
    {
      id: 'w3',
      title: 'Read Books',
      content: 'Read for 30 minutes',
      current: 5,
      target: 5,
      completed: true,
      type: 'weekly-count' as const
    }
  ]);

  const [dailyMissionList, setDailyMissionList] = useState([
    {
      id: 'd1',
      title: 'Drink Water',
      content: '8 glasses of water',
      current: 5,
      target: 8,
      completed: false,
      type: 'daily' as const
    },
    {
      id: 'd2',
      title: 'Study',
      content: 'Learn new skills',
      current: 2,
      target: 2,
      completed: true,
      type: 'daily' as const
    }
  ]);

  const deleteMission = (id: string) => {
    setWeeklyMissionList(prev => prev.filter(m => m.id !== id));
    setDailyMissionList(prev => prev.filter(m => m.id !== id));
  };

  return (
    <div className="w-full h-full bg-gradient-to-br from-amber-100 to-amber-200 flex items-center justify-center p-8">
      <div className="w-[900px] h-[600px] bg-amber-50 rounded-lg shadow-2xl flex flex-col overflow-hidden">
        {/* Custom Title Bar */}
        <div className="bg-amber-100 border-b border-amber-300 px-4 py-2 flex items-center justify-between">
          <h1 className="text-lg font-semibold text-gray-800">Daily Weekly Mission</h1>
          <div className="flex gap-1">
            <TitleBarButton
              icon={<History size={18} />}
              onClick={() => navigate('/history')}
            />
            <TitleBarButton
              icon={<Trash2 size={18} />}
              onClick={() => setDeleteMode(!deleteMode)}
              variant={deleteMode ? 'danger' : 'default'}
            />
            <TitleBarButton
              icon={<X size={18} />}
              onClick={() => {}}
            />
          </div>
        </div>

        {/* Delete Mode Message */}
        {deleteMode && (
          <div className="bg-red-50 border-b border-red-200 px-4 py-2 text-center text-sm text-red-700">
            Select a mission to delete
          </div>
        )}

        {/* Main Content */}
        <div className="flex-1 overflow-auto p-6">
          <MissionSection
            title="Weekly Missions"
            onAddClick={() => navigate('/add?type=weekly')}
          >
            {weeklyMissionList.map((mission) => (
              <MissionCard
                key={mission.id}
                {...mission}
                deleteMode={deleteMode}
                selected={selectedMission === mission.id}
                onSelect={() => setSelectedMission(mission.id)}
                onDelete={() => deleteMission(mission.id)}
              />
            ))}
          </MissionSection>

          <MissionSection
            title="Daily Missions"
            onAddClick={() => navigate('/add?type=daily')}
          >
            {dailyMissionList.map((mission) => (
              <MissionCard
                key={mission.id}
                {...mission}
                deleteMode={deleteMode}
                selected={selectedMission === mission.id}
                onSelect={() => setSelectedMission(mission.id)}
                onDelete={() => deleteMission(mission.id)}
              />
            ))}
          </MissionSection>
        </div>
      </div>
    </div>
  );
}
