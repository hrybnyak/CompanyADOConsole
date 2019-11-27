using BLL.DTO;
using BLL.Mappers;
using System;
using DAL.TableDataGateway;
using DAL.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Linq;

namespace BLL
{
    public class TaskService
    {
        private SqlConnection _conn;

        private ProductMapper _productMapper = new ProductMapper();
        private ProviderMapper _providerMapper = new ProviderMapper();
        private CategoryMapper _categoryMapper = new CategoryMapper();

        public TaskService(SqlConnection connection)
        {
            _conn = connection;
        }

        public IEnumerable<ProductDTO> GetProductsByCategory(CategoryDTO category)
        {
            if (category == null)
            {
                return null;
            }
            var categoryTableData = new CategoryTableDataGateway(_conn);
            int? categoryId;
            if (categoryTableData.GetById(category.Id) == null)
            {
                if (categoryTableData.GetAll().Where(c => c.Name == category.Name).FirstOrDefault() == null)
                {
                    return null;
                }
                else
                {
                    categoryId = categoryTableData.GetAll().Where(c => c.Name == category.Name).FirstOrDefault().Id;
                }
            }
            else
            {
                categoryId = category.Id;
            }
            var productCollection = (new ProductTableDataGateway(_conn)).GetAll().Where(p => p.CategoryId == categoryId);
            return _productMapper.Map(productCollection);
        }

        public IEnumerable<ProviderDTO> GetProvidersByCategory(CategoryDTO category)
        {
            if (GetProductsByCategory(category) == null)
            {
                return null;
            }
            else
            {
                var products = GetProductsByCategory(category);
                List<Provider> providers = new List<Provider>();
                foreach (ProductDTO product in products)
                {
                    var provider = (new ProviderTableDataGateway(_conn)).GetById(product.ProviderId);
                    if (provider != null && !providers.Contains(provider))
                    {
                        providers.Add(provider);
                    }
                }
                return _providerMapper.Map(providers);
            }
        }

        public IEnumerable<ProductDTO> GetProductsByProvider(ProviderDTO provider)
        {
            if (provider == null)
            {
                return null;
            }
            int providerId;
            if ((new ProductTableDataGateway(_conn)).GetById(provider.Id) == null)
            {
                if ((new ProviderTableDataGateway(_conn)).GetAll().Where(p => p.Name == provider.Name) == null)
                {
                    return null;
                }
                else
                {
                    providerId = (new ProviderTableDataGateway(_conn)).GetAll().Where(p => p.Name == provider.Name).FirstOrDefault().Id;
                }
            }
            else
            {
                providerId = provider.Id ?? throw new ArgumentNullException(nameof(provider.Id));
            }
            var productCollection = (new ProductTableDataGateway(_conn)).GetAll().Where(p => p.ProviderId == providerId);
            return _productMapper.Map(productCollection);
        }

        public IEnumerable<ProviderDTO> GetProvidersWithMaximumNumberOfCategories()
        {
            var providers = (new ProviderTableDataGateway(_conn)).GetAll().ToList();
            var result = new List<ProviderDTO>();
            var numberPerProvider = new List<int>();
            int max = 0;
            foreach (Provider provider in providers)
            {
                var products = GetProductsByProvider(_providerMapper.Map(provider));
                List<int?> categories = new List<int?>();
                foreach(ProductDTO product in products)
                {
                    categories.Add(product.CategoryId);
                }
                categories = categories.Distinct().ToList();
                numberPerProvider.Add(categories.Count);
                if (categories.Count > max)
                {
                    max = categories.Count;
                }
            }
            for (int i = 0; i<numberPerProvider.Count; i++)
            {
                if (numberPerProvider[i] == max)
                {
                    result.Add(_providerMapper.Map(providers[i]));
                }
            }
            return result;
        }
    }
}
