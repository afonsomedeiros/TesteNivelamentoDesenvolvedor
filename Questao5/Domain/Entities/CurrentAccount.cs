using System;

namespace Questao5.Domain.Entities;

public class CurrentAccount{
    public Guid CurrentAccountID { get; private set; }
    public int AccountNumber { get; private set; }
    public string? Name { get; private set; }
    public bool Active { get; private set; }

    public CurrentAccount() {}
    public CurrentAccount(Guid CurrentAccountID, int AccountNumber, string Name, bool active) {
        this.CurrentAccountID = CurrentAccountID;
        this.AccountNumber = AccountNumber;
        this.Name = Name;
        this.Active = active;
    }

}