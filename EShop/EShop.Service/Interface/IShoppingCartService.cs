using EShop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDto getShoppingCartInfo(string userId);
        bool DeleteProductFromShoppingCart(string userId, Guid productId);
        bool OrderNow(string userId);

    }
}
