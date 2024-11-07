﻿using HiveGame.BusinessLogic.Models.Insects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiveGame.BusinessLogic.Models.Board
{
    public class BoardDTO
    {
        public List<VertexDTO> hexes { get; set; }
        public List<long> vertexidtoput { get; set; }
        public bool queenrulemet { get; set; }
    }
}
