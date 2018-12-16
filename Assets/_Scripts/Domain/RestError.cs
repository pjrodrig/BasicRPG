public class RestError {

    public readonly int errorCode;
    public readonly string message;

    public RestError(int errorCode, string message) {
        this.errorCode = errorCode;
        this.message = message;
    }
}
