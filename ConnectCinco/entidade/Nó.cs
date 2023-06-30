using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectCinco.entidade
{
    internal class Nó
    {
        private const int SIZE_X = 5;
        private const int SIZE_Y = 5;
        private int altura_do_nó;
        private char[,] tabuleiro = new char[SIZE_X, SIZE_Y];
        public int Heurística { get; set; }
        public List<Nó> Filhos { get; set; }
        public Nó(int altura_do_nó)
        {
            this.altura_do_nó = altura_do_nó;
            Heurística = -3;
            Filhos = new List<Nó>();
        }
        public void initTabuleiro(char[,] tabuleiro)
        {
            this.tabuleiro = tabuleiro;
        }
        public void setTabuleiro(int i, int j, char valor)
        {
            this.tabuleiro[i, j] = valor;
        }
        public int getAlturaDoNó() { return altura_do_nó; }

        internal char getTabuleiro(int i, int j)
        {
           return this.tabuleiro[i, j];
        }

        internal void printTabuleiro()
        {
           
                Console.WriteLine();
          
            Console.WriteLine("  1 2 3 4 5");
                for (int i = 0; i < SIZE_X; i++)
                {
                Console.Write((i + 1) + " ");
                    
                    for (int j = 0; j < SIZE_Y; j++)
                    {
                        
                      Console.Write(tabuleiro[i, j] + " ");
                    }
                    Console.WriteLine();
                }
            
        }

        internal Array getTabuleiroArray()
        {
            return this.tabuleiro;
        }

        public int CalcularHeuristica()
        {
            if (VerificarSequencia('O'))
            {
                // Jogador 'O' venceu
                return 1;
            }
            else if (VerificarSequencia('X'))
            {
                // Jogador 'X' venceu
                return -1;
            }
            else
            {
                // Nenhum jogador venceu, é um empate ou estado neutro
                return 0;
            }
        }

        private bool VerificarSequencia(char jogador)
        {
            // Verificar sequências de cinco em linha
            for (int i = 0; i < SIZE_X; i++)
            {
                for (int j = 0; j <= SIZE_Y - 4; j++)
                {
                    if (tabuleiro[i, j] == jogador &&
                        tabuleiro[i, j + 1] == jogador &&
                        tabuleiro[i, j + 2] == jogador &&
                        tabuleiro[i, j + 3] == jogador &&
                        tabuleiro[i, j + 4] == jogador)
                    {
                        return true;
                    }
                }
            }

            // Verificar sequências de cinco em coluna
            for (int j = 0; j < 5; j++)
            {
                for (int i = 0; i <= SIZE_X  - 4; i++)
                {
                    if (tabuleiro[i, j] == jogador &&
                        tabuleiro[i + 1, j] == jogador &&
                        tabuleiro[i + 2, j] == jogador &&
                        tabuleiro[i + 3, j] == jogador &&
                        tabuleiro[i + 4, j] == jogador)
                    {
                        return true;
                    }
                }
            }

            // Verificar sequências de cinco em diagonal
            for (int i = 0; i <= SIZE_X  - 4; i++)
            {
                for (int j = 0; j <= SIZE_Y - 4; j++)
                {
                    if (tabuleiro[i, j] == jogador &&
                        tabuleiro[i + 1, j + 1] == jogador &&
                        tabuleiro[i + 2, j + 2] == jogador &&
                        tabuleiro[i + 3, j + 3] == jogador &&
                        tabuleiro[i + 4, j + 4] == jogador)
                    {
                        return true;
                    }
                }
            }

            // Verificar sequências de cinco em diagonal invertida
            for (int i = 4; i < SIZE_X; i++)
            {
                for (int j = 0; j <= SIZE_Y - 4; j++)
                {
                    if (tabuleiro[i, j] == jogador &&
                        tabuleiro[i - 1, j + 1] == jogador &&
                        tabuleiro[i - 2, j + 2] == jogador &&
                        tabuleiro[i - 3, j + 3] == jogador &&
                        tabuleiro[i - 4, j + 4] == jogador)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        internal void setTabuleiro(char[,] chars)
        {
            this.tabuleiro = chars;
        }
    }
    
}
