using Dapper;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using Questao5.Infrastructure.Sqlite;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Response;
using Microsoft.Data.Sqlite;
using System.Text.Json;

namespace Questao5.Infrastructure.Data;

public class IdempotencyRepository : IIdempotencyRepository{

    private readonly DatabaseConfig _connection;

    public IdempotencyRepository(DatabaseConfig connection) {
        _connection = connection;
    }

    public void Add(string ididempotency, CreateCurrentAccountActivityCommand command, CreateCurrentAccountActivityResponse response) {
        try
        {
            string idmovimento = string.Empty;
            using (var db = new SqliteConnection(_connection.Name)) {
                string SQL = $"INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado) VALUES (@idempotencykey, @request, @response); ";
                string strRequest = JsonSerializer.Serialize(command);
                string strResponse = JsonSerializer.Serialize(response);
                db.Execute(SQL, new {idempotencykey=ididempotency, request=strRequest, response=strResponse});
            }
        }
        catch (System.Exception)
        {
            // TODO: Log implementation.
        }
    }

    public bool CheckIdempotencyExists(string idindepotency){
        try
        {
            int count;
            using (var db = new SqliteConnection(_connection.Name)) {
                string SQL = $"SELECT count(chave_idempotencia) as qtd FROM idempotencia WHERE chave_idempotencia=@ididempotencia";
                count = db.QuerySingle<int>(SQL, new {ididempotencia=idindepotency});
            }
            return count > 0;
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }

    public CreateCurrentAccountActivityResponse GetByKey(string idindepotency) {
        try
        {
            string strResponse = string.Empty;
            CreateCurrentAccountActivityResponse response = new CreateCurrentAccountActivityResponse();
            using (var db = new SqliteConnection(_connection.Name)) {
                string SQL = $"SELECT resultado FROM idempotencia WHERE chave_idempotencia=@ididempotencia";
                strResponse = db.QuerySingle<string>(SQL, new {ididempotencia=idindepotency});

                if (!string.IsNullOrEmpty(strResponse))
                    response = JsonSerializer.Deserialize<CreateCurrentAccountActivityResponse>(strResponse);
            }
            return response ?? new CreateCurrentAccountActivityResponse();
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
}