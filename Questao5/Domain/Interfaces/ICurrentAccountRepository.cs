namespace Questao5.Domain.Interfaces
{
    public interface ICurrentAccountRepository
    {
        public bool CheckByAccountNumber(int AccountNumber);
        public bool CheckCurrentAccountActive(int AccountNumber);
        public double? GetBalanceByAccountNumber(int accoutNumber);
        public string GetNameByAccountNumber(int accoutNumber);
    }
}
