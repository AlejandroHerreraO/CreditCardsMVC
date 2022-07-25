using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TarjetasMVC.Models;

namespace TarjetasMVC.Controllers {
    public class HomeController : Controller {

        static Data db = new Data() {
            customerList = new List<Customer> {
                    new Customer() { CustomerID = 0, CustomerName = "Jon" },
                    new Customer() { CustomerID = 1, CustomerName = "Smith" }
                },
            cardList = new List<Card> {
                    new Card() { CardNumber = "1900", CardType = Card.CardTypes.Visa, CardBalance = 12500, CardLimit = 25000, CustomerID = 0 },
                    new Card() { CardNumber = "4047", CardType = Card.CardTypes.MasterCard, CardBalance = 5000, CardLimit = 15000, CustomerID = 0 },
                    new Card() { CardNumber = "0721", CardType = Card.CardTypes.Visa, CardBalance = 0, CardLimit = 25000, CustomerID = 1 },
                    new Card() { CardNumber = "1000", CardType = Card.CardTypes.MasterCard, CardBalance = 0, CardLimit = 12000, CustomerID = 1 },
                    new Card() { CardNumber = "6563", CardType = Card.CardTypes.AmericanExpress, CardBalance = 29000, CardLimit = 35000, CustomerID = 1 }
                }
        };
        public ActionResult Index() {

            return View(db);
        }
        public ViewResult Charge(Data data) {

            ViewBag.Message = data.Amount;

            return View();

        }

        [HttpGet]
        public JsonResult UpdateValues(string cardNumber, int amountAdded) {
            List<string> cardInfo = new List<string>();

            foreach(Card card in db.cardList) {
                if (card.CardNumber.Equals(cardNumber)) {
                    card.CardBalance += amountAdded;

                    cardInfo.Add(card.CardNumber);
                    cardInfo.Add(card.CardType.ToString());
                    cardInfo.Add(card.CardBalance.ToString());
                    if (card.CardType == Card.CardTypes.AmericanExpress)
                        cardInfo.Add((card.CardLimit * 0.9).ToString());
                    else
                        cardInfo.Add(card.CardLimit.ToString());
                    cardInfo.Add("valid");
                    return Json(new { data = cardInfo }, JsonRequestBehavior.AllowGet);

                }
            }
            return Json(new { data = "" }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetDropdownList(int? value) {
            List<string> cards = new List<string>();
            cards.Clear();
            cards.Add("");
            foreach (Card card in db.cardList) {
                if (card.CustomerID == value)
                    cards.Add(card.CardNumber + " - " + card.CardType);
            }
            return Json(new { data = cards }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetCardInfo(string value) {
            List<string> cardInfo = new List<string>();

            if (value.Equals("")) {
                return Json(new { data = "" }, JsonRequestBehavior.AllowGet);
            }

            string number = value.Substring(0, 4);
            foreach (Card card in db.cardList) {
                if (card.CardNumber.Equals(number)) {
                    cardInfo.Add(card.CardNumber);
                    cardInfo.Add(card.CardType.ToString());
                    cardInfo.Add(card.CardBalance.ToString());
                    if (card.CardType == Card.CardTypes.AmericanExpress)
                        cardInfo.Add((card.CardLimit * 0.9).ToString());
                    else
                        cardInfo.Add(card.CardLimit.ToString());
                    if (card.Valid())
                        cardInfo.Add("valid");
                    else
                        cardInfo.Add("");
                    return Json(new { data = cardInfo }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { data = "" }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}