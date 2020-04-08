using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Fei9
    {
        string[] data;
        Floor floor;
        Player player;

        [SetUp]
        public void SetUp() {
            data = new string[] {
                "◆◆◆◆◆◆◆◆◆",
                "◆　　階　　　　◆",
                "◆　◇◇　◇◇　◆",
                "◆　◇　　　◇　◆",
                "◆　◇武武武◇試◆",
                "◆　◇◇◇◇◇　◆",
                "◆　　　　縛　　◆",
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
        public void Test_Fail() {
            player.Move(0, 0, 7, 6, 6, 6);
            Assert.AreNotEqual(floor.StairPosition, player.Position);
        }

        [Test]
        public void Test_Pass() {
            player.Move(4, 5, 6, 6, 6);
            floor.Show();
            player.Move(6, 7, 0);
            floor.Show();
            player.Move(0, 0, 2);
            player.Use(0);
            player.Move(1, 2);
            floor.Show();
            Assert.AreEqual(floor.StairPosition, player.Position);
            Assert.AreEqual(State.Alive, player.state);
        }
    }
}
