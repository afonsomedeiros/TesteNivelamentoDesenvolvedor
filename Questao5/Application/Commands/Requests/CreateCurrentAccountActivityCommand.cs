namespace Questao5.Application.Commands.Requests;

public class CreateCurrentAccountActivityCommand {
    public string IdempotencyKey { get; set; } = string.Empty;
    public int AccountNumber { get; set; }
    public double Value { get; set; }
    public char ActivityType { get; set; }
}