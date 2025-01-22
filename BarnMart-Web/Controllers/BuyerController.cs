using AutoMapper;
using BarnMart_Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using The_Barn_Mart;

namespace BarnMart_Web.Controllers
{
   
    public class BuyerController : Controller
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        public BuyerController(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            List<BuyerVM> list = mapper.Map<List<BuyerVM>>(context.BuyersINV.ToList());
            return View(list);
        }

        public IActionResult Add()
        {
            return View(new BuyerVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(BuyerVM model)
        {
            Buyer entity = mapper.Map<Buyer>(model);
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            var buyer = await context.BuyersINV.FindAsync(Id);
            context.Set<Buyer>().Remove(buyer);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null) return RedirectToAction("Index");
            BuyerVM buyer = mapper.Map<BuyerVM>(await context.BuyersINV.FindAsync(Id));
            return View(buyer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BuyerVM model)
        {
            context.Set<Buyer>().Update(mapper.Map<Buyer>(model));
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
