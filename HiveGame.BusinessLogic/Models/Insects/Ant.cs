﻿using HiveGame.BusinessLogic.Models.Board;
using HiveGame.Core.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiveGame.BusinessLogic.Models.Insects
{
    public class Ant : Insect
    {
        public Ant(PlayerColor color)
        {
            Type = InsectType.Ant;
            PlayerColor = color;
        }

        public override InsectValidationResult GetAvailableVertices(IVertex moveFrom, IHiveBoard board)
        {
            var result = new InsectValidationResult();

            List<IVertex> vertices = BasicCheck(moveFrom, board, out string? whyMoveImpossible);

            if(vertices.Count == 0) 
            {
                result.ReasonWhyEmpty = whyMoveImpossible;
                return result;
            }

            var freeHexesAround = CheckNotSurroundedFields(moveFrom, board);

            if (freeHexesAround.Count == 0)
            {
                result.ReasonWhyEmpty = "This insect is surrounded and can't move";
                return result;
            }

            List<IVertex> hexesToMoveFromfreeHexes = freeHexesAround
                .SelectMany(x => GetVerticesByBFS(moveFrom, board))
                .Distinct()
                .ToList();

            result.AvailableVertices = vertices.Intersect(hexesToMoveFromfreeHexes).ToList();

            return result;
        }
    }
}
