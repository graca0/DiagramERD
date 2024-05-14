using BBL.DTOModels;
using BBL.ServiceInterfaces;
using DAL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;

namespace BBL_DB
{
    public class Product : IProduct
    {
        private readonly WebshopContext dbContext;
        public Product(WebshopContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool AddNewProduct(string name, double price, int groupId)
        {
            try
            {
                var sql = @"INSERT INTO Product (Name, Price, Image, IsActive, GroupId)
                        VALUES (@Name, @Price, @Image, @IsActive, @GroupId);";

                var parameters = new[]
                {
            new SqlParameter("@Name", name),
                new SqlParameter("@Price", price),
            new SqlParameter("@Image",""),
            new SqlParameter("@IsActive", false),
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

        public IEnumerable<ProductResponse> GetProducts(bool? desc, string filtrByName, string filtrByGroup, int? filtrByGroupId, IProduct.FiltrBy? filtrBy, bool? showNonActive)
        {
            var connectionString = dbContext.Database.GetConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    var sql = @"SELECT Product.Id, Product.Name,ProductGroup.Name AS pgName , Product.Price, Product.Image, Product.IsActive, Product.GroupID from Product 
                                Left Join ProductGroup On Product.GroupId = ProductGroup.Id Where 1=1";

                    if (showNonActive != null && !showNonActive.Value)
                        sql += "and Product.IsActive = 1";

                    if (filtrByName != null && filtrByName != "")
                        sql += $"and Product.Name like '%{filtrByName}%'";
                    if (filtrByGroup != null && filtrByGroup != "")
                        sql += $"and ProductGroup.Name like '%{filtrByGroup}%'";
                    if (filtrByGroupId != null && filtrByGroupId != 0)
                    {
                        sql += $"and Product.GroupID like '%{filtrByGroupId}%'";

                    }
                        bool order = true;
                    if (desc == null || !desc.Value)
                    {
                        order = false;
                    }
                    switch (filtrBy)
                    {
                        case IProduct.FiltrBy.Name:
                            sql += " ORDER BY Name " + (order ? "DESC" : "ASC");
                            break;
                        case IProduct.FiltrBy.GroupName:
                            sql += " ORDER BY (SELECT Name FROM ProductGroup WHERE Id = Product.GroupID) " + (order ? "DESC" : "ASC");
                            break;
                        case IProduct.FiltrBy.GroupId:
                            sql += " ORDER BY GroupID " + (order ? "DESC" : "ASC");
                            break;
                    }


                    SqlCommand command = new SqlCommand(sql, connection);
                    List<ProductResponse> products = new List<ProductResponse>();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new ProductResponse()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name =  reader.GetString(reader.GetOrdinal("Name")),
                                Price = reader.GetDouble(reader.GetOrdinal("Price")),
                                GroupID = reader.IsDBNull(reader.GetOrdinal("GroupId")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("GroupId")),
                                GroupName = reader.GetString(reader.GetOrdinal("pgName"))
                            });
                        }
                    }
                    return products;
                }
                catch (Exception ex) { }
            }
            return null;

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
