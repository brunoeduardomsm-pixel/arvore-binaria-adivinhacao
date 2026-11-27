using System;
using System.Collections.Generic;
using System.IO;

namespace JogoAdivinhacao
{
    public class Arvore
    {
        public No Raiz;

        public Arvore()
        {
            // Arvore inicial: uma pergunta e dois palpites
            Raiz = new No("O animal vive na agua?");
            Raiz.Direita = new No("peixe");
            Raiz.Esquerda = new No("cachorro");
        }

        // Joga uma rodada: desce pela arvore perguntando ate chegar numa folha
        public void Jogar()
        {
            No atual = Raiz;
            No pai = null;
            bool veioPelaDireita = false;

            while (!atual.EhFolha())
            {
                bool sim = Perguntar(atual.Texto);
                pai = atual;
                if (sim)
                {
                    atual = atual.Direita;
                    veioPelaDireita = true;
                }
                else
                {
                    atual = atual.Esquerda;
                    veioPelaDireita = false;
                }
            }

            // Chegou numa folha: faz o palpite
            if (Perguntar("Eu acho que e: " + atual.Texto + ". Acertei?"))
            {
                Console.WriteLine("Acertei de novo!");
                return;
            }

            // Errou: aprende com o jogador
            Aprender(atual, pai, veioPelaDireita);
        }

        // Troca a folha errada por uma pergunta nova, com duas folhas
        private void Aprender(No folhaErrada, No pai, bool veioPelaDireita)
        {
            string certo = LerTexto("Desisto! Em que animal voce pensou? ");
            string pergunta = LerTexto("Digite uma pergunta que diferencie " + certo + " de " + folhaErrada.Texto + ": ");

            bool simParaNovo = Perguntar("Para " + certo + ", a resposta dessa pergunta e sim?");

            No novaPergunta = new No(pergunta);
            No folhaNova = new No(certo);

            if (simParaNovo)
            {
                novaPergunta.Direita = folhaNova;
                novaPergunta.Esquerda = folhaErrada;
            }
            else
            {
                novaPergunta.Esquerda = folhaNova;
                novaPergunta.Direita = folhaErrada;
            }

            // Liga a pergunta nova no lugar da folha antiga
            if (pai == null)
                Raiz = novaPergunta;
            else if (veioPelaDireita)
                pai.Direita = novaPergunta;
            else
                pai.Esquerda = novaPergunta;

            Console.WriteLine("Aprendi! Obrigado.");
        }

        // Le um texto do teclado e nao aceita resposta vazia
        private string LerTexto(string mensagem)
        {
            while (true)
            {
                Console.Write(mensagem);
                string t = Console.ReadLine();
                if (t == null) return "";
                t = t.Trim();
                if (t.Length > 0) return t;
                Console.WriteLine("Nao pode ficar vazio. Tente de novo.");
            }
        }

        private bool Perguntar(string texto)
        {
            while (true)
            {
                Console.Write(texto + " (s/n): ");
                string r = Console.ReadLine();
                if (r == null) return false;
                r = r.Trim().ToLower();
                if (r == "s" || r == "sim") return true;
                if (r == "n" || r == "nao") return false;
                Console.WriteLine("Responda com s ou n.");
            }
        }

        // Mostra a arvore deitada (direita = sim em cima, esquerda = nao embaixo)
        public void Imprimir()
        {
            ImprimirNo(Raiz, 0);
        }

        private void ImprimirNo(No no, int nivel)
        {
            if (no == null) return;
            ImprimirNo(no.Direita, nivel + 1);
            Console.WriteLine(new string(' ', nivel * 6) + (no.EhFolha() ? "(" + no.Texto + ")" : no.Texto));
            ImprimirNo(no.Esquerda, nivel + 1);
        }

        // ---------- Percursos (visitam todos os nos) ----------

        // Pre-ordem: raiz, esquerda, direita
        public void PreOrdem(No no, List<string> saida)
        {
            if (no == null) return;
            saida.Add(no.Texto);
            PreOrdem(no.Esquerda, saida);
            PreOrdem(no.Direita, saida);
        }

        // Em-ordem: esquerda, raiz, direita
        public void EmOrdem(No no, List<string> saida)
        {
            if (no == null) return;
            EmOrdem(no.Esquerda, saida);
            saida.Add(no.Texto);
            EmOrdem(no.Direita, saida);
        }

        // Pos-ordem: esquerda, direita, raiz
        public void PosOrdem(No no, List<string> saida)
        {
            if (no == null) return;
            PosOrdem(no.Esquerda, saida);
            PosOrdem(no.Direita, saida);
            saida.Add(no.Texto);
        }

        public void MostrarPercursos()
        {
            List<string> pre = new List<string>();
            List<string> em = new List<string>();
            List<string> pos = new List<string>();
            PreOrdem(Raiz, pre);
            EmOrdem(Raiz, em);
            PosOrdem(Raiz, pos);
            Console.WriteLine("Pre-ordem : " + string.Join(" | ", pre));
            Console.WriteLine("Em-ordem  : " + string.Join(" | ", em));
            Console.WriteLine("Pos-ordem : " + string.Join(" | ", pos));
        }

        // ---------- Estatisticas ----------

        // Altura = quantidade de niveis (uma arvore so com a raiz tem altura 1)
        public int Altura(No no)
        {
            if (no == null) return 0;
            int esq = Altura(no.Esquerda);
            int dir = Altura(no.Direita);
            return 1 + (esq > dir ? esq : dir);
        }

        public int ContarNos(No no)
        {
            if (no == null) return 0;
            return 1 + ContarNos(no.Esquerda) + ContarNos(no.Direita);
        }

        public int ContarFolhas(No no)
        {
            if (no == null) return 0;
            if (no.EhFolha()) return 1;
            return ContarFolhas(no.Esquerda) + ContarFolhas(no.Direita);
        }

        public void MostrarEstatisticas()
        {
            int altura = Altura(Raiz);
            int folhas = ContarFolhas(Raiz);
            int nos = ContarNos(Raiz);
            Console.WriteLine("Total de nos       : " + nos);
            Console.WriteLine("Perguntas (internos): " + (nos - folhas));
            Console.WriteLine("Palpites (folhas)  : " + folhas);
            Console.WriteLine("Altura da arvore   : " + altura);
            Console.WriteLine("Maximo de perguntas numa partida: " + (altura - 1));
        }

        // Salva a arvore no arquivo (percurso pre-ordem). "#" marca um filho vazio.
        public void Salvar(string caminho)
        {
            using (StreamWriter w = new StreamWriter(caminho))
            {
                SalvarNo(Raiz, w);
            }
        }

        private void SalvarNo(No no, StreamWriter w)
        {
            if (no == null)
            {
                w.WriteLine("#");
                return;
            }
            w.WriteLine(no.Texto);
            SalvarNo(no.Esquerda, w);
            SalvarNo(no.Direita, w);
        }

        // Carrega a arvore do arquivo, se ele existir
        public void Carregar(string caminho)
        {
            if (!File.Exists(caminho)) return;
            string[] linhas = File.ReadAllLines(caminho);
            if (linhas.Length == 0) return;
            int pos = 0;
            Raiz = CarregarNo(linhas, ref pos);
        }

        private No CarregarNo(string[] linhas, ref int pos)
        {
            if (pos >= linhas.Length) return null;
            string linha = linhas[pos++];
            if (linha == "#") return null;
            No no = new No(linha);
            no.Esquerda = CarregarNo(linhas, ref pos);
            no.Direita = CarregarNo(linhas, ref pos);
            return no;
        }
    }
}
