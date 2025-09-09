using System;
using System.Collections.Generic;
using Spotifei.Model;
using Spotifei.DAL;

namespace Spotifei.Controller
{
    public class MusicaController
    {
        private MusicaRepository musicaRepo = new MusicaRepository();

        public void CadastrarMusica(Musica musica)
        {
            musicaRepo.Adicionar(musica);
            Console.WriteLine(" Musica cadastrada com sucesso!");
        }

        public List<Musica> ListarMusicas()
        {
            return musicaRepo.Listar();
        }

        public void RemoverMusica(int id)
        {
            var musica = musicaRepo.BuscarPorId(id);
            if (musica != null)
            {
                musicaRepo.Remover(id);
                Console.WriteLine(" Musica removida!");
            }
            else
            {
                Console.WriteLine(" Musica não encontrada!");
            }
        }
    }
}
