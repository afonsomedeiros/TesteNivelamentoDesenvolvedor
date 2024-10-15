using Questao5.Application.Commands.Requests;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using Questao5.Application.Commands.Response;
using Questao5.Domain.Exceptions;

namespace Questao5.Application.Handlers;

public class CreateCurrentAccountActivityHandler {
    private readonly ICurrentAccountActivityRepository _currentAccountActivity;
    private readonly ICurrentAccountRepository _currentAccount;
    private readonly IIdempotencyRepository _idempotencyRepository;

    public CreateCurrentAccountActivityHandler(ICurrentAccountActivityRepository currentAccountActivity, IIdempotencyRepository idempotencyRepository, ICurrentAccountRepository currentAcount){
        _currentAccountActivity = currentAccountActivity;
        _currentAccount = currentAcount;
        _idempotencyRepository = idempotencyRepository;
    }

    public CreateCurrentAccountActivityResponse Handle(CreateCurrentAccountActivityCommand command){
        try
        {
            Validate(command);

            var currentAccountActivity = new CurrentAccountActivity(Guid.NewGuid(), new CurrentAccount(), DateTime.Now, command.ActivityType, command.Value);

            bool existIdenpotency = _idempotencyRepository.CheckIdempotencyExists(command.IdempotencyKey);
            if (!existIdenpotency)
            {
                string idmovimento = _currentAccountActivity.Add(command.AccountNumber.ToString(), currentAccountActivity);

                var response = new CreateCurrentAccountActivityResponse() { IDMovimento = idmovimento };

                _idempotencyRepository.Add(command.IdempotencyKey, command, response);

                return new CreateCurrentAccountActivityResponse() { IDMovimento = idmovimento };
            }
            else
            {
                return _idempotencyRepository.GetByKey(command.IdempotencyKey);
            }
        }
        catch (InvalidValueException ex)
        {
            return new CreateCurrentAccountActivityResponse() { Message = ex.Message };
        }
        catch (InvalidTypeException ex)
        {
            return new CreateCurrentAccountActivityResponse() { Message = ex.Message };
        }
        catch (InvalidAccountException ex)
        {
            return new CreateCurrentAccountActivityResponse() { Message = ex.Message };
        }
        catch (InactiveAccountException ex)
        {
            return new CreateCurrentAccountActivityResponse() { Message = ex.Message };
        }
    }

    private void Validate(CreateCurrentAccountActivityCommand command)
    {
        if (command.Value < 0)
            throw new InvalidValueException("Valor invalido. Valor deve ser um valor positivo.");

        if (!char.ToLower(command.ActivityType).Equals("c") && char.ToLower(command.ActivityType).Equals("d"))
            throw new InvalidTypeException("Tipo de movimento invalido. Os tipos aceitos são C - Crédito e D - Débito.");

        _currentAccount.CheckByAccountNumber(command.AccountNumber);
        _currentAccount.CheckCurrentAccountActive(command.AccountNumber);
    }
}