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
        private char[,] tabuleiro = new char[SIZE_X - 1, SIZE_Y - 1];
        public int Heurística { get; set; }
        public List<Nó> Filhos { get; set; }
        public Nó(int altura_do_nó)
        {
            this.altura_do_nó = altura_do_nó;
            Heurística = -3;
            Filhos = new List<Nó>();
        }
        public Nó(char[,] state)
        {
            this.tabuleiro = state;
            Filhos = new List<Nó>();
        }

        public void  AddFilho(Nó filho)
        {
            this.Filhos.Add(filho);
        }

        internal List<(int linha, int coluna)> gerar_movimentos()
        {
            List<(int linha, int coluna)> movimento = new List<(int linha, int coluna)>();
            //percorrer toda as linhas e colunas do tabuleiro
            for(int i = 0; i < SIZE_X - 1; i++) { 
                for(int j = 0; j < SIZE_Y - 1; j++)
                {
                    if (tabuleiro[i,j] == '-')
                    {
                        //elemento ausente na tabela
                        movimento.Add((i, j));
                    }
                }
            }
            return movimento;
        }

        internal bool isTerminal(int profundidade)
        {
            //método para verificar se o nó em questão é terminal se existe algum vencedor
            if(isVencedor(profundidade))
            {
                return true;
            }
            else
            {
                return false;

            }
        }
        //retorna se tamos um vencedor como sendo máquina ou jogador
        private bool isVencedor(int profundidade)
        {
            
            /*par para máquina ou seja representado por MAX(O)
             *impar para jogador adversário ou seja representado por MIN(X)
             *
             ***/
            var jogador = profundidade % 2 == 0 ? '0' : 'X';
            int vitória_em_linha = 0;//flag da vitória em linha
            int vitória_em_coluna = 0;//flag da vitória em coluna
            int vitória_em_diagonal = 0;// flag da vitória em diagonal
            int vitória_em_diagonal_dois = 0; //flag da vitória em diagonal dois 
            for(int i = 0; i < SIZE_X - 1; i++)
            {
                for(int j = 0;j < SIZE_Y - 1; j++)
                {   
                    //condição para vitória em linha
                    if (tabuleiro[i, j] == jogador) vitória_em_linha++;
                    else vitória_em_linha = 0;
                    //verificar se vitória está em diagonal principal 
                    if( i == j)
                    {
                        //vitória para coluna
                        if (tabuleiro[i,j] == jogador) vitória_em_diagonal++;
                        else vitória_em_diagonal = 0;

                    }
                    //vitória em coluna
                    if (tabuleiro[j, i] == jogador) vitória_em_coluna++;
                    else vitória_em_coluna = 0;
                    //vitória diagonal dois
                    if(i + j == SIZE_X - 1) vitória_em_diagonal_dois++;
                    else vitória_em_diagonal_dois = 0;

                }
                //se maior que cinco temos uma vitória em linha
                if (vitória_em_linha >= 5) return true;
                //se maior que cinco temos vitória em coluna 
                else if (vitória_em_coluna >= 5) return true;
                //vitória em cinco de diagonal
                else if(vitória_em_diagonal >= 5) return true;
                else if(vitória_em_diagonal_dois >= 5) return true;

            }
            return false;
        }

        internal Array getEstado()
        {
            return tabuleiro;
        }
    }
}
