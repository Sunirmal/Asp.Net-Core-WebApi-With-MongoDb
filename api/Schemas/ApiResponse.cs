namespace api.Schemas
{
    public class ApiResponse<T>
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public ApiResponse(bool error, string message, T data)
        {
            this.Error = error;
            this.Message = message;
            this.Data = data;
        }
    }
}