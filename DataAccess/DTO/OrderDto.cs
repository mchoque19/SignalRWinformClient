namespace DAL.DTO
{
    public class OrderDto : ClaseBaseDto
    {
        
       public int Id { get; set; }
        public string OrderType { get; set; }
        public string? RefOrderId { get; set; }
        public List<GroupDto> GroupOrderList { get; set; }
    }


}
