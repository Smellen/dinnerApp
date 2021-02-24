using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DinnerWebApp.Data;
using DinnerWebApp.Data.Models;
using DinnerWebApp.Models;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<ActionResult> Dinner()
        {
            var dinners = new List<Dinner>();
            var dinnersFromDatabase = await _repository.GetAllDinners();
            if (dinnersFromDatabase != null && dinnersFromDatabase.Any())
            {
                dinnersFromDatabase.ForEach(e => dinners.Add(_mapper.Map<Dinner>(e)));
            }

            return View(dinners);
        }

        public ActionResult AddNewDinner()
        {
            return View("CreateDinner", new Dinner());
        }

        public async Task<ActionResult> Create(Dinner dinner)
        {
            var mappedDinners = new List<Dinner>();
            if (dinner != null)
            {
                var dinners = await _repository.GetAllDinners();
                var matchingDate = dinners.FirstOrDefault(e => e.Date == dinner.Date);
                if (matchingDate == null)
                {
                   await _repository.Add(_mapper.Map<DinnerDao>(dinner));
                }
                
                dinners.ForEach(e => mappedDinners.Add(_mapper.Map<Dinner>(e)));
            }

            return View("Dinner", mappedDinners);
        }
    }
}
