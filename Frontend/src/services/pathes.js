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

//controller pathes
export const login_controller_path="/login"
export const home_controller_path="/Home"
export const store_controller_path="/Store"
export const register_controller_path="/Register"


//store function pathes
export const get_purchase_path="/getPurchaseHistory"

//chat function pathes
export const startChatPath = '/path/to/start/chat/endpoint';
export const endChatPath = '/path/to/end/chat/endpoint';
export const sendMessagePath = '/path/to/send/message/endpoint';
export const getAllChatsPath = '/path/to/get/all/chats/endpoint';


//regsiter function path
export const addPurchasePath= back_path+register_controller_path+"/addPurchase"
export const seePurchaseHistoryStorePath =back_path+register_controller_path+"/SeePurchaseHistoryStore"
// export const seePurchaseHistoryAllStorePath=back_path+paths.register_controller_path+"/addPurchase"
export const seePurchaseHistoryUserAndStorePath=back_path+register_controller_path+"/SeePurchaseHistoryUserAndStore"
export const seePurchaseHistoryUserPath=back_path+register_controller_path+"/SeePurchaseHistoryUser"