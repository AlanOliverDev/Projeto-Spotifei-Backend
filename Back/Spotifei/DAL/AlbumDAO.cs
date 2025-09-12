public class AlbumDAO : IDAO<Musica>
{
    private List<Album> albuns = new List<Album>();
    public class AlbumDAO : IDAO<Album>
    {
        private List<Album> albuns = new List<Album>();

        public void Adicionar(Album album)
        {
            albuns.Add(album);
        }

        public Album BuscarPorId(int id)
        {
            foreach (var album in albuns)
            {
                if (album.Id == id)
                {
                    return album;
                }
            }

            Console.WriteLine("Álbum não encontrado.");
            return null;
        }

        public bool Excluir(int id)
        {
            var albumExcluir = BuscarPorId(id);
            if (albumExcluir != null)
            {
                albuns.Remove(albumExcluir);
                Console.WriteLine("Álbum excluído com sucesso!");
                return true;
            }
            else
            {
                Console.WriteLine("Álbum não encontrado.");
                return false;
            }
        }

        public void Atualizar(int id, Album novoAlbum)
        {
            var albumExistente = BuscarPorId(id);
            if (albumExistente != null)
            {
                albuns.Remove(albumExistente);
                albuns.Add(novoAlbum);
                Console.WriteLine("Álbum atualizado com sucesso!");
            }
            else
            {
                Console.WriteLine("Não foi possível atualizar o álbum.");
            }
        }

        public List<Album> Listar()
        {
            return albuns;
        }
    }
}