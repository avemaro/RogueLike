using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Fei5
    {
        string[] data;

        [OneTimeSetUp]
        public void OneTimeSetUp() {
            data = new string[] {
                "◆◆◆◆◆◆◆◆◆",
                "◆　　　　　　　◆",
                "◆　　巻試吹　　◆",
                "◆◇◇◇◇◇◆　◆",
                "◆　　　ギ　　　◆",
                "◆　◆◇◇◇◆　◆",
                "◆　◆◇◇◇◆　◆",
                "◆　　　階　　　◆",
                "◆◆◆◆◆◆◆◆◆"
            };
        }

        [Test]
        public void Test_Fei5HasPrinted() {
            var floor = new Floor(data);
            Assert.AreEqual(data, floor.Show());
        }

        [Test]
        public void Test_Fei5() {
            var floor = new Floor(data);
            var player = floor.Player;
            var enemy = floor.GetEnemy(4, 4);

            player.Move(2, 2, 2, 4, 4, 6);
            player.Use(0);
            floor.Show();
            Assert.AreEqual((1, 4), enemy.Position);
        }
    }
}
