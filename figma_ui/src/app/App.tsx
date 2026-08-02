import { BrowserRouter, Routes, Route } from 'react-router';
import { MainWindow } from './screens/MainWindow';
import { AddMissionWindow } from './screens/AddMissionWindow';
import { MissionHistoryWindow } from './screens/MissionHistoryWindow';

export default function App() {
  return (
    <BrowserRouter>
      <div className="w-screen h-screen">
        <Routes>
          <Route path="/" element={<MainWindow />} />
          <Route path="/add" element={<AddMissionWindow />} />
          <Route path="/history" element={<MissionHistoryWindow />} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}