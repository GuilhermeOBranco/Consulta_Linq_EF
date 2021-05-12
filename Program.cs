using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace cursoLinqAlura.Entity
{
    class Program
    {
        static void Main(string[] args)
        {

            
            using (var context = new AluraTunesContext())
            {

                GetFaixas(context, "Led","");


                var query = from g in context.Generos
                            select g;

                foreach (var generos in query)
                {
                    // System.Console.WriteLine("{0}\t{1}", generos.Nome, generos.GeneroId);
                }

                System.Console.WriteLine();

                var queryJoin = from g in context.Generos
                                join f in context.Faixas
                                    on g.GeneroId equals f.GeneroId
                                select new
                                {
                                    f,
                                    g
                                };

                //MÉTODO PARA RETORNAR OS TOP 10
                queryJoin = queryJoin.Take(10);

                foreach (var disco in queryJoin)
                {
                    // System.Console.WriteLine("{0}\t{1}\t{2}", disco.f.Nome, disco.g.Nome, disco.f.PrecoUnitario);
                }

                //CONSULTA COM NOME DE BANDAS

                System.Console.WriteLine("DIGITE UM TEXTO PARA REALIZAR A BUSCA");
                var textoBusca = Console.ReadLine();

                var queryBandas = from f in context.Artista
                                  join alb in context.Albums
                                    on f.ArtistaId equals alb.ArtistaId
                                  where f.Nome.Contains(textoBusca)
                                  select new
                                  {
                                      NomeArtista = f.Nome,
                                      NomeAlbum = alb.Titulo
                                  };
                foreach (var artista in queryBandas)
                {
                    // System.Console.WriteLine("{0}\t{1}", artista.NomeArtista, artista.NomeAlbum);
                }

                System.Console.WriteLine();
                var query2 = from alb in context.Albums
                             where alb.Artista.Nome.Contains(textoBusca)
                             select new { NomeArtista = alb.Artista.Nome, NomeAlbum = alb.Titulo };

                foreach (var album in query2)
                {
                    // System.Console.WriteLine("{0}\t{1}",album.NomeAlbum,album.NomeArtista);
                }

            }

        }

        public static void GetFaixas(AluraTunesContext context, string buscaArsitas, string buscaAlbum)
        {
            var queryFaixas = from f in context.Faixas
                              where f.Album.Artista.Nome.Contains(buscaArsitas)
                              select f;
            
            //ADICIONA O PARAMETRO DE NOME DE ALBUM NA CONSULTA CASO A VARIAVEL NÃO VENHA VAZIA
            if(!string.IsNullOrEmpty(buscaAlbum))
            {
                queryFaixas = queryFaixas.Where(x => x.Album.Titulo.Contains(buscaAlbum));
            }

            foreach (var faixa in queryFaixas)
            {
                System.Console.WriteLine("{0}\t{1}", faixa.Genero.Nome.PadRight(10), faixa.Nome);
            }
        }

    }
}
