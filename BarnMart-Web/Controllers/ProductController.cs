using AutoMapper;
using BarnMart_Web.Models;
using BarnMart_Web.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using The_Barn_Mart;

namespace BarnMart_Web.Controllers
{
   
    public class ProductController : Controller
    {
        private readonly IProductRepo repo;
        private readonly IMapper mapper;
        public ProductController(IProductRepo repo, IMapper mapper)
        {
         this.repo = repo;
         this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(mapper.Map<List<ProductVM>>(await repo.GetAllSync()));
        }

        public IActionResult Add()
        {
            return View(new ProductVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ProductVM model)
        {
            model.DateAdded = DateTime.Now;
            Product entity = mapper.Map<Product>(model);

            if (ModelState.IsValid)
            {
                await repo.AddAsync(entity);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            var product = await repo.GetAsync(Id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }

            await repo.DeleteAsync(Id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null) RedirectToAction("Index");
            ProductVM product = mapper.Map<ProductVM>(await repo.GetAsync((int)Id));
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductVM model)
        {
            if (ModelState.IsValid)
            {
                model.DateUpdated = DateTime.Now;
                await repo.EditAsync(mapper.Map<Product>(model));
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }
    }
}
