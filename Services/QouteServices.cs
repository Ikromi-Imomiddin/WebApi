using Dapper;
using Npgsql;
using Domain;
namespace Services;
public class QouteServices
{
    private string _connectionString;
    public QouteServices()
    {
        _connectionString = "Server=127.0.0.1;Port=5432;Database=NextTryWebApi;User Id=postgres;Password=Ikromi8008;";
    }

    public async Task<List<Quote>> GetQuotes()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            var sql = "select * from Quote ;";
            var result = await connection.QueryAsync<Quote>(sql);
            return result.ToList();
        }
    }

     public async Task<List<QuoteWithCategoryDto>> GetQuotesWithCategoryName(int CategoryId)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            var sql ="SELECT q.id,q.quotetext,q.author,c.categoryname FROM Category as c INNER JOIN Quote as q ON q.Categoryid = c.id;";
            var result = await connection.QueryAsync<QuoteWithCategoryDto>(sql);
            return result.ToList();
        }
    }


    public async Task<string> AddQuote(Quote quote)
    {

        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            try

            {
                string? sql = $"Insert into Quote (Author,Quotetext,CategoryId) VAlUES ('{quote.Author}','{quote.Quotetext}',{quote.CategoryId})";

                {
                    var result = await connection.ExecuteAsync(sql);
                    return "Seccess";
                }
            }
            catch (Exception ex)

            {
                return $"Vary bad{ex.Message}";
            }

    }
    public async Task<string> DeleteQuote(int id)
    {

        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            string sql = $"delete from Quote where Id = '{id}';";
            try
            {

                var result = await connection.ExecuteAsync(sql);
                return "Seccess";
            }

            catch (Exception ex)
            {
                return $"Vary bad{ex.Message}";

            }
        }
    }
    public async Task<string> UpdateQuote(Quote quote)
    {

        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            string sql = $"UPDATE Quote SET Author = '{quote.Author}', CategoryId = '{quote.CategoryId}' WHERE Id = {quote.Id};";
            try
            {
                var result = await connection.ExecuteAsync(sql);
                return $"Seccess";
            }

            catch (Exception ex)
            {
                return $"Vary bad{ex.Message}";

            }
        }
    }
    public async Task<List<Quote>> GetAllQuotesByCategory(int id)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))

        {
            string sql = ($"select * from Quote where CategoryId = {id} ;");
            try
            {

                var result = await connection.QueryAsync<Quote>(sql);
                return result.ToList();
            }

            catch (Exception ex)
            {
                return  null;
            }
        }
    }

    public async Task<string> GetRandom(int id)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            string sql = ($"select * from Quote order by random() Limit 1 ;");
            try
            {

                var result = await connection.ExecuteAsync(sql);
                return "Seccess";
            }
            catch (Exception ex)
            {
                return $"Vary bad{ex.Message}";
            }

        }
    }
}

