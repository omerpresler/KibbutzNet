import logo from './logo.svg';
import './App.css';
import Login from './pages/Login';
import Register from './pages/Regsiter';
import * as paths from './services/pathes';

import {
  BrowserRouter as Router,
  Routes,
  Route,
  Navigate,
} from "react-router-dom"
function App() {
  return (
    <Router>
        <Routes>
          <Route index element={<Login />} />
          <Route path={paths.register_contorller_path} element={<Register />} />
        </Routes>
    </Router>
)
};


export default App;
