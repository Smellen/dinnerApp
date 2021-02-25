using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DinnerWebApp.Data;
using DinnerWebApp.Data.Models;
using DinnerWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DinnerWebApp.Controllers
{
    public class DinnerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDataRepository _repository;

        public DinnerController(IMapper mapper, IDataRepository repository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<ActionResult> Index(int skip = 0, int take = 5)
        {
            var dinners = new List<Dinner>();
            var dinnersFromDatabase = await _repository.GetDinners(skip, take);
            if (dinnersFromDatabase != null && dinnersFromDatabase.Any())
            {
                dinnersFromDatabase.ForEach(e => dinners.Add(_mapper.Map<Dinner>(e)));
            }

            var dinnerModel = new DinnerModel()
            {
                Dinners = dinners,
                Take = take,
                Skip = skip
            };

            return View(dinnerModel);
        }

        public async Task<ActionResult> AddNewDinner()
        {
            var owners = await _repository.GetOwners();
            var model = new AddDinnerModel()
            {
                Owners = new List<Owner>(),
                Dinner = new Dinner()
            };

            owners.ForEach(e => model.Owners.Add(_mapper.Map<Owner>(e)));
            ViewBag.OwnerList = ToSelectList(model.Owners, "OwnerId", "OwnerName");

            return View("CreateDinner", model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Dinner dinner)
        {
            if (dinner != null)
            {
                var dinners = await _repository.Search(dinner.Date);
                if (dinners == null)
                {
                    await _repository.Add(_mapper.Map<DinnerDao>(dinner));
                }
                else
                {
                    // Go back to the edit page
                    return View("CreateDinner", new Dinner());
                }
            }

            return RedirectToAction("Index");
        }

        [NonAction]
        public SelectList ToSelectList(List<Owner> owners, string valueField, string textField)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var owner in owners)
            {
                list.Add(new SelectListItem()
                {
                    Text = owner.Name,
                    Value = owner.Id
                });
            }

            return new SelectList(list, "Value", "Text");
        }
    }
}
