﻿using HiveGame.BusinessLogic.Models;
using HiveGame.BusinessLogic.Models.Board;
using HiveGame.BusinessLogic.Models.Insects;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiveGameTests.Models.Board
{
    public class HiveBoardTests
    {
        private readonly IHiveBoard _board;

        public HiveBoardTests()
        {
            _board = new HiveBoard();
        }

        [Fact]
        public void AddVertex_ShouldAddVertexToBoard()
        {
            var vertex = new Vertex();

            _board.AddVertex(vertex);

            Assert.Single(_board.Vertices);
            Assert.Contains(vertex, _board.Vertices);
        }

        [Fact]
        public void EmptyVertices_ShouldReturnOnlyEmptyVertices()
        {
            var emptyVertexMock = new Mock<IVertex>();
            emptyVertexMock.Setup(x => x.Coords).Returns(new Point2D(0, 0));
            emptyVertexMock.Setup(v => v.IsEmpty).Returns(true);

            var nonEmptyVertexMock = new Mock<IVertex>();
            nonEmptyVertexMock.Setup(x => x.Coords).Returns(new Point2D(0, 1));
            nonEmptyVertexMock.Setup(v => v.IsEmpty).Returns(false);

            _board.AddVertex(emptyVertexMock.Object);
            _board.AddVertex(nonEmptyVertexMock.Object);

            var result = _board.EmptyVertices;

            Assert.Single(result);
            Assert.Contains(emptyVertexMock.Object, result);
        }
        [Fact]
        public void FirstMoves_ShouldReturnTrueWhenLessThanTwoNonEmptyVertices()
        {
            var nonEmptyVertexMock = new Mock<IVertex>();
            nonEmptyVertexMock.Setup(x => x.Coords).Returns(new Point2D(0, 1));
            nonEmptyVertexMock.Setup(v => v.IsEmpty).Returns(false);

            _board.AddVertex(nonEmptyVertexMock.Object);

            var result = _board.FirstMoves;

            Assert.True(result);
        }

        [Fact]
        public void FirstMoves_ShouldReturnFalseWhenTwoOrMoreNonEmptyVertices()
        {
            var nonEmptyVertexMock1 = new Mock<IVertex>();
            nonEmptyVertexMock1.Setup(v => v.IsEmpty).Returns(false);
            nonEmptyVertexMock1.Setup(x => x.Coords).Returns(new Point2D(0, 0));
            var nonEmptyVertexMock2 = new Mock<IVertex>();
            nonEmptyVertexMock2.Setup(v => v.IsEmpty).Returns(false);
            nonEmptyVertexMock2.Setup(x => x.Coords).Returns(new Point2D(0, 1));

            _board.AddVertex(nonEmptyVertexMock1.Object);
            _board.AddVertex(nonEmptyVertexMock2.Object);

            var result = _board.FirstMoves;

            Assert.False(result);
        }

        [Fact]
        public void GetVertexByCoord_ShouldReturnNullIfVertexNotExist()
        {
            var result = _board.GetVertexByCoord(1, 2);

            Assert.Null(result);
        }

        [Fact]
        public void GetVertexByCoord_ShouldReturnVertexIfExists()
        {
            var vertex = new Vertex { Coords = new Point2D(1, 2) };
            _board.AddVertex(vertex);

            var result = _board.GetVertexByCoord(1, 2);

            Assert.Equal(vertex, result);
        }

        [Fact]
        public void AddEmptyVerticesAround_ShouldBe7Vertices()
        {
            var vertexMock = new Mock<IVertex>();
            vertexMock.Setup(v => v.Coords).Returns(new Point2D(0, 0));

            _board.AddVertex(vertexMock.Object);

            _board.AddEmptyVerticesAround(vertexMock.Object);

            Assert.Equal(7, _board.Vertices.Count);
        }

    }
}
