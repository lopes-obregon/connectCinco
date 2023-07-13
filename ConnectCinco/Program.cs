using ConnectCinco.entidade;

namespace ConnectCinco
{
    internal class Program
    {
        private  const int ALTURA_MAXIMA = 5;
        private const int TABULEIRO_X = 5;
        private const int TABULEIRO_Y = 5;
        private static int nós = 0;
        static void Main(string[] args)
        {
            int opção = 0;
            Console.WriteLine("--------------------------");
            Console.WriteLine("0 - Sair\t 1- IA versus\t 2- PVP");
            opção = int.Parse(Console.ReadLine());
            switch (opção)
            {
                case 1:
                    IaVersus();
                    break;
                case 2:
                    Pvp();
                    break;
                default:
                    break;
                
            }
            
        }

        private static void Pvp()
        {
            Console.WriteLine("PVP");
                Tabuleiro tabuleiro = new Tabuleiro();
                tabuleiro.init();
            var jogador_O = true;
            while (true)
            {
                Console.Write("Jogador: ");
                if (jogador_O) Console.WriteLine("O");
                else Console.WriteLine("X");
                tabuleiro.printCampo();
                Console.WriteLine("Informe a linha!");
                int linha = int.Parse(Console.ReadLine());
                Console.WriteLine("Informe a coluna!");
                int coluna = int.Parse(Console.ReadLine());
                tabuleiro.setCampo(linha - 1, coluna - 1, jogador_O ? 'O' : 'X');
                jogador_O = jogador_O == false ? true : false;
                if (tabuleiro.isVencedor())
                {
                    Console.WriteLine("Temos um vencedor!");
                    break;
                }
            }
        }

        private static void IaVersus()
        {
            var profundidade = 5;
            Console.WriteLine("IA VERSUS!");
            Tabuleiro tabuleiro = new Tabuleiro();
            tabuleiro.init();
           
            
            
            while(true)
            {
                
                
                
                Nó game_tree = new Nó((char[,])tabuleiro.getCampo().Clone());
                GerarGameTree(game_tree, profundidade);
                Tuple<int,int> melhor_jogada = null;
                var valor = 0;
                (valor, melhor_jogada) = MinimaxAlphabeta(game_tree, profundidade, int.MinValue, int.MaxValue, true);
                tabuleiro.setCampo(melhor_jogada.Item1, melhor_jogada.Item2, 'O');
               
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
               

            }
           
        }

        private static (int, Tuple<int, int>) MinimaxAlphabeta(Nó nó, int profundidade, int alpha, int beta, bool jogador_max)
        {
            if (profundidade == 0 || nó.isTerminal(profundidade)) return (nó.avaliação(profundidade), nó.getMovimento());
            if(jogador_max)
            {   //melhor pontuação
                var  valor = int.MinValue;
                Tuple<int,int> melhor_movimento = new Tuple<int, int>(-2, -2);
                foreach (var filho in nó.Filhos)
                {
                    int valor_do_filho;
                    Tuple<int, int> movimento_do_filho = null;
                    (valor_do_filho, movimento_do_filho) = MinimaxAlphabeta(filho, profundidade - 1, alpha, beta, false);
                    if (valor_do_filho > valor)
                    {
                        valor = valor_do_filho;
                        melhor_movimento = movimento_do_filho;
                    }
                    alpha = Math.Max(alpha, valor);
                    if (beta <= alpha) break; //poda alfa-beta
                }
                return (valor, melhor_movimento);
            }
            else
            {
                var valor = int.MaxValue;
                Tuple<int,int> melhor_movimento = new Tuple<int, int>(-2,-2);//corrigir error de retorno
                foreach(var filho in nó.Filhos)
                {
                    int valor_do_filho;
                    Tuple<int, int> movimento_do_filho = null;
                    (valor_do_filho, movimento_do_filho) = MinimaxAlphabeta(filho, profundidade - 1, alpha, beta, true);
                    if(valor_do_filho < valor)
                    {
                        valor = valor_do_filho;
                        melhor_movimento = movimento_do_filho;
                    }
                    beta = Math.Min(beta, valor);
                    if(beta <= alpha) break; // poda alfa beta

                }
                return (valor, melhor_movimento);
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
                Nó novo_nó = new Nó((char[,])novo_estado, movimento);
                //adiciona  o novo nó como filho do nó atual.
                nós++;
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

    internal record struct NewStruct(int valor, Tuple<int, int> Item2)
    {
        public static implicit operator (int valor, Tuple<int, int>)(NewStruct value)
        {
            return (value.valor, value.Item2);
        }

        public static implicit operator NewStruct((int valor, Tuple<int, int>) value)
        {
            return new NewStruct(value.valor, value.Item2);
        }
    }
}