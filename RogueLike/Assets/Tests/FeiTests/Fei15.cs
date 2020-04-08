using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Fei15
    {
        string[] data;
        Floor floor;
        Player player;

        [SetUp]
        public void SetUp() {
            data = new string[] {
                "◆◆◆◆◆◆◆◆◆",
                "◆　　　　　　　◆",
                "◆　ト　　　試　◆",
                "◆◇◇◇◇◇◇◇◆",
                "◆　　　◇　　　◆",
                "◆　杖　　　武　◆",
                "◆　　　階　　　◆",
                "◆◆◆◆◆◆◆◆◆"
            };
            floor = new Floor(data);
            player = floor.Player;
        }

        [Test]
        public void Test_floorHasPrinted() {
            Assert.AreEqual(data, floor.Show());
        }

        [Test]
        public void Test_Pass() {
            player.Move(6, 6, 6, 6, 4);
            player.Use(0);
            player.Move(2);
            player.Throw(0);
            floor.Show();
            player.Move(2, 2, 2, 2);
            floor.Show();
            player.Move(6, 6, 4);
            floor.Show();
            player.Use(0);
            player.Move(4, 5);
            Assert.AreEqual(floor.StairPosition, player.Position);
        }
    }
}
