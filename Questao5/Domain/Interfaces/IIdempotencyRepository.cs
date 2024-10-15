using Questao5.Domain.Entities;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Response;

namespace Questao5.Domain.Interfaces;

public interface IIdempotencyRepository{
    void Add(string ididempotency, CreateCurrentAccountActivityCommand command, CreateCurrentAccountActivityResponse response);

    bool CheckIdempotencyExists(string idindepotency);
    CreateCurrentAccountActivityResponse GetByKey(string idindepotency);
}