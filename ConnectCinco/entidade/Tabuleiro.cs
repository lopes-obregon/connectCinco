using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConnectCinco.entidade
{



    internal class Tabuleiro
    {
        private const int SIZE_X = 5;
        private const int SIZE_Y = 5;
        public char[,] campo = new char[SIZE_X - 1, SIZE_Y - 1 ];
        private Int16 contadorA = 0;
        private Int16 contadorB = 0;
        internal void init()
        {
            for(int i = 0; i < SIZE_X - 1; i++) { 
                for(int j = 0; j < SIZE_Y - 1; j++)
                {
                    campo[i, j] = '-';
                }
            }
        }
        //se retornar falso não tem ganhadores
        public bool isVencedor()
        {
            //verifica em linha
            int jogador_vitorioso_em_linha = verificaEmLinha();
            int jogador_vitorioso_em_coluna = verificaEmConluna();
            int jogador_vitorioso_em_diagonal = verificaEmDiagonal();
            int jogador_vitorioso_em_diagonal2 = verificaEmDiagonal2();
            if (jogador_vitorioso_em_linha == 1 || jogador_vitorioso_em_linha == 2)
            {
                return true;
            }
            else if (jogador_vitorioso_em_coluna == 1 || jogador_vitorioso_em_coluna == 2)
            {
                return true;
            }
            else if (jogador_vitorioso_em_diagonal == 1 || jogador_vitorioso_em_diagonal == 2)
            {
                return true;
            }
            else if (jogador_vitorioso_em_diagonal2 == 1 || jogador_vitorioso_em_diagonal2 == 2)
            {
                return true;
            }
            else
            {
                return false;

            }
        }
        //verifica se tem vitória em diagonal secundária
        public int verificaEmDiagonal2()
        {
            contadorA = 0;
            contadorB = 0;
            for (int i = 0; i < SIZE_X - 1; i++)
            {
                for (int j = SIZE_Y; j < 0; j--)
                {
                    if (i == j)
                    {
                        if (campo[i, j] == 'X') contadorA++;
                        else { contadorA = 0; }
                        if (campo[i, j] == 'O') contadorB++;
                        else { contadorB = 0; }
                    }
                }
                if (contadorA == 5) return 1;
                else if (contadorB == 5) return 2;
                contadorA = contadorB = 0;

            }
            return 0;
        }
        //verifica se tem vitória em diagonal
        public int verificaEmDiagonal()
        {
            contadorA = 0;
            contadorB = 0;
            for(int i = 0; i < SIZE_X - 1; i++) { 
                for(int j = 0; j < SIZE_Y - 1; j++)
                {
                    if(i == j)
                    {
                        if (campo[i,j] == 'X') contadorA++;
                        else { contadorA = 0; }
                        if (campo[i,j] == 'O') contadorB++;
                        else { contadorB = 0; }
                    }
                }
                if (contadorA == 5) return 1;
                else if (contadorB == 5) return 2;
                contadorA = contadorB = 0;

            }
            return 0;
        }
        //verifica se tem vitória em coluna
        public int verificaEmConluna()
        {
            contadorA = 0;
            contadorB = 0;
           for (int i = 0;i < SIZE_X - 1;i++)
            {
                for( int j = 0;j < SIZE_Y - 1;j++)
                {
                    if (campo[j, i] == 'X' ) contadorA++;
                    else { contadorA = 0; }
                    if (campo[j, i] == 'O') contadorB++;
                    else
                    {
                        contadorB = 0;
                    }

                }
                if (contadorA == 5) return 1;
                else if (contadorB == 5) return 2;
                contadorA = contadorB = 0;
            }
            return 0;
        }
        //verificia vitória em linha
        public int verificaEmLinha()
        {

            for (int i = 0; i < SIZE_X - 1; i++)
            {
                for (int j = 0; j < SIZE_Y - 1; j++)
                {
                    if (campo[i, j] == 'X') contadorA++;
                    else
                    {
                        contadorA = 0;
                    }
                   if (campo[i, j] == 'O') contadorB++;
                    else { contadorB = 0;}
                }
                if (contadorA >= 5) return 1;
                else if (contadorB >= 5) return 2;
                contadorA = contadorB = 0;
            }
            return 0; // 0 para não ter vencedor em linha
        }
        //verifica se é vazio se for seta com o valor correspondente
        internal void setCampo(int i, int j, char jogador)
        {
            if (campo[i, j] == '-')
            {
                campo[i, j] = jogador;

            }
            else
            {
                Console.WriteLine("Campo já ocupado");
            }
        }
        //imprime o campo no console
        public void printCampo()
        {
            Console.WriteLine();
            Console.WriteLine("   1 2 3 4 5");
            for(int i = 0;i < SIZE_X - 1;i++)
            {
                Console.Write(i+1 + " ");
                for(int j = 0; j < SIZE_Y - 1; j++)
                {
                    if(j == 0)
                    {
                        Console.Write(" ");
                    }
                    Console.Write(campo[i, j]+ " ");
                }
                Console.WriteLine();
            }
        }
        //faz a jogada da ia
        public void FazerJogada(Jogada jogada)
        {
            int linha = jogada.i;//linha
            int coluna = jogada.j;//coluna
            char jogador = jogada.v;//jogador

            // Verificar se a posição está livre
            if (campo[linha, coluna] == '-')
            {
                // Realizar a jogada
                campo[linha, coluna] = jogador;
                return;
            }
            else
            {
                // Posição já ocupada, jogada inválida, indo  até achar uma jogada válida
                if(linha >= SIZE_X)
                {
                    return;
                }
                if(coluna >= SIZE_Y)
                {
                    coluna = 0;
                    linha++;
                    jogada.i = linha; jogada.j = coluna;
                    FazerJogada(jogada);
                }
                else
                {
                    coluna++;
                    jogada.j = coluna;
                    FazerJogada(jogada);
                }
            }
        }
        public Array getCampo()
        {
            return campo;
        }

    }
}
