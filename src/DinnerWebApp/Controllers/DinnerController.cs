using System;
using System.Collections.Generic;
using DinnerWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DinnerWebApp.Controllers
{
    public class DinnerController : Controller
    {
        public List<Dinner> Dinners { get; set; }

        public DinnerController()
        {
            Dinners = new List<Dinner>()
            {
                new Dinner()
                {
                    BonusPoints = 9.0,
                    BaseScore = 9.0,
                    Date = DateTime.Today,
                    Description = "Test dinner",
                    Owner = new Owner()
                    {
                        Name = "ellen",
                        Total = 9.0
                    }
                }
            };
        }

        // GET: DinnerController
        public ActionResult Dinner()
        {
            return View(Dinners);
        }

        // GET: DinnerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DinnerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DinnerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DinnerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DinnerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DinnerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DinnerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
