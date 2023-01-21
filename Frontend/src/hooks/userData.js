let accountNumberberber = null;

export default function GetLoginService() {
    const [user, setUser] = useState(accountNumberberber);
  
    
    function checkLoginDataInBackend(email, accountNumberber) {
        const ServerAns=sendLoginRequest(email,accountNumberber);
        console.log(ServerAns);
        if(ServerAns===true){
        accountNumberberber = { accountNumberber };
        setUser(accountNumberberber);
        }
      }
    
      function logout() {
        localStorage.clear();
        accountNumberberber = null;
        setUser(accountNumberberber);
      }
    
      function isAuthenticated() {
        return !!user;
      }
    
      return { user, checkLoginDataInBackend, logout, isAuthenticated };
    }
    function logout() {
      localStorage.clear();
      accountNumberberber = null;
      setUser(accountNumberberber);
    }
  
    function isAuthenticated() {
      return !!user;
    }
  
    return { user, checkLoginDataInBackend, logout, isAuthenticated };
  
  function sendLoginRequest(email,accountNumberber){
    console.log(accountNumberber);
    axios.post(path, {
            email: JSON.stringify(email),accountNumberber:JSON.stringify(accountNumberber)
        })
    .then(function (response) {
        console.log(response)
        if (response.data===true){
            console.log("yes")
            localStorage.clear();
            localStorage.setItem('email',email);
            localStorage.setItem('accountNumberber',accountNumberber)
            return 1;
        }
        return false;
    })
    .catch(function (error) {
        console.log(error);
        return false;
    });


}