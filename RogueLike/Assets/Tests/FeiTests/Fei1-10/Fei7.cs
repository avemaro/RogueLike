using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Fei7
    {
        string[] data;
        Floor floor;
        Player player;

        [SetUp]
        public void SetUp() {
            data = new string[] {
                "◆◆◆◆◆◆◆◆◆",
                "◆Ｇ　　　　　Ｇ◆",
                "◆　武Ｇ武Ｇ武　◆",
                "◆Ｇ武　武　武Ｇ◆",
                "◆　武　武　武　◆",
                "◆　Ｇ　　　Ｇ　◆",
                "◆　◇◇◇◇◇階◆",
                "◆Ｇ　試眠　　　◆",
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
        public void Test_Fei7_Fail() {
            player.Move(2, 2, 2, 1);
            floor.Show();
            Assert.AreNotEqual(floor.StairPosition, player.Position);
        }

        [Test]
        public void Test_Fei7_Pass() {
            player.Move(2);
            player.Use(0);
            player.Move(2, 2, 1);
            floor.Show();
            Assert.AreEqual(floor.StairPosition, player.Position);
        }

        [Test]
        public void Test_EnemiesMove() {
            var enemy = floor.GetEnemy(6, 4);
            player.Move(2);
            floor.Show();
            Assert.AreEqual((5, 5), enemy.Position);
            player.Move(2);
            floor.Show();
            player.Move(2);
            floor.Show();
            Assert.AreEqual((6, 5), enemy.Position);
        }
    }
}
