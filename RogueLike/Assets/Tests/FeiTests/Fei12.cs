using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Fei12
    {
        string[] data;
        Floor floor;
        Player player;

        [SetUp]
        public void SetUp() {
            data = new string[] {
                "◆◆◆◆◆◆◆◆◆◆◆　　　　　　　",
                "◆　武　　武　　　　◆　　　　　　　",
                "◆　　　　　　　　　◆　　　　　　　",
                "◆　　　　　　　　　◆　　◆◆◆◆◆",
                "◆　　　　眼　　　　◆◆◆◆　　　◆",
                "◆武　　薬爆薬　　武　　　　　　階◆",
                "◆　　　試眼　　　　◆◆◆◆　　　◆",
                "◆　　 　　　 　　◆　　◆◆◆◆◆",
                "◆　　　　　　　　　◆　　　　　　　",
                "◆　武　　武　　武　◆　　　　　　　",
                "◆◆◆◆◆◆◆◆◆◆◆　　　　　　　"
            };
            floor = new Floor(data);
            player = floor.Player;
        }

        [Test]
        public void Test_floorHasPrinted() {
            var expected = new string[] {
                "◆◆◆◆◆◆◆◆◆◆◆　　　　　　　",
                "◆　武　　武　　　　◆　　　　　　　",
                "◆　　　　　　　　　◆　　　　　　　",
                "◆　　　　　　　　　◆　　◆◆◆◆◆",
                "◆　　　　眼　　　　◆◆◆◆　　　◆",
                "◆武　　薬　薬　　武　　　　　　階◆",
                "◆　　　試眼　　　　◆◆◆◆　　　◆",
                "◆　　　　　　　　　◆　　◆◆◆◆◆",
                "◆　　　　　　　　　◆　　　　　　　",
                "◆　武　　武　　武　◆　　　　　　　",
                "◆◆◆◆◆◆◆◆◆◆◆　　　　　　　"
            };
            Assert.AreEqual(expected, floor.Show());
        }

        [Test]
        public void Test_Pass() {
            player.Move(0, 1);
            floor.Show();
            player.Move(4);
            floor.Show();
            Assert.True(player.Move(2));
            floor.Show();
            Assert.True(player.Move(2));
            floor.Show();
            player.Move(2, 2, 2, 2, 2,
                        2, 2, 2, 2);
            floor.Show();
            Assert.AreEqual(State.Alive, player.state);
            Assert.AreEqual(floor.StairPosition, player.Position);
        }
    }
}
