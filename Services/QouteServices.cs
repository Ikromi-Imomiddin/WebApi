using Dapper;
using Domain;
using Infrastructure.DataContext;
namespace Services;
public class QouteServices
{
    private DataContext _datacontext;
    public QouteServices(DataContext context)
    {
        _datacontext = context;
    } 

    public async Task<List<Quote>> GetQuotes()
    {
       await using var connection = _datacontext.CreateConnection();
        {
            var sql = "select * from Quote ;";
            var result = await connection.QueryAsync<Quote>(sql);
            return result.ToList();
        }
    }

     public async Task<List<QuoteWithCategoryDto>> GetQuotesWithCategoryName(int CategoryId)
    {
        await using var connection = _datacontext.CreateConnection();
        {
            var sql ="SELECT q.id,q.quotetext,q.author,c.categoryname FROM Category as c INNER JOIN Quote as q ON q.Categoryid = c.id;";
            var result = await connection.QueryAsync<QuoteWithCategoryDto>(sql);
            return result.ToList();
        }
    }


    public async Task<string> AddQuote(Quote quote)
    {

        await using var connection = _datacontext.CreateConnection();
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

       await using var connection = _datacontext.CreateConnection();
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

       await using var connection = _datacontext.CreateConnection();
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
        await using var connection = _datacontext.CreateConnection();
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
        await using var connection = _datacontext.CreateConnection();
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

