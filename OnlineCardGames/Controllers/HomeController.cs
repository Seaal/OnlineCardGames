using OnlineCardGames.Entities;
using OnlineCardGames.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OnlineCardGames.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            GameLogicService logicService = new GameLogicService();

            IList<Card> cards = logicService.GetNewDeck();
            ViewBag.Cards = cards;
            return View();
        }
    }
}
