using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Fei10
    {
        string[] data;
        Floor floor;
        Player player;

        [SetUp]
        public void SetUp() {
            data = new string[] {
                "◆◆◆◆◆◆◆◆◆◆◆◆◆",
                "◆　　　　　　　　　　　◆",
                "◆　　◇　　　　　　　　◆",
                "◆　試　　　ク　　　　階◆",
                "◆　　◆　　　　　　　　◆",
                "◆　　　　　　　　　　　◆",
                "◆◆◆◆◆◆◆◆◆◆◆◆◆"
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
            player.Move(Direction.right);
            Assert.AreEqual(State.Dead, player.state);
        }

        [Test]
        public void Test_Pass() {
            player.Move(4, 0, 4);
            floor.Show();
            player.Move(6);
            floor.Show();
            player.Move(3, 7);
            floor.Show();
            Assert.AreEqual(State.Alive, player.state);
        }
    }
}


