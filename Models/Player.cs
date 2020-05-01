using System;
using RocketShip.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RocketShip.Models
{
    public class Player
    {
        [Key]
        public int PlayerId { get;set; }
        public string Username { get;set; }

        public Card rdOneCard { get;set; }
        public Card rdTwoCard { get;set; }
        public Card rdThreeCard { get;set; }
        public Card rdFourCard { get;set; }

        public Player() { }

    }

}