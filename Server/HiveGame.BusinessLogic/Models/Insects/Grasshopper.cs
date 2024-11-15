﻿using HiveGame.BusinessLogic.Models.Extensions;
using HiveGame.BusinessLogic.Models.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HiveGame.BusinessLogic.Models.Board.DirectionConsts;

namespace HiveGame.BusinessLogic.Models.Insects
{
    public class Grasshopper : Insect
    {
        public Grasshopper(PlayerColor color)
        {
            Type= InsectType.Grasshopper;
            PlayerColor = color;
        }

        public override InsectValidationResult GetAvailableVertices(Vertex moveFrom, HiveBoard board)
        {
            var result = new InsectValidationResult();

            var vertices = BasicCheck(moveFrom, board, out string? whyMoveImpossible);

            if (vertices.Count == 0)
            {
                result.ReasonWhyEmpty = whyMoveImpossible;
                return result;
            }

            var possibleMoves = GetPossibleMovesForGrasshopper(moveFrom, board);

            result.AvailableVertices = vertices
                .Where(x => possibleMoves.Any(y => y.Equals(x)))
                .Distinct()
                .ToList();

            return result;
        }

        public List<Vertex> GetPossibleMovesForGrasshopper(Vertex moveFrom, HiveBoard board)
        {
            var possibleMoves = new List<Vertex>();

            foreach ( var direction in Enum.GetValues<Direction>()) 
            {
                if (direction == Direction.Up || direction == Direction.Down)
                    continue;

                var currentPoint = moveFrom.Coords;

                var offset2D = NeighborOffsetsDict[direction].To2D();

                while(!board.GetVertexByCoord(currentPoint).IsEmpty)
                {
                    currentPoint = new Point2D(currentPoint.X + offset2D.X, currentPoint.Y + offset2D.Y);
                }

                possibleMoves.Add(board.GetVertexByCoord(currentPoint));
            }

            var verticesToRemove = board.GetAdjacentVerticesByCoordList(moveFrom);

            possibleMoves = possibleMoves.Except(verticesToRemove).ToList();

            return possibleMoves;
        }
    }
}