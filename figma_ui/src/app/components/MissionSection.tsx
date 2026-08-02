interface MissionSectionProps {
  title: string;
  children: React.ReactNode;
  onAddClick?: () => void;
}

export function MissionSection({ title, children, onAddClick }: MissionSectionProps) {
  return (
    <div className="mb-6">
      <div className="flex items-center justify-between mb-3 pb-2 border-b border-amber-300">
        <h2 className="text-lg font-semibold text-gray-800">
          {title}
        </h2>
        {onAddClick && (
          <button
            onClick={onAddClick}
            className="w-8 h-8 rounded-full bg-blue-500 text-white flex items-center justify-center hover:bg-blue-600 transition-colors"
          >
            +
          </button>
        )}
      </div>
      <div className="space-y-3">
        {children}
      </div>
    </div>
  );
}
