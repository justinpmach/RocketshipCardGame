using System;
using System.Collections.Generic;
using RocketShip.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RocketShip.Models
{
    public class Deck
    {
        [Key]
        public int DeckId { get;set; }
        public List<Card> cards { get;set; }

        public Deck() { }
        public Deck(List<Card> cardlist)
        {
            cards = cardlist;
        }
    }

}