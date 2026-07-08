import { Check } from 'lucide-react';
import { ProgressIndicator } from './ProgressIndicator';
import { WeekdaySelector } from './WeekdaySelector';

interface MissionCardProps {
  title: string;
  content: string;
  current: number;
  target: number;
  completed: boolean;
  type?: 'daily' | 'weekly-count' | 'weekly-days';
  scheduledDays?: number[];
  currentDay?: number;
  deleteMode?: boolean;
  selected?: boolean;
  onSelect?: () => void;
  onDelete?: () => void;
}

export function MissionCard({
  title,
  content,
  current,
  target,
  completed,
  type = 'daily',
  scheduledDays,
  currentDay,
  deleteMode,
  selected,
  onSelect,
  onDelete,
}: MissionCardProps) {
  return (
    <div
      className={`bg-amber-50 rounded-lg p-4 border transition-all ${
        deleteMode
          ? selected
            ? 'border-red-500 border-2 bg-red-50'
            : 'border-red-200 hover:border-red-400 cursor-pointer'
          : 'border-amber-200'
      } ${completed && !deleteMode ? 'opacity-50' : ''}`}
      onClick={deleteMode ? onDelete : undefined}
    >
      <div className="flex items-start justify-between gap-3 mb-3">
        <div className="flex-1 min-w-0">
          <h3 className={`font-medium mb-1 truncate ${completed && !deleteMode ? 'text-gray-500' : 'text-gray-800'}`}>
            {title}
          </h3>
          <p className={`text-sm ${completed && !deleteMode ? 'text-gray-400' : 'text-gray-600'}`}>
            {content}
          </p>
        </div>

        {!deleteMode && (
          <div
            className={`w-6 h-6 rounded-full flex items-center justify-center flex-shrink-0 ${
              completed ? 'bg-green-500' : 'border-2 border-gray-300'
            }`}
          >
            {completed && <Check size={14} className="text-white" />}
          </div>
        )}
      </div>

      {type === 'weekly-days' && scheduledDays ? (
        <WeekdaySelector
          mode="display"
          currentDay={currentDay}
          scheduledDays={scheduledDays}
        />
      ) : (
        <ProgressIndicator current={current} target={target} type="count" />
      )}
    </div>
  );
}
