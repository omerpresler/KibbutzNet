import logo from './logo.svg';
import './App.css';
import Login from './pages/Login';
import Register from './pages/Regsiter';
import * as paths from './services/pathes';
import Member from './pages/Member';
import Store from './pages/Store';

import {
  BrowserRouter as Router,
  Routes,
  Route,
  Navigate,
} from "react-router-dom"
import Home from './pages/Home';
function App() {
  return (
    <Router>
        <Routes>
          <Route index element={<Login />} />
          <Route path={paths.register_contorller_path} element={<Register />} />
          <Route path={paths.home_controller_path} element={<Home />} />
          <Route path={paths.member_controller_path} element={<Member />} />
          <Route path={paths.store_controller_path} element={<Store />} />
        </Routes>
    </Router>
)
};


export default App;
