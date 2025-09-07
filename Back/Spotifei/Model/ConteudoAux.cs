using SpotifeiProjeto;

namespace Spotifei.Model;
public class ConteudoAux: Conteudo
{
    public ConteudoAux(int id, string titulo, string categoria, string classificacao, 
                TimeSpan duracao, int? artistaId, string tipo) 
                : base(id, titulo, categoria, classificacao, duracao, artistaId, tipo)
        {
               
        }
}