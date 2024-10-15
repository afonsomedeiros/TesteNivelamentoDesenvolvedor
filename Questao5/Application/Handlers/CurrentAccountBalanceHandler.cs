using Questao5.Application.Commands.Requests;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using Questao5.Application.Commands.Response;
using Questao5.Domain.Exceptions;
using Questao5.Application.Queries.Requests;

namespace Questao5.Application.Handlers;

public class CurrentAccountBalanceHandler {
    private readonly ICurrentAccountRepository _currentAccount;

    public CurrentAccountBalanceHandler(ICurrentAccountRepository currentAcount){
        _currentAccount = currentAcount;
    }

    public CurrentAccountBalanceResponse Handle(int AccountNumber){
        try
        {
            Validate(AccountNumber);
            double? balance = _currentAccount.GetBalanceByAccountNumber(AccountNumber);

            if (!(balance != null))
                return new CurrentAccountBalanceResponse() { Message = "Nenhuma movimentação para a conta informada." };

            return new CurrentAccountBalanceResponse()
            {
                Balance = balance,
                Name = _currentAccount.GetNameByAccountNumber(AccountNumber),
                AccountNumber = AccountNumber,
                ResponseDate = DateTime.Now
            };

        }
        catch (InvalidAccountException ex)
        {
            return new CurrentAccountBalanceResponse() { Message = ex.Message };
        }
        catch (InactiveAccountException ex)
        {
            return new CurrentAccountBalanceResponse() { Message = ex.Message };
        }
    }

    private void Validate(int AccountNumber)
    {
        _currentAccount.CheckByAccountNumber(AccountNumber);
        _currentAccount.CheckCurrentAccountActive(AccountNumber);
    }
}