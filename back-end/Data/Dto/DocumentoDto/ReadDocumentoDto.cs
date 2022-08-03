using QualyTeamTest.Models;

namespace QualyTeamTest.Data.Dto.DocumentoDto
{
    public class ReadDocumentoDto
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Titulo { get; set; }
        public Processo Processo { get; set; }
        public string Categoria { get; set; }
     
    }
}
