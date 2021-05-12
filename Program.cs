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

                //GetFaixas(context, "Led", "");
                ContarFaixas(context, "Led", "");


            }

        }

        public static void GetFaixas(AluraTunesContext context, string buscaArsitas, string buscaAlbum)
        {
            var queryTeste = from f in context.Faixas
                             where f.Album.Artista.Nome.Contains(buscaArsitas)
                             select new { Album = f.Album.Titulo, faixaNome = f.Nome };

            //ADICIONA O PARAMETRO DE NOME DE ALBUM NA CONSULTA CASO A VARIAVEL NÃO VENHA VAZIA
            if (!string.IsNullOrEmpty(buscaAlbum))
            {
                queryTeste = queryTeste.Where(x => x.Album.Contains(buscaAlbum));
            }

            //ThenBy REALIZA UMA ORDENAÇÃO SECUNDÁRIA, ELE SÓ PODE SER USADO APÓS UMA CLAUSUALA DE ORDERBY
            queryTeste = queryTeste.OrderBy(x => x.Album).ThenBy(x => x.faixaNome);

            foreach (var faixa in queryTeste)
            {
                Console.WriteLine("{0}\t{1}", faixa.Album.PadRight(20), faixa.faixaNome);
            }
        }

        public static void ContarFaixas(AluraTunesContext context, string buscaArsitas, string buscaAlbum)
        {

            #region MétodoPorConsulta
            var query = from f in context.Faixas
                        where f.Album.Artista.Nome.Contains(buscaArsitas)
                        select f;

            var quantidade = query.Count();
            System.Console.WriteLine(quantidade);
            #endregion


            #region MétodoPorFunção
                var quantidadeFuncao = context.Faixas.Where(x => x.Album.Artista.Nome.Contains("Led")).Count();
                System.Console.WriteLine(quantidadeFuncao);
            #endregion

        //MÉTODO POR FUNÇÃO SE MOSTROU SER 52 MS MAIS RÁPIDO QUE O MÉTODO DE CONSULTA.
       }

    //EXEMPLOS REALIZADOS NO CURSO
        public static void Exemplos(AluraTunesContext context, string buscaArsitas, string buscaAlbum)
        {
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
}
