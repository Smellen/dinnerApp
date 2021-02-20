using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DinnerWebApp.Controllers
{
    public class LeaderboardController : Controller
    {
        // GET: LeaderboardController
        public ActionResult Leaderboard()
        {
            return View();
        }

        // GET: LeaderboardController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LeaderboardController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaderboardController/Create
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

        // GET: LeaderboardController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LeaderboardController/Edit/5
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

        // GET: LeaderboardController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaderboardController/Delete/5
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
