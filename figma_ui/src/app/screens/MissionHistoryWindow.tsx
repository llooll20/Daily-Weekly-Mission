import { useState } from 'react';
import { X, ChevronLeft, ChevronRight } from 'lucide-react';
import { useNavigate } from 'react-router';
import { TitleBarButton } from '../components/TitleBarButton';
import {
  startOfMonth, endOfMonth, startOfWeek, endOfWeek,
  eachDayOfInterval, format, isSameMonth, isSameDay, addMonths, subMonths, parseISO
} from 'date-fns';

interface HistoryItem {
  id: string;
  title: string;
  type: 'Daily' | 'Weekly';
  current: number;
  target: number;
  completionRate: number;
  completedDates: string[];
  period: string;
  status: 'not-started' | 'in-progress' | 'completed';
}

export function MissionHistoryWindow() {
  const navigate = useNavigate();
  const [selectedMission, setSelectedMission] = useState<string>('1');

  const missions: HistoryItem[] = [
    {
      id: '1',
      title: 'Exercise',
      type: 'Weekly',
      current: 3,
      target: 3,
      completionRate: 100,
      completedDates: ['2026-05-26', '2026-05-28', '2026-05-30'],
      period: '2026-05-25 ~ 2026-05-31',
      status: 'completed'
    },
    {
      id: '2',
      title: 'Read Books',
      type: 'Weekly',
      current: 4,
      target: 5,
      completionRate: 80,
      completedDates: ['2026-05-25', '2026-05-27', '2026-05-29', '2026-05-31'],
      period: '2026-05-25 ~ 2026-05-31',
      status: 'in-progress'
    },
    {
      id: '3',
      title: 'Morning Meditation',
      type: 'Daily',
      current: 0,
      target: 1,
      completionRate: 0,
      completedDates: [],
      period: '2026-06-01',
      status: 'not-started'
    }
  ];

  const selected = missions.find(m => m.id === selectedMission);

  const getInitialMonth = (mission: HistoryItem | undefined) => {
    if (!mission) return new Date();
    if (mission.completedDates.length > 0) return parseISO(mission.completedDates[0]);
    if (mission.period) return parseISO(mission.period.split(' ~ ')[0] ?? mission.period);
    return new Date();
  };

  const [calendarMonth, setCalendarMonth] = useState<Date>(() => getInitialMonth(selected));

  const handleMissionSelect = (id: string) => {
    setSelectedMission(id);
    const m = missions.find(mi => mi.id === id);
    setCalendarMonth(getInitialMonth(m));
  };

  const calendarDays = eachDayOfInterval({
    start: startOfWeek(startOfMonth(calendarMonth), { weekStartsOn: 0 }),
    end: endOfWeek(endOfMonth(calendarMonth), { weekStartsOn: 0 }),
  });

  const getStatusColor = (status: string) => {
    switch (status) {
      case 'completed':
        return 'text-green-600 bg-green-100';
      case 'in-progress':
        return 'text-blue-600 bg-blue-100';
      case 'not-started':
        return 'text-gray-600 bg-gray-100';
      default:
        return 'text-gray-600 bg-gray-100';
    }
  };

  const getStatusLabel = (status: string) => {
    switch (status) {
      case 'completed':
        return 'Completed';
      case 'in-progress':
        return 'In Progress';
      case 'not-started':
        return 'Not Started';
      default:
        return 'Unknown';
    }
  };

  return (
    <div className="w-full h-full bg-gradient-to-br from-amber-100 to-amber-200 flex items-center justify-center p-8">
      <div className="w-[900px] h-[600px] bg-amber-50 rounded-lg shadow-2xl flex flex-col overflow-hidden">
        {/* Custom Title Bar */}
        <div className="bg-amber-100 border-b border-amber-300 px-4 py-2 flex items-center justify-between">
          <h1 className="text-lg font-semibold text-gray-800">Mission History</h1>
          <TitleBarButton
            icon={<X size={18} />}
            onClick={() => navigate('/')}
          />
        </div>

        {/* Main Content */}
        <div className="flex-1 flex overflow-hidden">
          {/* Mission List */}
          <div className="w-1/3 border-r border-amber-300 overflow-auto">
            {missions.map((mission) => (
              <button
                key={mission.id}
                onClick={() => handleMissionSelect(mission.id)}
                className={`w-full p-4 text-left border-b border-amber-200 transition-colors ${
                  selectedMission === mission.id
                    ? 'bg-amber-100'
                    : 'hover:bg-amber-50'
                }`}
              >
                <div className="font-medium text-gray-800 mb-1">{mission.title}</div>
                <div className="text-sm text-gray-600">{mission.type}</div>
                <div className="text-xs text-gray-500 mt-1">{mission.period}</div>
              </button>
            ))}
          </div>

          {/* Mission Details */}
          {selected && (
            <div className="flex-1 p-6 overflow-auto">
              <div className="space-y-6">
                <div>
                  <h2 className="text-2xl font-semibold text-gray-800 mb-2">
                    {selected.title}
                  </h2>
                  <div className="flex items-center gap-3">
                    <span className="text-sm text-gray-600">{selected.type} Mission</span>
                    <span className={`text-xs px-2 py-1 rounded-full font-medium ${getStatusColor(selected.status)}`}>
                      {getStatusLabel(selected.status)}
                    </span>
                  </div>
                </div>

                <div className="bg-amber-100 rounded-lg p-4 space-y-3">
                  <div className="flex justify-between items-center">
                    <span className="text-sm text-gray-700">Progress</span>
                    <span className="text-lg font-semibold text-gray-800">
                      {selected.current} / {selected.target}
                    </span>
                  </div>
                  <div className="h-2 bg-amber-200 rounded-full overflow-hidden">
                    <div
                      className="h-full bg-blue-500 transition-all"
                      style={{ width: `${selected.completionRate}%` }}
                    />
                  </div>
                  <div className="text-center text-sm text-gray-600">
                    {selected.completionRate}% Complete
                  </div>
                </div>

                <div>
                  <h3 className="text-sm font-medium text-gray-700 mb-2">Period</h3>
                  <div className="bg-amber-50 border border-amber-200 rounded px-3 py-2 text-gray-700">
                    {selected.period}
                  </div>
                </div>

                <div>
                  <h3 className="text-sm font-medium text-gray-700 mb-3">
                    Completed Dates ({selected.completedDates.length})
                  </h3>
                  {/* Calendar */}
                  <div className="bg-amber-50 border border-amber-200 rounded-lg p-3 select-none">
                    {/* Month nav */}
                    <div className="flex items-center justify-between mb-3">
                      <button
                        onClick={() => setCalendarMonth(prev => subMonths(prev, 1))}
                        className="p-1 rounded hover:bg-amber-200 text-gray-600 transition-colors"
                      >
                        <ChevronLeft size={14} />
                      </button>
                      <span className="text-sm font-semibold text-gray-800">
                        {format(calendarMonth, 'MMMM yyyy')}
                      </span>
                      <button
                        onClick={() => setCalendarMonth(prev => addMonths(prev, 1))}
                        className="p-1 rounded hover:bg-amber-200 text-gray-600 transition-colors"
                      >
                        <ChevronRight size={14} />
                      </button>
                    </div>
                    {/* Day headers */}
                    <div className="grid grid-cols-7 mb-1">
                      {['S', 'M', 'T', 'W', 'T', 'F', 'S'].map((d, i) => (
                        <div key={i} className="text-center text-[10px] font-semibold text-gray-400 py-1">
                          {d}
                        </div>
                      ))}
                    </div>
                    {/* Days */}
                    <div className="grid grid-cols-7 gap-y-0.5">
                      {calendarDays.map((day, i) => {
                        const isCompleted = selected.completedDates.some(d => isSameDay(parseISO(d), day));
                        const inMonth = isSameMonth(day, calendarMonth);
                        return (
                          <div key={i} className="flex items-center justify-center">
                            <div
                              className={[
                                'w-7 h-7 flex items-center justify-center rounded-full text-xs font-medium transition-colors',
                                !inMonth ? 'text-gray-300' : '',
                                inMonth && isCompleted
                                  ? 'bg-green-500 text-white font-bold shadow-sm'
                                  : inMonth
                                  ? 'text-gray-700'
                                  : '',
                              ].join(' ')}
                            >
                              {format(day, 'd')}
                            </div>
                          </div>
                        );
                      })}
                    </div>
                    {selected.completedDates.length === 0 && (
                      <p className="text-center text-xs text-gray-400 mt-2 italic">No completed dates yet</p>
                    )}
                  </div>
                </div>
              </div>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
