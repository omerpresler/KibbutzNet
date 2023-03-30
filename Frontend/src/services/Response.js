export class Response {
    constructor(response) {
        this.value = response.value;
        this.wasExecption = response.wasExecption;
    }
    static create(value, wasExecption) {

        const res= new Response({
            value: value,
            wasExecption: wasExecption,
        })
        return res

    }
}

