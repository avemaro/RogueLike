using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Fei8
    {
        string[] data;
        Floor floor;
        Player player;

        [SetUp]
        public void SetUp() {
            data = new string[] {
                "◆◆◆◆◆◆◆◆◆◆◆",
                "◆　　　　◇跳　　　◆",
                "◆　　　　◇眼　　　◆",
                "◆　　　　◇試　　　◆",
                "◆　　　　◇　　　　◆",
                "◆　　　階◇　　　　◆",
                "◆◆◆◆◆◆◆◆◆◆◆",
                "　◆◆◆◆◆◆◆　　　",
                "　◆　　　　　◆　　　",
                "　◆　　　　　◆　　　",
                "　◆　　　　　◆　　　",
                "　◆跳　　　　◆　　　",
                "　◆◆◆◆◆◆◆　　　",
                "　◆　　　　跳◆　　　",
                "　◆　　　　　◆　　　",
                "　◆　　　　　◆　　　",
                "　◆　　　　　◆　　　",
                "　◆◆◆◆◆◆◆　　　"
            };

            floor = new Floor(data);
            player = floor.Player;
        }

        [Test]
        public void Test_floorHasPrinted() {
            var expected = new string[] {
                "◆◆◆◆◆◆◆◆◆◆◆",
                "◆　　　　◇　　　　◆",
                "◆　　　　◇眼　　　◆",
                "◆　　　　◇試　　　◆",
                "◆　　　　◇　　　　◆",
                "◆　　　階◇　　　　◆",
                "◆◆◆◆◆◆◆◆◆◆◆",
                "　◆◆◆◆◆◆◆　　　",
                "　◆　　　　　◆　　　",
                "　◆　　　　　◆　　　",
                "　◆　　　　　◆　　　",
                "　◆　　　　　◆　　　",
                "　◆◆◆◆◆◆◆　　　",
                "　◆　　　　　◆　　　",
                "　◆　　　　　◆　　　",
                "　◆　　　　　◆　　　",
                "　◆　　　　　◆　　　",
                "　◆◆◆◆◆◆◆　　　"
            };
            Assert.AreEqual(expected, floor.Show());
        }

        [Test]
        public void Test_warpTrapWorks() {
            player.Move(Direction.up);
            player.Use(0);
            player.Move(Direction.up);
            floor.Show();
            Assert.AreNotEqual((6, 1), player.Position);
        }
    }
}
