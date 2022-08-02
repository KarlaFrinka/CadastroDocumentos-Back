namespace back_end.Models
{
    public class Upload
    {
        public int Id { get; set; } 
        public int IdDocumento { get; set; }    
        public string? Arquivo { get; set; }
    }
}
