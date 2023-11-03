namespace MonitorCocinaApi.DTO
{
    public class ResponseDto
    {
        public int Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public ResponseDto()
        {
            Success = 0;
        }
    }
}
