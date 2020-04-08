using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Fei1
    {
        string[] data;

        [OneTimeSetUp]
        public void OneTimeSetUp() {
            data = new string[] {
                "◆◆◆◆◆◆◆◆◆◆◆",
                "◆　　　◇　◇　◇　◆",
                "◆　試　　◆　◆　◇◆",
                "◆　　　◇　　　◇　◆",
                "◆◇　◆　◇◆◇　◇◆",
                "◆　◇　◇　◇　◇　◆",
                "◆◇　◇◆◇　◆　◇◆",
                "◆　◇　　　◇　　　◆",
                "◆◇　◆　◆　　階　◆",
                "◆　◇　◇　◇　　　◆",
                "◆◆◆◆◆◆◆◆◆◆◆"
            };
        }

        [Test]
        public void DangeonDataHasLoaded() {
            var floor = new Floor(data);
            Assert.AreEqual(TerrainType.wall, floor.GetTerrain(0, 0));
            Assert.AreEqual(TerrainType.land, floor.GetTerrain(1, 1));
            Assert.AreEqual(TerrainType.water, floor.GetTerrain(4, 1));
            Assert.AreEqual((8, 8), floor.StairPosition);
            Assert.AreEqual((2, 2), floor.Player.Position);
            floor.Show();
        }

        [Test]
        public void PlayerMoves() {
            var floor = new Floor(data);
            var player = floor.Player;

            Assert.True(player.Move(Direction.down));
            Assert.False(player.Move(Direction.downRight));
            Assert.True(player.Move(Direction.down));
            Assert.False(player.Move(Direction.downRight));
            Assert.True(player.Move(Direction.downLeft));
            Assert.AreEqual((1, 5), player.Position);
            floor.Show();
        }

        [Test]
        public void Test_Fei1() {
            var floor = new Floor(data);
            var player = floor.Player;

            int[] wrongMoves = new int[18];
            for (int i = 0; i < wrongMoves.Length; i++)
                wrongMoves[i] = Random.Range(0, 7);
            player.Move(DirectionExtend.GetDirections(wrongMoves));
            Assert.AreNotEqual((8, 8), player.Position);

            floor = new Floor(data);
            player = floor.Player;
            int[] correctMoves = { 4, 4, 5, 3, 3,
                                   2, 2, 1, 7, 7,
                                   1, 2, 2, 3, 3,
                                   5, 4, 4};
            player.Move(DirectionExtend.GetDirections(correctMoves));
            Assert.AreEqual(floor.StairPosition, player.Position);
        }


    }
}
