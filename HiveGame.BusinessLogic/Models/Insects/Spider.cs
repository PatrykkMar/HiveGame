﻿using HiveGame.BusinessLogic.Models.Board;
using HiveGame.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiveGame.BusinessLogic.Models.Insects
{
    public class Spider : Insect
    {
        public Spider(PlayerColor color)
        {
            Type = InsectType.Spider;
            PlayerColor = color;
        }

        public override InsectValidationResult GetAvailableVertices(IVertex moveFrom, IHiveBoard board)
        {
            var result = new InsectValidationResult();

            List<IVertex> vertices = BasicCheck(moveFrom, board, out string? whyMoveImpossible);

            if (vertices.Count == 0)
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
                .SelectMany(x => GetVerticesByBFS(moveFrom, board, onlyDistance: 3))
                .Distinct()
                .ToList();

            result.AvailableVertices = vertices.Where(x => hexesToMoveFromfreeHexes.Any(y => y.Equals(x))).ToList();

            return result;
        }
    }
}
