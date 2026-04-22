namespace Acadeno.Backend.Models.Education.Terms
{
    public class Term
    {
        public string TermID { get; set; } = string.Empty;
        public string TermName { get; set; } = string.Empty;
        public bool IsCurrent { get; set; }
    }
}