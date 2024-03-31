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
using static System.Net.Mime.MediaTypeNames;

namespace BBL_DB
{
    public class Project : IProduct
    {
        private readonly WebshopContext dbContext;
        public Project(WebshopContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool AddNewProduct(string name, double price, int groupId)
        {
            try
            {
                var sql = "INSERT INTO Products (Name, Price, Image, IsActive, GroupId) " +
                        "VALUES (@Name, @Price, @Image, @IsActive, @GroupId);";

                var parameters = new[]
                {
            new SqlParameter("@Name", name),
                new SqlParameter("@Price", price),
            new SqlParameter("@Image",""),
            new SqlParameter("@IsActive", true),
            new SqlParameter("@GroupId", groupId)
        };
                dbContext.Database.ExecuteSqlRaw(sql, parameters);
            }
            catch (Exception ex) { return false; }
            return true;
        }

        public bool ChangeActivityOfProduct(int productId, bool state)
        {
            try
            {
                var sql = @"
                UPDATE Product
                SET IsActive = @IsActive
                WHERE Id = @productId";

                var parameters = new[]
                {
            new SqlParameter("@productId", productId),
            new SqlParameter("@IsActive", state)
        };

                dbContext.Database.ExecuteSqlRaw(sql, parameters);
            }
            catch (Exception ex) { return false; }
            return true;
        }

        public IEnumerable<ProductResponse> GetProducts(bool desc, string filtrByName, string filtrByGroup, int filtrByGroupId, IProduct.FiltrBy filtrBy, bool showNonActive)
        {
            throw new NotImplementedException();
        }

        public bool RemoveProduct(int productId)
        {
            try
            {
                var sql = @"
                DELETE 
                FROM Product
                WHERE Id= @productId";
                var parameters = new[]
                {
                    new SqlParameter("@productId", productId),
                };
                dbContext.Database.ExecuteSqlRaw(sql, parameters);
            }
            catch (Exception ex) { return false; }
            return true;

        }
    }
}
