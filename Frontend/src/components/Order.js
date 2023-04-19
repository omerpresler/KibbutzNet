class Order {
    constructor(orderId, memberName, memberId, active, cost, description) {
      this.orderID = orderId;
      this.date = new Date();
      this.status = "received";
      this.memberId = memberId;
      this.memberName = memberName;
      this.active = active;
      this.cost = cost;
      this.description = description;
    }
  
    toString() {
      return `${this.memberName}\t${this.memberId}\t${this.cost}\t${this.description}\t${this.status}`;
    }
  
    getID() {
      return this.orderID;
    }
  
    setStatus(status) {
      this.status = status;
    }
  }
  
  export default Order;
  