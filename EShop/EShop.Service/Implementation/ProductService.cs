using EShop.Domain.DomainModels;
using EShop.Domain.DTO;
using EShop.Repository.Interface;
using EShop.Service.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> productRepository;
        private readonly IRepository<ProductInShoppingCart> productInShoppingCartRepository;
        private readonly IUserRepository userRepository;
        private readonly ILogger<ProductService> _logger;
                
        public ProductService(IRepository<Product> productRepository, IUserRepository userRepository, IRepository<ProductInShoppingCart> productInShoppingCartRepository, ILogger<ProductService> logger)
        {
            this.productRepository = productRepository;
            this.userRepository = userRepository;
            this.productInShoppingCartRepository = productInShoppingCartRepository;
            _logger = logger;
        }
        public bool AddToShoppingCart(AddToShoppingCartDto item, string userId)
        {
            var user = this.userRepository.Get(userId);
            var userShoppingCart = user.UserCart;
            if (item.ProductId != null && userShoppingCart != null)
            {
                var product = this.GetDetailsForProduct(item.ProductId);

                if (product != null)
                {
                    ProductInShoppingCart itemToAdd = new ProductInShoppingCart
                    {
                        Id = Guid.NewGuid(),
                        Product = product,
                        ProductId = product.Id,
                        ShoppingCart = userShoppingCart,
                        ShoppingCartId = userShoppingCart.Id,
                        Quantity = item.Quantity
                    };
                    this.productInShoppingCartRepository.Insert(itemToAdd);
                    return true;
                }
                return false;
            }
            return false;
        }


        public void CreateNewProduct(Product product)
        {
            this.productRepository.Insert(product);
        }

        public void DeleteProduct(Guid id)
        {
            var product = this.GetDetailsForProduct(id);
            this.productRepository.Delete(product);
        }

        public Product GetDetailsForProduct(Guid? id)
        {
            return this.productRepository.Get(id);
        }

        public List<Product> GetProducts()
        {
            return this.productRepository.GetAll().ToList();
        }

        public AddToShoppingCartDto GetShoppingCartInfo(Guid? id)
        {
            var product = this.GetDetailsForProduct(id);
            AddToShoppingCartDto model = new AddToShoppingCartDto
            {
                SelectedProduct = product,
                ProductId = product.Id,
                Quantity = 1
            };
            return model;
        }

        public void UpdateExistingProduct(Product product)
        {
            this.productRepository.Update(product);
        }
    }
}
