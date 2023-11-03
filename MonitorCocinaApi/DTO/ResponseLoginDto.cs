namespace MonitorCocinaApi.DTO
{
	public class ResponseLoginDto<T>
	{
		public string Date { get; set; } = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
		//public string Request { get; set; } = string.Empty;

		public int Result { get; set; }
		//public string Message { get; set; } = string.Empty;

		public T Content { get; set; } 

		public ErrorDto? Error { get; set; }
	}
}