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

        [Test]
        public void PawnAttacks() {
            data = new string[] {
                "◆◆◆◆◆◆◆　　　　　",
                "◆階　　　　◆◆◆◆◆◆",
                "◆　　　　　　　　　　◆",
                "◆　　　マ　◆◆◆◆　◆",
                "◆◆◆◆◇◆◆◆◆◆　◆",
                "◆　　草　　◆◆◆◆　◆",
                "◆　　試　　　　　　　◆",
                "◆　　　　　◆◆◆◆◆◆",
                "◆◆◆◆◆◆◆　　　　　"
            };
            floor = new Floor(data);
            player = floor.Player;
            var enemy = floor.GetEnemy(4, 3);
            enemy.HP = 10;

            player.Move(Direction.left);
            var piece = player.Spawn(Chess.Pawn);
            player.Move(2, 2, 2, 2, 2,
                        2, 2, 2, 0, 0,
                        0, 0, 6, 6, 6,
                        6);
            Assert.Less(piece.HP, 10);
            player.Attack();
            Assert.Less(enemy.HP, 10);
            floor.Show();
        }
    }
}
