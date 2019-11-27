using System;
using System.Configuration;
using System.Data.SqlClient;
using BLL;
using BLL.DTO;

namespace EPAM.RD5_Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int key = 1;
                string connString = ConfigurationManager.ConnectionStrings["CompanyDatabase"].ConnectionString;
                SqlConnection connection = new SqlConnection(connString);
                using (connection)
                {
                    connection.Open();
                    TaskService service = new TaskService(connection);
                    do
                    {
                        Console.WriteLine("--------------Menu----------------");
                        Console.WriteLine("Press 0 to exit");
                        Console.WriteLine("Press 1 to get all products by category name");
                        Console.WriteLine("Press 2 to get all products by provider name");
                        Console.WriteLine("Press 3 to get all providers by category name");
                        Console.WriteLine("Press 4 to get all providers with max number of categories of products they provide");
                        if (Int32.TryParse(Console.ReadLine(), out key))
                        {
                            switch (key)
                            {
                                case 1:
                                    {
                                        Console.WriteLine("Please enter category name:");
                                        CategoryDTO category = new CategoryDTO
                                        {
                                            Name = Console.ReadLine()
                                        };
                                        var products = service.GetProductsByCategory(category);
                                        if (products != null)
                                        {
                                            foreach (ProductDTO product in products)
                                            {
                                                Console.WriteLine($"{product.Name}       {product.Price}       {product.CategoryId}       {product.ProviderId}");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("There is no products under this category");
                                        }
                                        break;
                                    }
                                case 2:
                                    {
                                        Console.WriteLine("Please enter provider name:");
                                        ProviderDTO provider = new ProviderDTO
                                        {
                                            Name = Console.ReadLine()
                                        };
                                        var products = service.GetProductsByProvider(provider);
                                        if (products != null)
                                        {
                                            foreach (ProductDTO product in products)
                                            {
                                                Console.WriteLine($"{product.Name}       {product.Price}       {product.CategoryId}       {product.ProviderId}");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("There is no products from this provider");
                                        }
                                        break;
                                    }
                                case 3:
                                    {
                                        Console.WriteLine("Please enter category name:");
                                        CategoryDTO category = new CategoryDTO
                                        {
                                            Name = Console.ReadLine()
                                        };
                                        var providers = service.GetProvidersByCategory(category);
                                        if (providers != null)
                                        {
                                            foreach (ProviderDTO provider in providers)
                                            {
                                                Console.WriteLine($"{provider.Name}       {provider.PhoneNumber}");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("There is no providers of products of this category");
                                        }
                                        break;
                                    }
                                case 4:
                                    {
                                        var providers = service.GetProvidersWithMaximumNumberOfCategories();
                                        if (providers != null)
                                        {
                                            foreach (ProviderDTO provider in providers)
                                            {
                                                Console.WriteLine($"{provider.Name}       {provider.PhoneNumber}");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("There is no providers");
                                        }
                                        break;

                                    }
                                default:
                                    {
                                        Console.WriteLine("Please, try again!");
                                        break;
                                    }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Try again!");
                        }

                    } while (key != 0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
