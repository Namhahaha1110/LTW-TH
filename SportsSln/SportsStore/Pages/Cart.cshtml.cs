using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore.Models;

namespace SportsStore.Pages
{
    public class CartModel : PageModel
    {
        private readonly IStoreRepository repository;
        private readonly Cart cart;

        public CartModel(IStoreRepository repo, Cart cartService)
        {
            repository = repo;
            cart = cartService;
            Cart = cartService;
        }

        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; } = "/";

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            Cart = cart;
        }

        public IActionResult OnPost(int productId, string returnUrl)
        {
            Product? product = repository.Products
                .FirstOrDefault(p => p.ProductId == productId);

            if (product != null)
            {
                Cart.AddItem(product, 1);
            }

            return RedirectToPage(new { returnUrl });
        }

        public IActionResult OnPostRemove(int productId, string returnUrl)
        {
            Product? product = repository.Products
                .FirstOrDefault(p => p.ProductId == productId);

            if (product != null)
            {
                Cart.RemoveLine(product);
            }

            return RedirectToPage(new { returnUrl });
        }
    }
}
