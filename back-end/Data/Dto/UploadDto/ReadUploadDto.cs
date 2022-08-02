namespace back_end.Data.Dto.UploadDto
{
    public class ReadUploadDto
    {
        public int Id { get; set; }
        public int IdDocumento { get; set; }
        public Byte[] Arquivo { get; set; }
    }
}
