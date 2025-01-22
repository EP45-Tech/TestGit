using AutoMapper;
using BarnMart_Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using The_Barn_Mart;

namespace BarnMart_Web.Controllers
{
    
    public class SellerController : Controller
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        public SellerController(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            List<SellerVM> list = mapper.Map<List<SellerVM>>(context.SellersINV.ToList());
            return View(list);
        }

        public IActionResult Add()
        {
            return View(new SellerVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(SellerVM model)
        {
            await context.AddAsync(mapper.Map<Seller>(model));
            //context.Set<Seller>().Add(model);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            var seller = await context.SellersINV.FindAsync(Id);
            context.Set<Seller>().Remove(seller);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null) return RedirectToAction("Index");
            SellerVM seller = mapper.Map<SellerVM>(await context.SellersINV.FindAsync(Id));

            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SellerVM model)
        {
            context.Set<Seller>().Update(mapper.Map<Seller>(model));
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


    }
}
