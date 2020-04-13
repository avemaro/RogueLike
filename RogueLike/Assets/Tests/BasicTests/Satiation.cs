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
    }
}
