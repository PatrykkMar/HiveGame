﻿using HiveGame.BusinessLogic.Models.Graph;
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

        public override IList<Vertex> GetAvailableVertices(Vertex moveFrom, HiveBoard board)
        {
            var vertices = BasicCheck(moveFrom, board);

            if (CheckIfSurrounded(moveFrom, board))
                return new List<Vertex>();

            vertices = vertices.Intersect(board.GetAdjacentVerticesByCoordList(moveFrom)).ToList();

            vertices = GetAntVerticesByBFS(moveFrom, board);

            return vertices;
        }

        private IList<Vertex> GetAntVerticesByBFS(Vertex moveFrom, HiveBoard board) 
        {
            var result = new List<Vertex>();
            var visited = new HashSet<Vertex>();
            var queue = new Queue<Vertex>();

            queue.Enqueue(moveFrom);
            visited.Add(moveFrom);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (current.IsEmpty)
                {
                    result.Add(current);
                }

                var adjacent = board.GetAdjacentVerticesByCoordList(current);

                if (adjacent.Count < 5)
                {
                    foreach (var edge in adjacent)
                    {
                        if (!visited.Contains(edge))
                        {
                            visited.Add(edge);
                            queue.Enqueue(edge);
                        }
                    }
                }
            }

            return result;
        }
    }
}
