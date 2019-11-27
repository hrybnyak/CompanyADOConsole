using BLL.DTO;
using DAL.Models;
using DAL.TableDataGateway;
using System.Linq;
using System;
namespace BLL.Mappers
{
    public class ProductMapper : MapperBase<Product, ProductDTO>
    {
        public override Product Map(ProductDTO element)
        {
            return new Product
            {
                Name = element.Name,
                Price = element.Price,
                CategoryId = element.CategoryId,
                ProviderId = element.ProviderId
            };
        }

        public override ProductDTO Map(Product element)
        {
            return new ProductDTO
            {
                Name = element.Name,
                Price = element.Price,
                CategoryId = element.CategoryId,
                ProviderId = element.ProviderId
            };
        }
    }
}
