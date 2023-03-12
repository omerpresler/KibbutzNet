let storeID = null;

export default function GetStoreLoginService() {
    const [store, setStore] = useState(storeID);


    function checkStoreLoginDataInBackend(store, password) {
        const ServerAns=sendLoginStoreRequest(store,password);
        console.log(ServerAns);
        if(ServerAns===true){
            storeID = { store };
            setStore(storeID);
        }
    }

    function logout() {
        localStorage.clear();
        storeID = null;
        setStore(storeID);
    }

    function isAuthenticated() {
        return !!store;
    }

    return { store, checkStoreLoginDataInBackend, logout, isAuthenticated };
}
function logout() {
    localStorage.clear();
    storeID = null;
    setStore(storeID);
}

function isAuthenticated() {
    return !!store;
}

return { store, checkStoreLoginDataInBackend, logout, isAuthenticated };

function sendStoreLoginRequest(store,password){
    console.log(store);
    axios.post(path, {
        email: JSON.stringify(store),password:JSON.stringify(password)
    })
        .then(function (response) {
            console.log(response)
            if (response.data===true){
                console.log("yes")
                localStorage.clear();
                localStorage.setItem('store',store);
                localStorage.setItem('password',password)
                return 1;
            }
            return false;
        })
        .catch(function (error) {
            console.log(error);
            return false;
        });


}