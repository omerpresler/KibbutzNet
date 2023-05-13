export class Response {
    constructor(response) {
        this.value = response.value;
        this.exceptionHasOccured = response.exceptionHasOccured;
        this.errorMessage=response.errorMessage
    }
    static create(value, exceptionHasOccured,errorMessage) {
        const res= new Response({
            value: value,
            exceptionHasOccured: exceptionHasOccured,
            errorMessage:errorMessage
        })
        return res

    }
}

