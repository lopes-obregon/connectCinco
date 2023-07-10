using ConnectCinco.entidade;

namespace ConnectCinco
{
    internal class Program
    {
        private  const int ALTURA_MAXIMA = 5;
        private const int TABULEIRO_X = 5;
        private const int TABULEIRO_Y = 5;
        static void Main(string[] args)
        {
            int opção = 0;
            Console.WriteLine("--------------------------");
            Console.WriteLine("0 - Sair\t 1- IA versus");
            opção = int.Parse(Console.ReadLine());
            switch (opção)
            {
                case 1:
                    IaVersus();
                    break;
                default:
                    break;
                
            }
            
        }

        private static void IaVersus()
        {
            Console.WriteLine("IA VERSUS!");
            Tabuleiro tabuleiro = new Tabuleiro();
            tabuleiro.init();
            //raiz.printArvore(raiz.getHead());
            //raiz.printheuristica(raiz.getHead());
            
            
            while(true)
            {
                tabuleiro.printCampo();
                Console.WriteLine("Informe a linha!");
                int linha = int.Parse(Console.ReadLine());
                Console.WriteLine("Informe a coluna!");
                int coluna = int.Parse(Console.ReadLine());
                tabuleiro.setCampo(linha - 1, coluna - 1, 'X');
                tabuleiro.printCampo();
                if (tabuleiro.isVencedor())
                {
                    Console.WriteLine("Temos um vencedor!");
                    break;
                }
                else
                {
                    //Console.WriteLine("Inicio arvore!--------------------------------------");
                    GameTree gameTree = new GameTree(tabuleiro);
                    GerarArvore(gameTree.getHead(), 0);
                   // gameTree.printArvore(gameTree.getHead());
                    //Console.WriteLine("FIM ARVORE ----------------------------------------");
                    //Jogada melhor_jogada = gameTree.EncontrarMelhorJogada();
                    //CORRIGINDO ERROR DE JOGADOR NULL
                    //if (melhor_jogada.v != 'O') melhor_jogada.v = 'O';
                   // tabuleiro.FazerJogada(melhor_jogada);

                }
               

            }
           
        }

        private static void GerarArvore(Nó pai, int index)
        {
            if (pai == null) return;
            else
            {
                GerarFilhos(pai.getAlturaDoNó()+1, pai);
               /* foreach(Nó filho in pai.Filhos)
                {
                    GerarFilhos(filho.getAlturaDoNó() + 1, filho);
                }*/
               //existe pelomenos 1 filho
               if(pai.Filhos.Count > 0)
                {
                    GerarArvore(pai.Filhos[index], index+1);
                }
            }
        }

        private static void GerarFilhos(int altura, Nó pai)
        {
            if (altura >= ALTURA_MAXIMA)
            {
                return;
            }
            else
            {

                for(int i = 0; i < TABULEIRO_X; i++)
                {
                    for(int j = 0; j < TABULEIRO_Y; j++)
                    {
                        if(pai.getTabuleiro(i,j) == '-')
                        {
                            Nó novo_nó = new Nó(altura);
                            novo_nó.initTabuleiro((char[,])pai.getTabuleiroArray().Clone());
                            //max 'X'
                            if(pai.getAlturaDoNó()%2 == 0)
                            {
                                novo_nó.setTabuleiro(i, j, 'O');
                            }
                            else
                            {
                                //min 'O'
                                novo_nó.setTabuleiro(i, j, 'X');
                            }
                            pai.Filhos.Add(novo_nó);

                        }
                    }
                }
            }
        }
    }
}