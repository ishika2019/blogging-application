namespace myproject.Models.DTO
{
    public class loginResponseDto
    {
        public string email {  get; set; }
        public string token {  get; set; }
        public  List<string> roles { get; set; }
    }
}
