namespace Task1QQQ.Models
{
    public class Substance
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Density { get; set; }
        public decimal CalorificValue { get; set; }
        public int MinConcentration { get; set; }
        public int MaxConcentration { get; set; }
        public SubstanceType SubstanceType { get; set; }
    }
}
