import { useState } from 'react';
import { X } from 'lucide-react';
import { useNavigate } from 'react-router';
import { TitleBarButton } from '../components/TitleBarButton';
import { WeekdaySelector } from '../components/WeekdaySelector';

export function AddMissionWindow() {
  const navigate = useNavigate();
  const [missionType, setMissionType] = useState<'daily' | 'weekly'>('daily');
  const [selectedDays, setSelectedDays] = useState<number[]>([]);

  const handleToggleDay = (day: number) => {
    setSelectedDays(prev =>
      prev.includes(day)
        ? prev.filter(d => d !== day)
        : [...prev, day]
    );
  };

  return (
    <div className="w-full h-full bg-gradient-to-br from-amber-100 to-amber-200 flex items-center justify-center p-8">
      <div className="w-[600px] bg-amber-50 rounded-lg shadow-2xl flex flex-col overflow-hidden">
        {/* Custom Title Bar */}
        <div className="bg-amber-100 border-b border-amber-300 px-4 py-2 flex items-center justify-between">
          <h1 className="text-lg font-semibold text-gray-800">Add Mission</h1>
          <TitleBarButton
            icon={<X size={18} />}
            onClick={() => navigate('/')}
          />
        </div>

        {/* Form Content */}
        <div className="p-6 space-y-4">
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              Mission Title
            </label>
            <input
              type="text"
              placeholder="Enter mission title"
              className="w-full px-3 py-2 bg-white border border-amber-300 rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
            />
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              Mission Content
            </label>
            <textarea
              placeholder="Enter mission description"
              rows={3}
              className="w-full px-3 py-2 bg-white border border-amber-300 rounded focus:outline-none focus:ring-2 focus:ring-blue-500 resize-none"
            />
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              Mission Type
            </label>
            <div className="flex gap-3">
              <button
                onClick={() => setMissionType('daily')}
                className={`flex-1 py-2 px-4 rounded font-medium transition-colors ${
                  missionType === 'daily'
                    ? 'bg-blue-500 text-white'
                    : 'bg-amber-200 text-gray-700 hover:bg-amber-300'
                }`}
              >
                Daily
              </button>
              <button
                onClick={() => setMissionType('weekly')}
                className={`flex-1 py-2 px-4 rounded font-medium transition-colors ${
                  missionType === 'weekly'
                    ? 'bg-blue-500 text-white'
                    : 'bg-amber-200 text-gray-700 hover:bg-amber-300'
                }`}
              >
                Weekly
              </button>
            </div>
          </div>

          {missionType === 'weekly' && (
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-2">
                Select Days
              </label>
              <WeekdaySelector
                selectedDays={selectedDays}
                onToggle={handleToggleDay}
                mode="select"
              />
            </div>
          )}

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              Target Count
            </label>
            <input
              type="number"
              placeholder="e.g., 3"
              min="1"
              className="w-full px-3 py-2 bg-white border border-amber-300 rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
            />
          </div>

          <div className="grid grid-cols-2 gap-4">
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-2">
                Start Date
              </label>
              <input
                type="date"
                className="w-full px-3 py-2 bg-white border border-amber-300 rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
              />
            </div>
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-2">
                End Date
              </label>
              <input
                type="date"
                className="w-full px-3 py-2 bg-white border border-amber-300 rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
              />
            </div>
          </div>

          {/* Action Buttons */}
          <div className="flex gap-3 pt-4">
            <button
              onClick={() => navigate('/')}
              className="flex-1 py-2.5 px-4 bg-gray-300 text-gray-700 rounded font-medium hover:bg-gray-400 transition-colors"
            >
              Cancel
            </button>
            <button
              onClick={() => navigate('/')}
              className="flex-1 py-2.5 px-4 bg-blue-500 text-white rounded font-medium hover:bg-blue-600 transition-colors"
            >
              Save Mission
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}
