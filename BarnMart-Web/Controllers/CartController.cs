using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using The_Barn_Mart;

namespace BarnMart_Web.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CheckoutResult()
        {
            // Your checkout logic here...
            //RedirectToAction("checkout", "Home");
            //return View(); 
            try
            {
                // Process the checkout (e.g., save order details, update inventory, etc.)
                TempData["CheckoutMessage"] = "success";
            }
            catch (Exception ex)
            {
                TempData["CheckoutMessage"] = "error";
                // Log the exception or handle it as needed
            }

            return RedirectToAction("CheckoutConfirmation");
        }

        public IActionResult CheckoutConfirmation()
        {
            return View();
        }

        public IActionResult Index()
        {
            var cart = GetOrCreateCart(); // Assuming this gets the cart
            foreach (var item in cart.Items)
            {
                if (item.Schedule != null)
                {
                    Console.WriteLine(item.Schedule.vehicle); // Debugging output
                }
            }

            return View(cart);
        }



        [HttpPost]
        public IActionResult AddProductToCart(int productId)
        {
            var cart = GetOrCreateCart();
            var product = _context.ProductsINV.Find(productId);

            if (product == null)
            {
                return NotFound();
            }

            var cartItem = new CartItem
            {
                ProductId = productId,
                Product = product,
                Quantity = 1,
                TotalPrice = product.Price
            };
            cart.Items.Add(cartItem);

            _context.SaveChanges();

            return RedirectToAction("SelectSchedule", new { cartItemId = cartItem.Id });
        }

        [HttpGet]
        public IActionResult SelectSchedule(int cartItemId)
        {   
            var cartItem = _context.CartItems.Include(ci => ci.Product).FirstOrDefault(ci => ci.Id == cartItemId);
            if (cartItem == null)
            {
                return NotFound();
            }

            ViewBag.Schedules = _context.Schedules.ToList();  

            return View(cartItem);
        }

        [HttpPost]
        public IActionResult SelectSchedule(int cartItemId, int scheduleId)
        {
            var cartItem = _context.CartItems.Include(ci => ci.Product).FirstOrDefault(ci => ci.Id == cartItemId);
            if (cartItem == null)
            {
                return NotFound();
            }

            var schedule = _context.Schedules.Find(scheduleId);
            if (schedule == null)
            {
                return NotFound("Schedule not found.");
            }

            cartItem.ScheduleId = scheduleId;
            cartItem.Schedule = schedule;

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log the exception and return an error message
                Console.WriteLine($"An error occurred while saving the cart item: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

            return RedirectToAction("Index");
        }
        private Cart GetOrCreateCart()
        {
            try
            {
                var cart = _context.Carts.Include(c => c.Items)
                                         .ThenInclude(i => i.Product)
                                         .FirstOrDefault();

                if (cart == null)
                {
                    cart = new Cart();
                    _context.Carts.Add(cart);
                    _context.SaveChanges();
                }

                return cart;
            }
            catch (Exception ex)
            {
                // Log the exception and handle it appropriately
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
