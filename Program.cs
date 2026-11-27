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

            bool continuar = true;
            while (continuar)
            {
                Console.WriteLine();
                Console.WriteLine("1 - Jogar");
                Console.WriteLine("2 - Ver a arvore");
                Console.WriteLine("3 - Percursos (pre, em e pos-ordem)");
                Console.WriteLine("4 - Estatisticas (altura, nos, folhas)");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha: ");
                string op = Console.ReadLine();
                if (op == null) break;
                Console.WriteLine();

                switch (op.Trim())
                {
                    case "1":
                        Console.WriteLine("Pense em um animal e responda com s ou n.");
                        arvore.Jogar();
                        arvore.Salvar(Arquivo);
                        break;
                    case "2":
                        arvore.Imprimir();
                        break;
                    case "3":
                        arvore.MostrarPercursos();
                        break;
                    case "4":
                        arvore.MostrarEstatisticas();
                        break;
                    case "0":
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("Opcao invalida.");
                        break;
                }
            }

            Console.WriteLine("Ate mais!");
        }
    }
}
