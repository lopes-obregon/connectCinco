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
            GameTree gameTree = new GameTree(tabuleiro);

            //raiz.printArvore(raiz.getHead());
            //raiz.printheuristica(raiz.getHead());
            Jogada melhor_jogada = gameTree.EncontrarMelhorJogada();
            tabuleiro.FazerJogada(melhor_jogada);

            /*while (true)
            {
                
            }*/
        }
    }
}