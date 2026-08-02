interface ProgressIndicatorProps {
  current: number;
  target: number;
  type?: 'count' | 'weekday';
}

export function ProgressIndicator({ current, target, type = 'count' }: ProgressIndicatorProps) {
  const progress = Math.min((current / target) * 100, 100);

  if (type === 'count') {
    return (
      <div className="flex items-center gap-2">
        <div className="flex-1 h-1.5 bg-amber-200 rounded-full overflow-hidden">
          <div
            className="h-full bg-blue-500 transition-all duration-300"
            style={{ width: `${progress}%` }}
          />
        </div>
        <span className="text-sm text-gray-600 min-w-[3rem] text-right">
          {current}/{target}
        </span>
      </div>
    );
  }

  return null;
}
