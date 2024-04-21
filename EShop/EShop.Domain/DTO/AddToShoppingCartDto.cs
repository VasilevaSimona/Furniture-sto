using EShop.Domain.DomainModels;
using System;

namespace EShop.Domain.DTO
{
    public class AddToShoppingCartDto
    {
        public Product SelectedProduct { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

    }
}
