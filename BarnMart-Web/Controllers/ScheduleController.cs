using AutoMapper;
using BarnMart_Web.Models.Repositories;
using BarnMart_Web.Models;
using Microsoft.AspNetCore.Mvc;
using The_Barn_Mart;
using Microsoft.EntityFrameworkCore;

namespace BarnMart_Web.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly AppDbContext context;
        private readonly IScheduleRepo repo;
        private readonly IMapper mapper;

        public ScheduleController(AppDbContext context, IScheduleRepo repo, IMapper mapper)
        {
            this.context = context;
            this.repo = repo;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            return View(mapper.Map<List<ScheduleVM>>(await repo.GetAllSync()));
        }

        public IActionResult Add()  
        {
            return View(new ScheduleVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ScheduleVM model)
        {
            if (ModelState.IsValid)
            {
                model.DateAdded = DateTime.Now;
                await repo.AddAsync(mapper.Map<Schedule>(model));
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            var schedule = await repo.GetAsync(Id);
            if (schedule == null)
            {
                return RedirectToAction("Index");
            }

            await repo.DeleteAsync(Id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null) RedirectToAction("Index");
            ScheduleVM schedule = mapper.Map<ScheduleVM>(await repo.GetAsync((int)Id));
            return View(schedule);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ScheduleVM model)
        {
            if (ModelState.IsValid)
            {
                model.DateAdded = DateTime.Now;
                await repo.EditAsync(mapper.Map<Schedule>(model));
                return RedirectToAction("Index");

            }
            else
            {
                return View(model);
            }
        }
        public IActionResult YourAction()
        {
            // Populate ViewBag.Schedules with a non-null list
            ViewBag.Schedules = context.Schedules.ToList(); // or another source

            var model = new List<ScheduleVM>(); // Populate your model here

            return View(model);
        }
    }
}
