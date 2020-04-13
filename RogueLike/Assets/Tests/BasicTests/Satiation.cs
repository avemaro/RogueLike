using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{

    public class Satiation {
        string[] data;
        Floor floor;
        Player player;

        [SetUp]
        public void SetUp()
        {
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

            floor = FloorMaker.Create(data);
            player = floor.Player;

        }

        [Test]
        public void SatiationDecrease1per10Turns() {
            Assert.AreEqual(100, player.Satiation);
            for (var i = 0; i < 5; i++)
                player.Move(0, 4);
            Assert.AreEqual(99, player.Satiation);
            for (var i = 0; i < 5; i++)
                player.Move(0, 4);
            Assert.AreEqual(98, player.Satiation);
        }

        [Test]
        public void HPDecreases1per1TurnWhenSatiation0() {
            var hp = player.HP;
            for (var i = 0; i < 499; i++) {
                player.Move(0, 4);
                Assert.AreNotEqual(0, player.Satiation);
            }
            player.Move(0, 4);
            Assert.AreEqual(0, player.Satiation);
            Assert.AreEqual(hp, player.HP);

            player.Move(0);
            Assert.AreEqual(hp - 1, player.HP);
            player.Move(4);
            Assert.AreEqual(hp - 2, player.HP);
        }
    }
}
