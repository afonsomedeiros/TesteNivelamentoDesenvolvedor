using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Response;

namespace Questao5.Domain.Entities;

public class Idempotency{
    public Guid IdempotencyKey { get; set; }
    public CreateCurrentAccountActivityCommand Command { get; set; }
    public CreateCurrentAccountActivityResponse Response { get; set; }


    public Idempotency(Guid IdempotencyKey, CreateCurrentAccountActivityCommand Command, CreateCurrentAccountActivityResponse Response) {
        this.IdempotencyKey = IdempotencyKey;
        this.Command = Command;
        this.Response = Response;
    }

}