using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RocketShip.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace RocketShip.Controllers
{
    public class HomeController : Controller
    {
    private RSContext db;
        public HomeController(RSContext context)
        {
            db = context;
        }
        
        public IActionResult Index()
        {
            return RedirectToAction("Info");
        }

        //==========How to Play==========
    
        [HttpGet("HowToPlay")]
        public IActionResult Info()
        {
            return View();
        }

        //==========New Game===========

        [HttpPost("NewGame")]
        public IActionResult Play()
        {
            HttpContext.Session.Clear();
            List<Player> allPlayers = db.Players.OrderBy(p => p.PlayerId).ToList();
            List<Deck> allDecks = db.Decks.Include(d => d.cards).OrderBy(d => d.DeckId).ToList();

            if(allDecks != null)
            {
                foreach (var deck in allDecks)
                {
                    db.Decks.Remove(deck);
                    db.SaveChanges();
                }
            }
            
            if(allPlayers != null)
            {
                foreach (var player in allPlayers)
                {
                    db.Players.Remove(player);
                    db.SaveChanges();
                }
            }
            List<Card> cardList =  new List<Card>();
            foreach (var suit in new [] { "♠", "♡", "♣", "♢", })
            {
                for (var rank = 1; rank <= 13; rank++)
                {
                    string strVal;
                    if(rank == 1)
                    {
                        strVal = "ACE";
                    }
                    else if(rank == 11)
                    {
                        strVal = "J ♞";
                    }
                    else if(rank == 12)
                    {
                        strVal = "Q ♛";
                    }
                    else if(rank == 13)
                    { 
                        strVal = "K ♚";
                    }
                    else
                    {
                        strVal = rank.ToString() + " " + "♟";
                    }
                    Card newCard = new Card(strVal, suit, rank);
                    cardList.Add(newCard);
                } 
            }
            Deck deck1 = new Deck(cardList);
            Random rand = new Random(); 
            for(int n = 0; n < deck1.cards.Count; n++)
            {
                var k = rand.Next(n + 1);
                var temp = deck1.cards[n];
                deck1.cards[n] = deck1.cards[k];
                deck1.cards[k] = temp;
            }
            db.Decks.Update(deck1);
            db.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        //=========Dashboard===========

        [HttpGet("/home")]
        public IActionResult Dashboard()
        {
            

            // for(int n = 0; n < deck1.cards.Count; n++)
            // {
            //     var k = rand.Next(n + 1);
            //     var temp = deck1.cards[n];
            //     deck1.cards[n] = deck1.cards[k];
            //     deck1.cards[k] = temp;
            // }
            List<Player> allPlayers = db.Players.Include(p => p.rdOneCard).Include(p => p.rdTwoCard).Include(p => p.rdThreeCard).Include(p => p.rdFourCard).ToList();
            
            // db.Decks.Add(deck1);
            // db.SaveChanges();

            ViewBag.CardArr = db.Decks.Include(d => d.cards).FirstOrDefault(d => d.DeckId > 0);
            ViewBag.players = allPlayers;
            ViewBag.RoundOneResult = HttpContext.Session.GetObjectFromJson<List<string>>("RoundOneResult");
            return View();
        }

        //========Player Actions========
        [HttpPost("RoundOne")]
        public IActionResult RoundOne(int playerId, string rdOneData)
        {
            List<string> roundOneResult = new List<string>();
            Player rdOnePlayer = db.Players.Include(p => p.rdOneCard).FirstOrDefault(p => p.PlayerId == playerId);
            if(rdOneData == "smoke")
            {
                if(rdOnePlayer.rdOneCard.Suit == "♠" || rdOnePlayer.rdOneCard.Suit == "♣")
                {
                    roundOneResult.Add(rdOnePlayer.Username + " Give 1 sip!");
                }
                else
                {
                    roundOneResult.Add(rdOnePlayer.Username + " Take 1 sip!");
                }
            }
            if(rdOneData == "fire")
            {
                if(rdOnePlayer.rdOneCard.Suit == "♡" || rdOnePlayer.rdOneCard.Suit == "♢")
                {
                    roundOneResult.Add(rdOnePlayer.Username + " Give 1 sip!");
                }
                else
                {
                    roundOneResult.Add(rdOnePlayer.Username + " Take 1 sip!");
                }
            }
            HttpContext.Session.SetObjectAsJson("RoundOneResult", roundOneResult);
            return RedirectToAction("Dashboard");
        }

        [HttpPost("RoundTwo")]
        public IActionResult RoundTwo(int playerId, string rdTwoData)
        {
            List<string> roundTwoResult = new List<string>();
            Player rdTwoPlayer = db.Players.Include(p => p.rdOneCard).Include(p => p.rdTwoCard).FirstOrDefault(p => p.PlayerId == playerId);
            if (rdTwoData == "high")
            {
                if (rdTwoPlayer.rdOneCard.Val < rdTwoPlayer.rdTwoCard.Val)
                {
                    roundTwoResult.Add(rdTwoPlayer.Username + " " +  " Give 2 sips!");
                }
                else
                {
                    roundTwoResult.Add(rdTwoPlayer.Username + " " +  " Take 2 sips");
                }
            }
            if (rdTwoData == "low")
            {
                if (rdTwoPlayer.rdOneCard.Val > rdTwoPlayer.rdTwoCard.Val)
                {
                    roundTwoResult.Add(rdTwoPlayer.Username + " " +  " Give 2 sips!");
                }
                else
                {
                    roundTwoResult.Add(rdTwoPlayer.Username + " " +  " Take 2 sips!");
                }
            }
            HttpContext.Session.SetObjectAsJson("RoundTwoResult", roundTwoResult);
            return RedirectToAction("Dashboard");
        }

        [HttpPost("RoundThree")]
        public IActionResult RoundThree(int playerId, string rdThreeData)
        {
            List<string> roundThreeResult = new List<string>();
            Player rdThreePlayer = db.Players.Include(p => p.rdOneCard).Include(p => p.rdTwoCard).Include(p => p.rdThreeCard).FirstOrDefault(p => p.PlayerId == playerId);
            if (rdThreeData == "out")
            {
                if (rdThreePlayer.rdOneCard.Val > rdThreePlayer.rdThreeCard.Val && rdThreePlayer.rdThreeCard.Val < rdThreePlayer.rdTwoCard.Val || rdThreePlayer.rdOneCard.Val < rdThreePlayer.rdThreeCard.Val && rdThreePlayer.rdThreeCard.Val > rdThreePlayer.rdTwoCard.Val)
                {
                    roundThreeResult.Add(rdThreePlayer.Username + " " +  "Give 3 sips!");
                }
                else
                {
                    roundThreeResult.Add(rdThreePlayer.Username + " " +  "Take 3 sips!");
                }
            }
            if (rdThreeData == "in")
            {
                if (rdThreePlayer.rdOneCard.Val < rdThreePlayer.rdThreeCard.Val && rdThreePlayer.rdThreeCard.Val < rdThreePlayer.rdTwoCard.Val || rdThreePlayer.rdOneCard.Val > rdThreePlayer.rdThreeCard.Val && rdThreePlayer.rdThreeCard.Val > rdThreePlayer.rdTwoCard.Val)
                {
                    roundThreeResult.Add(rdThreePlayer.Username + " " +  "Give 3 sips!");
                }
                else
                {
                    roundThreeResult.Add(rdThreePlayer.Username + " " +  "Take 3 sips!");
                }
            }
            HttpContext.Session.SetObjectAsJson("RoundThreeResult", roundThreeResult);
            return RedirectToAction("Dashboard");
        }

        [HttpPost("RoundFour")]
        public IActionResult RoundFour(int playerId, string rdFourData)
        {
            List<string> roundFourResult = new List<string>();
            Player rdFourPlayer = db.Players.Include(p => p.rdFourCard).FirstOrDefault(p => p.PlayerId == playerId);
            if (rdFourData == "spades")
            {
                if (rdFourPlayer.rdFourCard.Suit == "♠")
                {
                    roundFourResult.Add(rdFourPlayer.Username + " " +  " Give 4 sips!");
                }
                else
                {
                    roundFourResult.Add(rdFourPlayer.Username + " " +  " Take 4 sips!");
                }
            }
            if (rdFourData == "clubs")
            {
                if (rdFourPlayer.rdFourCard.Suit == "♣")
                {
                    roundFourResult.Add(rdFourPlayer.Username + " " +  " Give 4 sips!");
                }
                else
                {
                    roundFourResult.Add(rdFourPlayer.Username + " " +  " Take 4 sips!");
                }
            }
            if (rdFourData == "diamonds")
            {
                if (rdFourPlayer.rdFourCard.Suit == "♢")
                {
                    roundFourResult.Add(rdFourPlayer.Username + " " +  " Give 4 sips!");
                }
                else
                {
                    roundFourResult.Add(rdFourPlayer.Username + " " +  " Take 4 sips!");
                }
            }
            if (rdFourData == "hearts")
            {
                if (rdFourPlayer.rdFourCard.Suit == "♡")
                {
                    roundFourResult.Add(rdFourPlayer.Username + " " +  " Give 4 sips!");
                }
                else
                {
                    roundFourResult.Add(rdFourPlayer.Username + " " +  " Take 4 sips!");
                }
            }
            HttpContext.Session.SetObjectAsJson("RoundFourResult", roundFourResult);
            return RedirectToAction("Dashboard");
        }


        //=======Create a New Player=======

        [HttpPost("/newplayer")]
        public IActionResult Create(string userName)
        {
            Deck mainDeck = db.Decks.Include(d => d.cards).FirstOrDefault(d => d.DeckId > 0);
            
            List<Card> cardHand = new List<Card>();
            for(int i = 12; i < 16; i++)
            {
                cardHand.Add(mainDeck.cards[i]);
                mainDeck.cards.Remove(mainDeck.cards[i]);
                db.Decks.Update(mainDeck);
            }
            db.SaveChanges();
            Player playerNew = new Player()
            {
                Username = userName,
                rdOneCard = cardHand[0],
                rdTwoCard = cardHand[1],
                rdThreeCard = cardHand[2],
                rdFourCard = cardHand[3],
            };

            db.Players.Add(playerNew);
            db.SaveChanges();
            return RedirectToAction("Dashboard");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


// Somewhere in your namespace, outside other classes
public static class SessionExtensions
{
    // We can call ".SetObjectAsJson" just like our other session set methods, by passing a key and a value
    public static void SetObjectAsJson(this ISession session, string key, object value)
    {
        // This helper function simply serializes theobject to JSON and stores it as a string in session
        session.SetString(key, JsonConvert.SerializeObject(value));
    }
    // generic type T is a stand-in indicating that we need to specify the type on retrieval
    public static T GetObjectFromJson<T>(this ISession session, string key)
    {
        string value = session.GetString(key);
        // Upon retrieval the object is deserialized based on the type we specified
        return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
    }
}
