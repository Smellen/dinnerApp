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

            var owners = await GetOwners(string.Empty);
            
            var dict = new Dictionary<string, Owner>();

            foreach(var owner in owners)
            {
                dict.Add(owner.Id, owner);
            }

            ViewBag.OwnerList = dict;

            return View(dinnerModel);
        }

        public async Task<ActionResult> AddNewDinner()
        {
            var model = new AddDinnerModel()
            {
                Owners = new List<Owner>(),
                Dinner = new Dinner()
            };

            var owners = await GetOwners(string.Empty);
            ViewBag.OwnerList = ToSelectList(owners, string.Empty, string.Empty);

            return View("AddDinner", model);
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
                    // Failed to add dinner.
                    return View("AddDinner", new Dinner());
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(DateTime date)
        {
            var dinner = (await _repository.Search(date)).FirstOrDefault();
            var owner = await _repository.GetOwners(dinner.Owner);
            ViewBag.OwnerName = owner.FirstOrDefault().Name;

            return View("Details", _mapper.Map<Dinner>(dinner));
        }

        public async Task<ActionResult> Delete(DateTime date)
        {
            await _repository.DeleteDinner(date);

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

        private async Task<List<Owner>> GetOwners(string id)
        {
            var ownerResult = new List<Owner>();
            var ownersFromDatabase = await _repository.GetOwners(id);
            ownersFromDatabase.ForEach(e => ownerResult.Add(_mapper.Map<Owner>(e)));

            return ownerResult;
        }
    }
}
