using Dapper;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using Questao5.Infrastructure.Sqlite;
using Microsoft.Data.Sqlite;

namespace Questao5.Infrastructure.Data;

public class CurrentAccountActivityRepository : ICurrentAccountActivityRepository{

    private readonly DatabaseConfig _connection;

    public CurrentAccountActivityRepository(DatabaseConfig connection) {
        _connection = connection;
    }

    public string Add(string AccountNumber, CurrentAccountActivity currentAccountActivity) {
        try
        {
            int rowsaffecteds;
            using (var db = new SqliteConnection(_connection.Name)) {
                string SQL = $"INSERT INTO movimento VALUES (@idmovimento, (SELECT idcontacorrente FROM contacorrente where numero=@idcontacorrente), @datamovimento, @tipomovimento, @valor); ";

                rowsaffecteds = db.Execute(SQL, new {
                    idmovimento = currentAccountActivity.CurrentAccountID,
                    idcontacorrente = AccountNumber,
                    datamovimento = currentAccountActivity.ActivityDate,
                    tipomovimento = currentAccountActivity.ActivityType,
                    valor = currentAccountActivity.Value
                });
            }
            return rowsaffecteds > 0 ? currentAccountActivity.CurrentAccountID.ToString() : "";
        }
        catch (System.Exception)
        {
            // TODO: Log implementation.
            return "";
        }
    }

    public string GetByIdIndepotency(string idindepotency){
        try
        {
            string idmovimento = string.Empty;
            using (var db = new SqliteConnection(_connection.Name)) {
                string SQL = $"SELECT chave_idempotencia FROM idempotencia WHERE chave_idempotencia=@ididempotencia";
                idindepotency = db.QuerySingle<string>(SQL, new {ididempotencia=idindepotency});
            }
            return idmovimento;
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
}