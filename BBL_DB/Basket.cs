using BBL.DTOModels;
using BBL.ServiceInterfaces;
using DAL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BBL_DB
{
    public class Basket : IBasket
    {
        private readonly WebshopContext dbContext;
        public Basket(WebshopContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool AddProductToBasket(BasketRequest basket)
        {
            try
            {
                var sql = @"INSERT INTO BasketPosition (ProductId, Amount, UserId) 
                       VALUES (@ProductId, @Amount, @UserId)";
                var parameters = new[]
                {
                    new SqlParameter("@UserId", basket.UserId),
                    new SqlParameter("@ProductId", basket.ProductId),
                    new SqlParameter("@Amount", basket.Amount)
                };

                dbContext.Database.ExecuteSqlRaw(sql, parameters);
            }
            catch (Exception ex) { return false; }
            return true;
        }

        public bool ChangeBasketAmount(BasketRequest basket)
        {
            try
            {
                var sql = @"
            UPDATE BasketPosition
            SET Amount = @Amount
            WHERE UserId = @UserId 
            AND ProductId = @ProductId";

                var parameters = new[]
                {
            new SqlParameter("@UserId", basket.UserId),
            new SqlParameter("@Amount", basket.Amount),
            new SqlParameter("@ProductId", basket.ProductId)
        };

                dbContext.Database.ExecuteSqlRaw(sql, parameters);
            }
            catch (Exception ex) { return false; }


            return true;

        }


        public bool GenerateOrder(int userId)
        {
            var connectionString = dbContext.Database.GetConnectionString();
                List<Tuple<int, double>> coilosc = new List<Tuple<int, double>>();
            using (SqlConnection local = new SqlConnection(connectionString))
            {
                try
                {
                    var sql = @"
            SELECT ProductId, Amount
            FROM  BasketPosition
            WHERE UserId = @UserId";

                    SqlCommand command = new SqlCommand(sql, local);
                    command.Parameters.AddWithValue("@UserId", userId);

                    local.Open(); 

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            coilosc.Add(new Tuple<int, double>(
                                reader.GetInt32(reader.GetOrdinal("ProductId")),
                                reader.GetInt32(reader.GetOrdinal("Amount"))
                            ));
                        }
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
          
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
              
                List<ProductResponse> products = new List<ProductResponse>();
                try
                {
                    var sql = @"SELECT Product.Id, Product.Name,ProductGroup.Name AS pgName , Product.Price, Product.Image, Product.IsActive, Product.GroupID from Product Left Join ProductGroup On Product.GroupId = ProductGroup.Id  ";
                    SqlCommand command = new SqlCommand(sql, connection);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new ProductResponse()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Price = reader.GetDouble(reader.GetOrdinal("Price")),
                                GroupID = reader.IsDBNull(reader.GetOrdinal("GroupId")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("GroupId")),
                                GroupName = reader.GetString(reader.GetOrdinal("pgName"))
                            });
                        }
                    }
                }
                catch (Exception ex) { return false; }
                int idDodanego;
                try
                {
                    var sql = @"INSERT INTO [Order] (UserId, Date) 
    OUTPUT INSERTED.Id
    VALUES (@UserId, @Date)";

                    var parameters = new[]
                    {
                    new SqlParameter("@UserId",userId),
                    new SqlParameter("@Date", DateTime.Now)
                };

                    idDodanego =  dbContext.Database.ExecuteSqlRaw(sql, parameters);
                }
                catch (Exception ex) { return false; }
                try
                {
                    var sql = @"INSERT INTO OrderPosition (OrderId, Amount, Price) 
                       VALUES (@OrderId, @Amount, @Price)";
                    var parameters = new[]
                    {
                    new SqlParameter("@OrderId",idDodanego),
                    new SqlParameter("@Amount",  coilosc
                        .Where(c => products.Any(p => p.Id == c.Item1))
                        .Sum(c => c.Item2)),
                    new SqlParameter("@Price",products
                    .Sum(p => p.Price) )
                };

                    dbContext.Database.ExecuteSqlRaw(sql, parameters);
                }
                catch (Exception ex) { return false; }
            }
            return true;
        }

        public bool Payment(int userId, int payment)
        {
            throw new NotImplementedException();
        }

        public bool RemoveProductBasket(int productId, int userId)
        {
            try
            {
                var sql = @"
            DELETE FROM BasketPosition
            WHERE UserId = @UserId
            AND ProductId = @ProductId";

                var parameters = new[]
                {
            new SqlParameter("@UserId", userId),

            new SqlParameter("@ProductId",productId)
        };

                dbContext.Database.ExecuteSqlRaw(sql, parameters);
            }
            catch (Exception ex) { return false; }
            return true;

        }
    }
}
