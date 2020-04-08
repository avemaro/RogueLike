using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Fei14
    {
        string[] data;
        Floor floor;
        Player player;

        [SetUp]
        public void SetUp() {
            data = new string[] {
                "◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆",
                "◆　　　◆　　　　　◆　　　　◆",
                "◆　　　◇　　◆　　◇　丸　眼◆",
                "◆　階　◇　　　　　◇　　　試◆",
                "◆　　　◇　　　　　◇　　　　◆",
                "◆　　　◆　丸　　　◆　　　　◆",
                "◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆"
            };
            floor = new Floor(data);
            player = floor.Player;
        }

        [Test]
        public void Test_floorHasPrinted() {
            var expected = new string[] {
                "◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆",
                "◆　　　◆　　　　　◆　　　　◆",
                "◆　　　◇　　◆　　◇　　　眼◆",
                "◆　階　◇　　　　　◇　　　試◆",
                "◆　　　◇　　　　　◇　　　　◆",
                "◆　　　◆　　　　　◆　　　　◆",
                "◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆"
            };

            Assert.AreEqual(expected, floor.Show());
        }

        [Test]
        public void Test_LogTrapWorks() {
            player.Move(6, 6, 7, 2);
            Assert.AreEqual((8, 2), player.Position);
            player.Move(4, 5, 6, 6, 3);
            Assert.AreEqual((2, 1), player.Position);
            player.Move(4, 4);
            Assert.AreEqual(floor.StairPosition, player.Position);
        }
    }
}
