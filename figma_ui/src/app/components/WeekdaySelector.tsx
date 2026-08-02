interface WeekdaySelectorProps {
  selectedDays?: number[];
  onToggle?: (day: number) => void;
  currentDay?: number;
  scheduledDays?: number[];
  mode?: 'select' | 'display';
}

const WEEKDAYS = ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'];

export function WeekdaySelector({
  selectedDays = [],
  onToggle,
  currentDay,
  scheduledDays = [],
  mode = 'select'
}: WeekdaySelectorProps) {
  return (
    <div className="flex gap-2">
      {WEEKDAYS.map((day, index) => {
        const dayNumber = index + 1;
        const isSelected = selectedDays.includes(dayNumber);
        const isCurrent = currentDay === dayNumber;
        const isScheduled = scheduledDays.includes(dayNumber);

        if (mode === 'display') {
          return (
            <div
              key={day}
              className={`w-9 h-9 rounded-full flex items-center justify-center text-xs font-medium transition-colors ${
                isCurrent
                  ? 'bg-blue-500 text-white'
                  : isScheduled
                  ? 'bg-blue-100 text-blue-700'
                  : 'bg-amber-200 text-gray-600'
              }`}
            >
              {day.charAt(0)}
            </div>
          );
        }

        return (
          <button
            key={day}
            onClick={() => onToggle?.(dayNumber)}
            className={`w-9 h-9 rounded-full flex items-center justify-center text-xs font-medium transition-colors ${
              isSelected
                ? 'bg-blue-500 text-white'
                : 'bg-amber-200 text-gray-600 hover:bg-amber-300'
            }`}
          >
            {day.charAt(0)}
          </button>
        );
      })}
    </div>
  );
}
