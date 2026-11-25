using System;

namespace JogoAdivinhacao
{
    class Program
    {
        const string Arquivo = "arvore.txt";

        static void Main(string[] args)
        {
            Arvore arvore = new Arvore();
            arvore.Carregar(Arquivo);

            Console.WriteLine("=== Jogo da Adivinhacao (Arvore Binaria) ===");
            Console.WriteLine("Pense em um animal e responda com s ou n.");

            bool continuar = true;
            while (continuar)
            {
                Console.WriteLine();
                arvore.Jogar();
                arvore.Salvar(Arquivo);

                Console.Write("Ver a arvore agora? (s/n): ");
                string ver = Console.ReadLine();
                if (ver != null && ver.Trim().ToLower() == "s")
                {
                    Console.WriteLine();
                    arvore.Imprimir();
                }

                Console.Write("Jogar de novo? (s/n): ");
                string de_novo = Console.ReadLine();
                continuar = de_novo != null && de_novo.Trim().ToLower() == "s";
            }

            Console.WriteLine("Ate mais!");
        }
    }
}
