using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests {
    public class Fei4 {
        string[] data;

        [OneTimeSetUp]
        public void OneTimeSetUp() {
            data = new string[] {
                "◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆",
                "◆　　　◇　　　◇　　　◇　　　◆",
                "◆　　　◇　　　◇　　　◇　　　◆",
                "◆　試杖◇　マ　◇　マ　◇　マ階◆",
                "◆　　　◇　　　◇　　　◇　　　◆",
                "◆　　　◇　　　◇　　　◇　　　◆",
                "◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆"
            };
        }

        [Test]
        public void Test_Fei4() {
            var floor = new Floor(data);
            var player = floor.Player;
            Assert.AreEqual(data, floor.Show());

            player.Move(Direction.right);
            player.Use(0);
            floor.Show();
            Assert.AreEqual((6, 3), player.Position);
            player.Use(0);
            Assert.AreEqual((10, 3), player.Position);
            player.Throw(0);
            floor.Show();
            Assert.AreEqual((14, 3), player.Position);
            player.Move(Direction.right);
            Assert.AreEqual(floor.StairPosition, player.Position);
        }
    }
}