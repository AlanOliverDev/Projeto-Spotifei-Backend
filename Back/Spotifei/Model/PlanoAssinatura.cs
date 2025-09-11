
using System;

namespace Spotifei.Model
{
    public class PlanoAssinatura
    {
        public int Id { get; set; }
        public string TipoPlano { get; set; }
        public decimal PrecoMensal { get; set; }
        public int MaxPerfis { get; set; }
        public string QualidadeAudio { get; set; }
        public string QuantidadeAnuncios { get; set; }

        public PlanoAssinatura() { }

        public PlanoAssinatura(int id, string tipoPlano, decimal precoMensal, int maxPerfis,
            string qualidadeAudio, string quantidadeAnuncios)
        {
            Id = Id;
            TipoPlano = tipoPlano;
            PrecoMensal = precoMensal;
            MaxPerfis = maxPerfis;
            QualidadeAudio = qualidadeAudio;
            QuantidadeAnuncios = quantidadeAnuncios;
        }

        public decimal CalcularPrecoAnual()
        {
            return PrecoMensal * 12;
        }

        public void ObterBeneficios()
        {
            Console.WriteLine($"plano: {TipoPlano}");
            Console.WriteLine($"preço Mensal: R${PrecoMensal}");
            Console.WriteLine($"perfis Permitidos: {MaxPerfis}");
            Console.WriteLine($"qualidade do Áudio: {QualidadeAudio}");
            Console.WriteLine($"anúncios: {QuantidadeAnuncios}");
        }
    }
}
