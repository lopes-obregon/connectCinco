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
                    Nó game_tree = new Nó((char[,])tabuleiro.getCampo().Clone());
                    GerarGameTree(game_tree, 5);
                    // gameTree.printArvore(gameTree.getHead());
                    //Console.WriteLine("FIM ARVORE ----------------------------------------");
                    //Jogada melhor_jogada = gameTree.EncontrarMelhorJogada();
                    //CORRIGINDO ERROR DE JOGADOR NULL
                    //if (melhor_jogada.v != 'O') melhor_jogada.v = 'O';
                    // tabuleiro.FazerJogada(melhor_jogada);

                }
               

            }
           
        }

        private static void GerarGameTree(Nó nó, int profundidade)
        {
            if(profundidade == 0 || nó.isTerminal(profundidade)) {
                return;
            }
            //Gere as jogadas possíveis a partir do estado atual do jogo
            List<(int linha, int coluna)> movimentos = nó.gerar_movimentos();
            foreach(var movimento in movimentos)
            {
                //Crie um novo nó para representar o estado resultante da jogada
                var novo_estado = AplicarMovimento((char[,])nó.getEstado().Clone(), movimento, profundidade);
                Nó novo_nó = new Nó((char[,])novo_estado);
                //adiciona  o novo nó como filho do nó atual
                nó.AddFilho(novo_nó);
                //gerar recursivamente  a arvore
                GerarGameTree(novo_nó, profundidade - 1);
            }
        }

        private static Array AplicarMovimento(char[,] estado_atual, (int linha, int coluna) movimento, int profundidade)
        {
            /*par para máquina ou seja representado por MAX(O)
             *impar para jogador adversário ou seja representado por MIN(X)
             *
             ***/
            estado_atual[movimento.linha, movimento.coluna] = profundidade % 2 == 0 ? '0' : 'X';
            return estado_atual;
        }
    }
}