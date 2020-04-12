using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PawnTest
    {
        string[] data;
        Floor floor;
        Player player;

        [SetUp]
        public void SetUp() {
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
            floor = new Floor(data);
            player = floor.Player;
        }

        [Test]
        public void PawnHasSpawned() {
            player.Spawn(Chess.Pawn);
            var pawn = floor.GetCreature(player.Front);
            Assert.AreEqual(player.Front, pawn.Position);
            floor.Show();
        }

        [Test]
        public void PawnFollows() {
            player.Spawn(Chess.Pawn);
            Assert.True(player.Move(Direction.down));
            var pawn = floor.GetCreature(player.Front);
            Assert.AreEqual(player.Front, pawn.Position);
            Assert.NotNull(floor.GetCreature(player.Front));
            floor.Show();
            Assert.True(player.Move(Direction.down));
            Assert.Null(floor.GetCreature(player.Front));
            floor.Show();
            Assert.True(player.Move(Direction.up));
            Assert.NotNull(floor.GetCreature(player.Back));
            floor.Show();
        }
    }
}
