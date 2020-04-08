using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Fei11
    {
        string[] data;
        Floor floor;
        Player player;

        [SetUp]
        public void SetUp() {
            data = new string[] {
                "　　　　　　　　　　　◆◆◆◆◆◆◆",
                "◆◆◆◆◆◆◆◆　　　◆　武　　　◆",
                "◆　武　　　　◆　　　◆　　　武　◆",
                "◆　　　　武　◆◆◆◆◆　　　　　◆",
                "◆試真　武　　　　　　　　真　階武◆",
                "◆　　　　武　◆◆◆◆◆　　　　　◆",
                "◆　武　　　　◆　　　◆　　　武　◆",
                "◆◆◆◆◆◆◆◆　　　◆　武　　　◆",
                "　　　　　　　　　　　◆◆◆◆◆◆◆"
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
            player.Move(2, 2, 1);
            floor.Show();
            Assert.AreEqual(State.Dead, player.state);
        }

        //[Test]
        //public void Test_Fail2() {
        //    player.Move(2);
        //    player.Use(0);
        //    player.Move(2, 2, 2, 2, 2,
        //                2, 2, 2, 2, 2,
        //                2);
        //    floor.PrintFloor();
        //    player.Move(2, 2);
        //    floor.PrintFloor();
        //    Assert.AreEqual(State.Dead, player.state);
        //}

        [Test]
        public void Test_Pass() {
            player.Move(2);
            player.Use(0);
            player.Move(2, 2, 2, 2, 2,
                        2, 2, 2, 2, 2,
                        2);
            floor.Show();
            player.Use(0);
            player.Move(2, 2);
            floor.Show();
            Assert.AreEqual(floor.StairPosition, player.Position);
        }
    }
}
