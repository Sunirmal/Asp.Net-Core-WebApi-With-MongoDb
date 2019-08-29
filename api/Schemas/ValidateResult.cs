namespace api.Schemas
{
    public class ValidateResult
    {
        public ValidateResult(bool error, string message)
        {
            this.Error = error;
            this.Message = message;
        }
        public bool Error { get; set; }
        public string Message { get; set; }
    }
}