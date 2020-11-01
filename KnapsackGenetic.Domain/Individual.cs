namespace KnapsackGenetic.Domain
{
    public class Individual
    {
        public bool[] Genes { get; set; }

        public Individual Clone()
        {
            return new Individual
            {
                Genes = (bool[])this.Genes.Clone()
            };
        }
    }
}
