class Message {
    constructor(sender, message, addon = null) {
      this.sender = sender;
      this.message = message;
      this.addon = addon;
    }
  
    toString() {
      return JSON.stringify(this);
    }
  }
  
  export default Message;