namespace TodoApi.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }       // Indicates if the request was successful
        public string Message { get; set; }     // Optional message
        public T Data { get; set; }             // The actual data
    }
}
