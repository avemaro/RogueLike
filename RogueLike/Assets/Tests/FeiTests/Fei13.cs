using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Fei13
    {
        string[] data;
        Floor floor;
        Player player;

        [SetUp]
        public void SetUp() {
            data = new string[] {
                "◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆",
                "◆◆◆◆◆◆◆◆Ｇ◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆",
                "◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆Ｇ◆",
                "◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆",
                "◆つ透◆◆◆◆◆◆◆◆◆◆◆◆◆◆☆☆☆◆◆◆◆◆◆◆◆◆◆",
                "◆試つ◆◆◆Ｇ　　◇　　　◆◆◆◆☆Ｇ☆◆◆◆◆　　　　◆◆",
                "◆◆◆◆◆◆　　　◇　つ　◆◆◆◆☆☆☆◆◆◆◆　　　武◆◆",
                "◆◆◆◆◆◆　　　◇　　　◆◆◆◆◆◆◆◆◆◆◆階　　　◆◆",
                "◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆",
                "◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆",
                "◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆"
            };
            floor = new Floor(data);
            player = floor.Player;
        }

        [Test]
        public void Test_floorHasPrinted() {
            Assert.AreEqual(data, floor.Show());
        }

        [Test]
        public void Test_wallHasDigged() {
            floor.Show();
            player.Move(Direction.right);
            player.Equip(0);
            player.Attack();
            player.Move(Direction.right);
            player.Attack();
            player.Move(Direction.right);
            player.Attack();
            player.Move(Direction.right);
            floor.Show();

            var expected = new string[] {
                "◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆",
                "◆◆◆◆◆◆◆◆Ｇ◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆",
                "◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆Ｇ◆",
                "◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆",
                "◆つ透◆◆◆◆◆◆◆◆◆◆◆◆◆◆☆☆☆◆◆◆◆◆◆◆◆◆◆",
                "◆　　　　試Ｇ　　◇　　　◆◆◆◆☆Ｇ☆◆◆◆◆武　　　◆◆",
                "◆◆◆◆◆◆　　　◇　つ　◆◆◆◆☆☆☆◆◆◆◆　　　　◆◆",
                "◆◆◆◆◆◆　　　◇　　　◆◆◆◆◆◆◆◆◆◆◆階　　　◆◆",
                "◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆",
                "◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆",
                "◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆"
            };
            Assert.AreEqual(expected, floor.Show());
        }
    }
}
