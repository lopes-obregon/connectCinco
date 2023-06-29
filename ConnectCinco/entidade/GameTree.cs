using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConnectCinco.entidade
{
    internal class GameTree
    {
    
        private const int LIMITE_ALTURA = 2;
        private Nó head;
        public GameTree(Tabuleiro tabuleiro)
        {
            int altura_do_nó = 0;
            Nó raíz = new Nó(altura_do_nó);
            raíz.initTabuleiro((char[,]) tabuleiro.campo.Clone()); 
            head = raíz;
            GerarArvore(raíz);
           
            
        }


        public int MinimaxAlfaBeta(Nó nó, int profundidade, int alfa, int beta, bool maximizando)
        {
            if (profundidade == 0 || nó.Filhos.Count == 0)
            {
                // Nó folha, retorna a heurística do nó
                return nó.CalcularHeuristica();
            }

            if (maximizando)
            {
                int melhorValor = int.MinValue;
                foreach (Nó filho in nó.Filhos)
                {
                    int valor = MinimaxAlfaBeta(filho, profundidade - 1, alfa, beta, false);
                    melhorValor = Math.Max(melhorValor, valor);
                    alfa = Math.Max(alfa, melhorValor);
                    if (beta <= alfa)
                        break; // Poda Alfa-Beta
                }
                return melhorValor;
            }
            else
            {
                int melhorValor = int.MaxValue;
                foreach (Nó filho in nó.Filhos)
                {
                    int valor = MinimaxAlfaBeta(filho, profundidade - 1, alfa, beta, true);
                    melhorValor = Math.Min(melhorValor, valor);
                    beta = Math.Min(beta, melhorValor);
                    if (beta <= alfa)
                        break; // Poda Alfa-Beta
                }
                return melhorValor;
            }
        }

        // 1 vitória -1 derrota e 0 empate máquina 'O'
        private void GerarArvore(Nó pai)
        {
            if (pai == null) return;
            if (pai.getAlturaDoNó() + 1 > LIMITE_ALTURA) { return; }
            else
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        /*char[,] campo = new char[9, 9];
                        campo = (char[,])pai.getTabuleiroArray().Clone();
                        */// Criar um novo nó
                        Nó novo_nó = new Nó(pai.getAlturaDoNó() + 1);
                        if (pai.getTabuleiro(i,j) == '-')
                        {
                            novo_nó.setTabuleiro((char[,])pai.getTabuleiroArray().Clone());
                            //impar jogador'x', par máquina joga 'o'
                            if (pai.getAlturaDoNó() % 2 == 0)
                            {
                                novo_nó.setTabuleiro(i, j, 'O');
                                
                            }
                            else
                            {
                                novo_nó.setTabuleiro(i, j, 'X');
                            }
                          
                        }
                        pai.Filhos.Add(novo_nó);
                    }
                }
                foreach (Nó filho in pai.Filhos)
                {
                    GerarArvore(filho);
                    int heuristica = MinimaxAlfaBeta(filho, filho.getAlturaDoNó(), int.MinValue, int.MaxValue, true);
                    filho.Heurística = heuristica;
                }
            }
        }

        // 1 vitória -1 derrota e 0 empate máquina 'O'

        public Nó getHead()
        {
            return head;
        }
        public void printArvore(Nó nó){
         
            if(nó != null)
            {
                
                nó.printTabuleiro();
                foreach(Nó filho in nó.Filhos)
                {
                    printArvore(filho);
                }
            }
            else 
            {
                return;
            }
                //filho.getTabuleiro().printCampo();
            
        }

        internal void initArvore()
        {
            preencheArvore(head, 0, 0);
        }
        // 1 vitória -1 derrota e 0 empate máquina 'O'
        private void preencheArvore(Nó pai, int i, int j)
        {
            if(pai == null)
            {
                return;
            }
            else
            {
                foreach(Nó filho in pai.Filhos)
                {
                    filho.initTabuleiro((char[,])pai.getTabuleiroArray().Clone());

                    //quém marca
                    //impar jogador, par máquina joga
                    if (filho.getTabuleiro(i, j) == '-')
                    {
                        //significa que podemos marcar esse lugar
                        if (filho.getAlturaDoNó() % 2 == 0)
                        {
                    
                           
                            filho.setTabuleiro(i, j, 'O');
                        }
                        else
                        {
                           
                            filho.setTabuleiro(i, j, 'X');
                        }

                    }
                    j++;
                    if(j >= 9)
                    {
                        j = 0;
                        i++;
                    }


                }
                //vai para o proximo filho
                foreach(Nó filho in pai.Filhos)
                {
                    preencheArvore(filho, 0, 0);

                }
            }
           
        }

        

        internal void printheuristica(Nó nó)
        {
           if(nó  == null) { return; }
            else
            {
                Console.WriteLine(nó.Heurística);
                foreach(Nó filho in nó.Filhos)
                {
                    printheuristica(filho);
                }
            }
        }
        //retorna uma string com a posição da melhor jogada
        // 1 vitória -1 derrota e 0 empate máquina 'O'
        public Jogada EncontrarMelhorJogada()
        {
            Nó melhorFilho = null;
            int melhorHeuristica = int.MinValue;

            foreach (Nó filho in head.Filhos)
            {
                if (filho.Heurística > melhorHeuristica)
                {
                    melhorHeuristica = filho.Heurística;
                    melhorFilho = filho;
                }
            }

            if (melhorFilho != null)
            {
                // Encontre a diferença entre o tabuleiro atual e o tabuleiro do melhor filho
                // para obter a jogada realizada pela IA
                char[,] tabuleiroAtual = (char[,])head.getTabuleiroArray();
                char[,] tabuleiroMelhorFilho = (char[,])melhorFilho.getTabuleiroArray();

                Jogada melhorJogada = EncontrarDiferencaEntreTabuleiros(tabuleiroAtual, tabuleiroMelhorFilho);

                return melhorJogada;
            }

            return null;
        }

        private Jogada EncontrarDiferencaEntreTabuleiros(char[,] tabuleiroAnterior, char[,] tabuleiroAtual)
        {
            int tamanho = tabuleiroAnterior.GetLength(0);
            for (int i = 0; i < tamanho; i++)
            {
                for (int j = 0; j < tamanho; j++)
                {
                    if (tabuleiroAnterior[i, j] != tabuleiroAtual[i, j])
                    {
                        // Encontrou a diferença, retorna a jogada correspondente
                        return new Jogada(i, j, tabuleiroAtual[i, j]);
                    }
                }
            }

            return null;
        }

    }
}
