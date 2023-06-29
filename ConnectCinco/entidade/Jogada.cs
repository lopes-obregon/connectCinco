namespace ConnectCinco.entidade
{
    internal class Jogada
    {
        public int i { get; set; }
        public int j { get; set; }
        public char v { get; set; }

        public Jogada(int i, int j, char v)
        {
            this.i = i;
            this.j = j;
            this.v = v;
        }
    }
}