//main pathes
export const  back_path="https://localhost:7058"
export const  front_path="/"
export const  home_page="/Home"


//login function pathes
export const login_to_user="/loginToUser"
export const login_to_register="/loginToRegstier"
export const login_to_store="/loginToStore"


//pages pathes
export const register_page_path="/Register"
export const member_page_path="/Member"
export const store_page_path="/Store"
export const purchse_history_page_path="/purchseHistory"
export const chat_manager_page_path="/chatManager"
export const order_manager_page_path="/orderManager"
export const page_manager_page_path="/pageManager"
export const admin_page_path="/admin"
export const report_page="/reports"




//controller pathes
export const login_controller_path="/login"
export const home_controller_path="/Home"
export const store_controller_path="/Store"
export const user_controller_path="/User"
export const register_controller_path="/Store"
export const admin_controller_path="/Admin"


//store function pathes
export const get_purchase_path="/getPurchaseHistory"

//chat function pathes
export const startChatUserPath = back_path+user_controller_path+"/openChatUser"
export const startChatStorePath = back_path+store_controller_path+"/openChatStore"

export const sendMessageUserPath = back_path+user_controller_path+"/sendMassageInChat"
export const sendMessageStorePath = back_path+store_controller_path+"/sendMassageInChat"
export const getAllUserChats = back_path+user_controller_path+"/getAllChats"
export const getAllStoreChats = back_path+store_controller_path+"/getAllChats"

// export const endChatUserPath = back_path+user_controller_path+"/openChatUser"
// export const endChatStorePath = back_path+store_controller_path+"/openChatUser"




//regsiter function path
export const addPurchasePath= back_path+register_controller_path+"/addPurchase"
export const seePurchaseHistoryStorePath =back_path+register_controller_path+"/SeePurchaseHistoryStore"
export const seePurchaseHistoryUserAndStorePath=back_path+register_controller_path+"/SeePurchaseHistoryUserAndStore"
export const seePurchaseHistoryUserPath=back_path+user_controller_path+"/SeePurchaseHistoryUser"


//pageManagerPathes
export const get_all_stores="check"

//order function pathes
export const addOrderPath=back_path+store_controller_path+"/addOrder"
export const getAllOrderStore=back_path+store_controller_path+"/seeOrderHistoryStore"
export const getAllOrderStoreUser=back_path+store_controller_path+"/seeOrderHistoryUserAndStore"
export const getAllOrderUser=back_path+user_controller_path+"/seeOrderHistoryUser"
export const changeOrderStatus=back_path+store_controller_path+"/changeOrdersStatus"
export const closeOrder=back_path+store_controller_path+"/closeOrder"
export const reOpenOrder=back_path+store_controller_path+"/reOpenOrder"



//admin function pathes

export const addUser=back_path+admin_controller_path+"/createNewMember"
export const addStore=back_path+admin_controller_path+"/createNewStore"
export const connectStoreUser=back_path+admin_controller_path+"/assignEmployeeToStore"


//page function pathes

export const getStore = back_path + store_controller_path + "/getSpecficStore";


//member function pathes
export const getAllStores = back_path + user_controller_path + "/getAllStores";

//report function pathes

export const sendReportByEmailUrl = back_path+store_controller_path+'/sendReportByEmail';
export const saveExcelReportUrl = back_path+store_controller_path+'/saveExcelReport';
export const saveSmSReportUrl = back_path+store_controller_path+'/sendReportBySMS';

