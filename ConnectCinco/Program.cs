using ConnectCinco.entidade;

namespace ConnectCinco
{
    internal class Program
    {
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
                   // gameTree.printArvore(gameTree.getHead());
                    //Console.WriteLine("FIM ARVORE ----------------------------------------");
                    Jogada melhor_jogada = gameTree.EncontrarMelhorJogada();
                    //CORRIGINDO ERROR DE JOGADOR NULL
                    if (melhor_jogada.v != 'O') melhor_jogada.v = 'O';
                    tabuleiro.FazerJogada(melhor_jogada);

                }
               

            }
           
        }
    }
}