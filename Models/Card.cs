using System;
using RocketShip.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RocketShip.Models
{
    public class Card
    {
        [Key]
        public int CardId { get;set; }
        public string StringVal { get;set; }
        public string Suit { get;set; }
        public int Val { get;set; }
        public Card() { }
        public Card(string strVal, string suit, int val)
        {
            StringVal = strVal;
            Suit = suit;
            Val = val;
        }
    }
}