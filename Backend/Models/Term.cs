namespace Acadeno.Backend.Models
{
    public class Term
    {
        public string TermID { get; set; } = string.Empty;
        public string TermName { get; set; } = string.Empty;
        public bool IsCurrent { get; set; }
    }
}