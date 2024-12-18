﻿using HiveGame.BusinessLogic.Models.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiveGame.BusinessLogic.Models.Extensions;
using static HiveGame.BusinessLogic.Models.Board.DirectionConsts;

namespace HiveGame.BusinessLogic.Models.Insects
{
    public class InsectValidationResult
    {
        public List<IVertex> AvailableVertices { get; set; } = new List<IVertex>();
        public string? ReasonWhyEmpty { get; set; }
    }
}
