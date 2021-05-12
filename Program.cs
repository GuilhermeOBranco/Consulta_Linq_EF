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
                // ContarFaixas(context, "Led", "");
                // FuncaoSum(context, "Led", "");


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

            System.Console.WriteLine("Usando Count com query syntax");

            var queryCount = (from f in context.Faixas
                              where f.Album.Artista.Nome.Contains(buscaArsitas)
                              select f).Count();
            System.Console.WriteLine(queryCount);

            #endregion


            #region MétodoPorFunção
            var quantidadeFuncao = context.Faixas.Where(x => x.Album.Artista.Nome.Contains("Led")).Count();
            System.Console.WriteLine(quantidadeFuncao);

            System.Console.WriteLine("SEM USO DO WHERE");

            var quantidadeFuncao2 = context.Faixas.Count(x => x.Album.Artista.Nome.Contains("Led"));
            System.Console.WriteLine(quantidadeFuncao2);
            #endregion

            //MÉTODO POR FUNÇÃO SE MOSTROU SER 52 MS MAIS RÁPIDO QUE O MÉTODO DE CONSULTA.
        }

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

        public static void FuncaoSum(AluraTunesContext context, string buscaArsitas, string buscaAlbum)
        {
            var consulta = from inf in context.ItemNotaFiscals
                           where inf.Faixa.Album.Artista.Nome.Contains(buscaArsitas)
                           select new { totalDoItem = inf.Quantidade * inf.PrecoUnitario };
            
            var totalArtista = consulta.Sum(x => x.totalDoItem);

            System.Console.WriteLine("Total do artista é: R$ {0}", totalArtista);
        }
    
        public static void FuncaoGroupBy(AluraTunesContext context, string buscaArsitas)
        {
            var query = from inf in context.ItemNotaFiscals
                        where inf.Faixa.Album.Artista.Nome.Contains(buscaArsitas)
                        group inf by inf.Faixa.Album into agrupado
                        let vendasPorAlbum = agrupado.Sum(a => a.Quantidade * a.PrecoUnitario)
                        orderby vendasPorAlbum
                            descending
                        select new 
                        { 
                         Titulo = agrupado.Key.Titulo,
                         TotalPorAlbum = vendasPorAlbum
                        };
            
            foreach (var item in query)
            {
                System.Console.WriteLine("{0} \t {1}" ,item.Titulo, item.TotalPorAlbum);
            }

            
        }
    }
}
