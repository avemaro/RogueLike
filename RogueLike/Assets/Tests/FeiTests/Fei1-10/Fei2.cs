using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Fei2
    {
        string[] data;

        [OneTimeSetUp]
        public void OneTimeSetUp() {
            data = new string[] {
                "◆◆◆◆◆◆◆◆◆◆◆　　　　　",
                "◆　　　◆◆◆　　　◆　　　　　",
                "◆　　試☆☆☆　　　◆　　　　　",
                "◆　　　◆◆◆　　　◆　　　　　",
                "◆◆◆◆◆◆◆　　　◆　　　　　",
                "　　　　　　◆　　　◆　　　　　",
                "　　　　　　◆◆☆◆◆　　　　　",
                "　　　　　　　◆☆◆　　　　　　",
                "　　　　　　　◆☆◆　　　　　　",
                "　　　　　　　◆☆◆◆◆◆◆◆◆",
                "　　　　　　　◆☆☆☆☆☆☆☆◆",
                "　　　　　　　◆◆☆☆☆☆☆☆◆",
                "　　　　　　　◆◆☆☆☆☆☆◆◆",
                "　　　　　　　◆◆◆☆☆☆☆◆◆",
                "　　　　　　　◆◆◆☆☆☆◆◆◆",
                "　　　　　　　◆◆◆◆☆☆◆◆◆",
                "　　　　　　　◆◆◆◆階◆◆◆◆",
                "　　　　　　　◆◆◆◆◆◆◆◆◆"
            };
        }


        [Test]
        public void Test_Fei2() {
            var floor = new Floor(data);
            var player = floor.Player;
            floor.Show();

            Assert.False(player.Move(Direction.right));
            player.Attack();
            floor.Show();
            Assert.True(player.Move(Direction.right));
            floor.Show();

            int[] moves = { 2, 2, 2, 3, 4, 4 };
            Assert.True(player.Move(DirectionExtend.GetDirections(moves)));
            floor.Show();
            player.Attack();
            floor.Show();

            moves = new int[] { 4, 4, 4, 4, 4,
                                2, 3, 3, 4, 4,
                                4, 4 };
            Assert.True(player.Move(DirectionExtend.GetDirections(moves)));
            floor.Show();
            Assert.AreEqual(floor.StairPosition, player.Position);
        }

    }
}
