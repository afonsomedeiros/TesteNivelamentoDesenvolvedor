using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Exceptions;
using Questao5.Domain.Interfaces;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Data
{
    public class CurrentAccountRepository : ICurrentAccountRepository
    {
        private readonly DatabaseConfig _connection;

        public CurrentAccountRepository(DatabaseConfig connection)
        {
            _connection = connection;
        }

        public bool CheckByAccountNumber(int AccountNumber)
        {
            int count;
            using (var db = new SqliteConnection(_connection.Name))
            {
                string SQL = $"SELECT count(idcontacorrente) FROM contacorrente where numero=@numero";
                count = db.QuerySingle<int>(SQL, new { numero = AccountNumber });
            }
            if (count <= 0)
                throw new InvalidAccountException("Conta invalida. Apenas contas registradas podem realizar movimentação.");

            return true;
            
        }

        public bool CheckCurrentAccountActive(int AccountNumber)
        {
            int count;
            using (var db = new SqliteConnection(_connection.Name))
            {
                string SQL = $"SELECT ativo FROM contacorrente where numero=@numero";
                count = db.QuerySingle<int>(SQL, new { numero = AccountNumber });
            }
            if (count <= 0)
                throw new InactiveAccountException("Conta Inativa. Apenas contas ativas podem realizar movimentação.");

            return true;

        }

        public double? GetBalanceByAccountNumber(int accountNumber)
        {
            try
            {
                double? balance = 0.0;
                using (var db = new SqliteConnection(_connection.Name))
                {
                    string SQL = $"SELECT SUM (CASE WHEN tipomovimento = 'C' THEN valor ELSE -valor END) AS saldo FROM movimento as m INNER JOIN contacorrente as cc on cc.idcontacorrente = m.idcontacorrente WHERE cc.numero = @numero";
                    balance = db.QuerySingle<double>(SQL, new { numero = accountNumber });
                }
                return balance;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string GetNameByAccountNumber(int accountNumber)
        {
            string name = string.Empty;
            using (var db = new SqliteConnection(_connection.Name))
            {
                string SQL = $"SELECT nome FROM contacorrente WHERE numero = @numero";
                name = db.QuerySingle<string>(SQL, new { numero = accountNumber });
            }
            return name;
        }
    }
}
