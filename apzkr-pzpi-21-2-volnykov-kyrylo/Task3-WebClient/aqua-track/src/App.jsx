import { BrowserRouter, Route, Routes, Navigate } from 'react-router-dom';
import Login from './components/user_components/Login/Login';
import SignUp from './components/user_components/SignUp/SignUp';
import './App.css'
import AdminAquarium from './components/aquarium_components/AdminAquarium/AdminAquarium';
import AdminPanel from './components/user_components/AdminPanel/AdminPanel';
import AquariumUpdateForm from './components/aquarium_components/AquariumUpdateForm/AquariumUpdateForm';
import AquariumAddForm from './components/aquarium_components/AquariumAddForm/AquariumAddForm';
import UserUpdateForm from './components/user_components/UserUpdateForm/UserUpdateForm';
import AquariumView from './components/aquarium_components/AquariumView/AquariumView';
import UserAquarium from './components/aquarium_components/UserAquarium/UserAquarium';
import Inhabitant from './components/inhabitant_components/Inhabitant/Inhabitant';
import InhabitantUpdateForm from './components/inhabitant_components/InhabitantUpdateForm/InhabitantUpdateForm';
import InhabitantAddForm from './components/inhabitant_components/InhabitantAddForm/InhabitantAddForm';
import InhabitantView from './components/inhabitant_components/InhabitantView/InhabitantView';
import ResearchReport from './components/research_report_components/ResearchReport/ResearchReport';
import ResearchReportAddForm from './components/research_report_components/ResearchReportAddForm/ResearchReportAddForm';
import AnalysisReport from './components/analysis_report_components/AnalysisReport/AnalysisReport';
import AnalysisReportAddForm from './components/analysis_report_components/AnalysisReportAddForm/AnalysisReportAddForm';
import AnalysisReportView from './components/analysis_report_components/AnalysisReportView/AnalysisReportView';

export function App() {
  return (
    <BrowserRouter>
      <Routes>
      <Route path="/" element={<Navigate to="/user/login" replace />} />
        <Route path="/user/">
          <Route path="sign-up" element={<SignUp />} />
          <Route path="login" element={<Login />} />

          <Route path="aquarium" element={<UserAquarium />} />

          <Route path="inhabitant/:aquariumId" element={<Inhabitant />} />
          <Route path="inhabitant/:aquariumId/update/:inhabitantId" element={<InhabitantUpdateForm />} />
          <Route path="inhabitant/add/:aquariumId" element={<InhabitantAddForm />} />
          <Route path="inhabitant/view/:inhabitantId" element={<InhabitantView />} />

          <Route path="research-report" element={<ResearchReport />} />
          <Route path="research-report/add" element={<ResearchReportAddForm />} />

          <Route path="analysis-report" element={<AnalysisReport />} />
          <Route path="analysis-report/add" element={<AnalysisReportAddForm />} />
          <Route path="analysis-report/view/:analysisReportId" element={<AnalysisReportView />} />

        </Route>

        <Route path="/admin/">
          <Route path="aquarium/:userId" element={<AdminAquarium />} />
          <Route path="aquarium/:userId/update/:aquariumId" element={<AquariumUpdateForm />} />
          <Route path="aquarium/view/:aquariumId" element={<AquariumView />} />
          <Route path="aquarium/add/:userId" element={<AquariumAddForm />} />

          <Route path="update/:userId" element={<UserUpdateForm />} />
          <Route path="panel" element={<AdminPanel />} />
        </Route>
        <Route path="/general/">
          <Route path="aquarium/view/:aquariumId" element={<AquariumView />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;