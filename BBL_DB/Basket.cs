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
                var sql = @"
                IF EXISTS (SELECT 1 FROM BasketPositions WHERE Id = @UserId AND ProductId = @ProductId)
                BEGIN
                    INSERT INTO BasketPositions (Id, ProductId, Amount)
                    VALUES (@UserId, @ProductId, @Amount)
                END";

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
            UPDATE BasketPositions
            SET Amount = @Amount
            WHERE Id = @UserId";

                var parameters = new[]
                {
            new SqlParameter("@UserId", basket.UserId),
            new SqlParameter("@Amount", basket.Amount)
        };

                dbContext.Database.ExecuteSqlRaw(sql, parameters);
            }
            catch (Exception ex) { return false; }
            return true;
        }

        public bool GenerateOrder(int userId)
        {
            throw new NotImplementedException();
        }

        public bool Payment(int userId, int payment)
        {
            throw new NotImplementedException();
        }

        public bool RemoveProductBasket(int productId, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
