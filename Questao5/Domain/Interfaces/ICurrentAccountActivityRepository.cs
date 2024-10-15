using Questao5.Domain.Entities;

namespace Questao5.Domain.Interfaces;

public interface ICurrentAccountActivityRepository{
    string Add(string AccountNumber, CurrentAccountActivity currentAccountActivity);
}