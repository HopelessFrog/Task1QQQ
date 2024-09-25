namespace Task1QQQ.Models
{
    public class Substance
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Density { get; set; }
        public int CalorificValue { get; set; }
        public int MinConcentration { get; set; }
        public int MaxConcentration { get; set; }
        public int SubstanceTypeId { get; set; }
    }
}
