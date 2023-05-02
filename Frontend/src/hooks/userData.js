let accountNumberberber = null;

export default function GetLoginService() {
    const [user, setUser] = useState(accountNumberberber);
    
      function logout() {
        localStorage.clear();
        accountNumberberber = null;
        setUser(accountNumberberber);
      }
    
      function isAuthenticated() {
        return !!user;
      }
    
      return { user, logout, isAuthenticated };
    }

  



