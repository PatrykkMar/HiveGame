﻿using HiveGame.BusinessLogic.Models.Insects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace HiveGame.BusinessLogic.Models
{
    public class Player
    {
        public string? PlayerId { get; set; }
        public PlayerColor PlayerColor { get; set; }
        public Dictionary<InsectType, int> PlayerInsects { get; set; }
        public int NumberOfMove { get; set; }
    }

    public enum PlayerColor
    {
        White = 0, Black = 1
    }
}
