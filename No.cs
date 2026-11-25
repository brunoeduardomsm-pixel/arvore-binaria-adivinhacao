using System;

namespace JogoAdivinhacao
{
    // Cada No guarda um texto: pode ser uma pergunta (no interno) ou um palpite (no folha).
    public class No
    {
        public string Texto;
        public No Esquerda; // caminho do "Nao"
        public No Direita;  // caminho do "Sim"

        public No(string texto)
        {
            Texto = texto;
            Esquerda = null;
            Direita = null;
        }

        // No folha = nao tem filhos = guarda um palpite
        public bool EhFolha()
        {
            return Esquerda == null && Direita == null;
        }
    }
}
