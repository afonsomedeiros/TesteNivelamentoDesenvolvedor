using System;

namespace Questao5.Domain.Entities;

public class CurrentAccountActivity{
    public Guid CurrentAccountID { get; private set; }
    public CurrentAccount currentAccount { get; private set; }
    public DateTime ActivityDate { get; private set; }
    public char ActivityType { get; private set; }
    public double Value { get; private set; }

    public CurrentAccountActivity(Guid CurrentAccountID, CurrentAccount currentAccount, DateTime ActivityDate, char ActivityType, double Value) {
        this.CurrentAccountID = CurrentAccountID;
        this.currentAccount = currentAccount;
        this.ActivityDate = ActivityDate;
        this.ActivityType = ActivityType;
        this.Value = Value;
    }

}