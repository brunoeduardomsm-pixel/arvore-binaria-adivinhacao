# Jogo da Adivinhação com Árvore Binária

Trabalho de Estrutura de Dados (3º período, Sistemas de Informação - UniSapiens Porto Velho).

O computador tenta adivinhar o animal em que você pensou, fazendo perguntas de **sim** ou **não**.
Se ele errar, ele pergunta qual era o animal e aprende uma pergunta nova para a próxima vez.

## Como a árvore funciona
- **Nós internos:** guardam perguntas
- **Nós folhas:** guardam palpites (animais)
- **Aresta da esquerda:** resposta "não"
- **Aresta da direita:** resposta "sim"

## Arquivos
| Arquivo | O que tem |
|---|---|
| `No.cs` | classe do nó (texto, filho esquerdo, filho direito) |
| `Arvore.cs` | a árvore: jogar, aprender, imprimir, salvar e carregar |
| `Program.cs` | menu do jogo (console) |
| `arvore.txt` | criado sozinho quando o jogo roda, guarda o que o jogo aprendeu |

## Como rodar
Precisa do .NET 10. Na pasta do projeto:

```
dotnet run
```

## Equipe
- Bruno Eduardo Messias da Silva Morais
- Lucas Teixeira
- Wellington Braga
- Gabriel Ferreira
