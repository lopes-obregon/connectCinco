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
        public char[,] campo = new char[9, 9];
        private Int16 contadorA = 0;
        private Int16 contadorB = 0;
        internal void init()
        {
            for(int i = 0; i < 9; i++) { 
                for(int j = 0; j < 9; j++)
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

        public int verificaEmDiagonal2()
        {
            contadorA = 0;
            contadorB = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 9; j < 0; j--)
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

        public int verificaEmDiagonal()
        {
            contadorA = 0;
            contadorB = 0;
            for(int i = 0; i < 9; i++) { 
                for(int j = 0; j < 9; j++)
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

        public int verificaEmConluna()
        {
            contadorA = 0;
            contadorB = 0;
           for (int i = 0;i < 9;i++)
            {
                for( int j = 0;j < 9;j++)
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

        public int verificaEmLinha()
        {

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
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

        internal void setCampo(int i, int j, char jogador)
        {
            campo[i, j] = jogador;
        }
        public void printCampo()
        {
            Console.WriteLine();
            for(int i = 0;i < 9;i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    Console.Write(campo[i, j]);
                }
                Console.WriteLine();
            }
        }

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
            }
            else
            {
                // Posição já ocupada, jogada inválida
                Console.WriteLine("Jogada inválida!");
            }
        }

    }
}
