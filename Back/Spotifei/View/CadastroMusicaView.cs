class CadastroMusicaView
{
    public static void Exibir(Musica musica)
    {
        System.Console.WriteLine("\nDigite o Titulo da Música: ");
        musica.Nome = Console.ReadLine();
        System.Console.WriteLine("Digite a Categoria da Música: ");
        musica.Categoria = Console.ReadLine();
        System.Console.WriteLine("Digite a duracao da musica em minutos: ");
        double minutos = double.Parse(Console.ReadLine());
        musica.Duracao = TimeSpan.FromMinutes(minutos);
        new MusicaController().CadastrarMusica(musica);
    }
}