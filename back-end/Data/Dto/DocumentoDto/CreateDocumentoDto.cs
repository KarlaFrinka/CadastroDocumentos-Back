﻿namespace QualyTeamTest.Data.Dto.DocumentoDto
{
    public class CreateDocumentoDto
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Titulo { get; set; }
        public int Processo { get; set; }
        public string Categoria { get; set; }
        public string Arquivo { get; set; }
    }
}
