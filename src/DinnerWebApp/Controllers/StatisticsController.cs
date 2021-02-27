using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DinnerWebApp.Data;
using DinnerWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DinnerWebApp.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDataRepository _repository;
        public StatisticsController(IMapper mapper, IDataRepository repository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<ActionResult> Statistics()
        {
            var ownerAverage = new Dictionary<Owner, double>();
            var bestRated = await _repository.BestRated();
            var owners = await _repository.GetOwners(string.Empty);
            foreach (var owner in owners)
            {
                ownerAverage.TryAdd(_mapper.Map<Owner>(owner), await _repository.AveragePerOwner(owner.Id));
            }

            var model = new StatisticsModel()
            {
                AverageDinnerScore = await _repository.AverageDinnerScore(),
                BestRatedDinner = _mapper.Map<Dinner>(bestRated),
                TotalAmountOfDinnersTracked = await _repository.DinnerCount(),
                AveragePerOwner = ownerAverage
            };

            ViewBag.OwnerName = owners.FirstOrDefault(e => e.Id == bestRated.Owner).Name;

            return View("Statistics", model);
        }
    }
}
