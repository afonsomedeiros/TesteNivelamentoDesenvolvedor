namespace Questao5.Application.Queries.Requests
{
    public class CurrentAccountBalanceResponse
    {
        public double? Balance { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime ResponseDate { get; set; }
        public int AccountNumber { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
