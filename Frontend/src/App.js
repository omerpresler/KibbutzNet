import logo from './logo.svg';
import './App.css';
import LoginUser from './pages/LoginUser';
import Register from './pages/Regsiter';
import * as paths from './services/pathes';
import Member from './pages/Member';
import Store from './pages/Store';
import LoginStore from './pages/LoginStore';
import PurchseHistoryPage from './pages/PurchseHistoryPage';
import ChatManagerPage from './pages/ChatsManger'
import OrderManger from './pages/OrderManager';
import PageManager from './pages/PageManger';
import AdminPage from './pages/Admin'
import ReportPage from './pages/Reports';
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
          
          <Route index element={<Home />} />
          <Route path={paths.home_page} element={<Home />} />

          <Route path={paths.login_to_user} element={<LoginUser />} />
          <Route path={paths.login_to_register} element={<LoginStore nextPage={paths.register_page_path} />} />
          <Route path={paths.login_to_store} element={<LoginStore nextPage={paths.store_page_path} />} />

          <Route path={paths.report_page} element={<ReportPage />} />
          <Route path={paths.register_page_path} element={<Register />} />
          <Route path={paths.member_page_path} element={<Member />} />
          <Route path={paths.store_page_path} element={<Store  />} />
          <Route path={paths.purchse_history_page_path} element={<PurchseHistoryPage  />} />
          <Route path={paths.chat_manager_page_path} element={<ChatManagerPage  />} />
          <Route path={paths.order_manager_page_path} element={<OrderManger  />} />
          <Route path={paths.page_manager_page_path} element={<PageManager  />} />
          <Route path={paths.admin_page_path} element={<AdminPage />} />

          

        </Routes>
    </Router>
)
};


export default App;
