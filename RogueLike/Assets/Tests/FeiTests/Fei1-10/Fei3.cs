﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Fei3
    {
        string[] data;

        [OneTimeSetUp]
        public void OneTimeSetUp() {
            data = new string[] {
                "◆◆◆◆◆◆◆　　　　　",
                "◆階　　　　◆◆◆◆◆◆",
                "◆　　　　　　　　　　◆",
                "◆　　　マ　◆◆◆◆　◆",
                "◆◆◆◆◇◆◆◆◆◆　◆",
                "◆　　草　　◆◆◆◆　◆",
                "◆　　試　　　　　　　◆",
                "◆　　　　　◆◆◆◆◆◆",
                "◆◆◆◆◆◆◆　　　　　"
            };
        }

        [Test]
        public void PlayerPickUpItem() {
            var floor = FloorMaker.Create(data);
            var player = floor.Player;
            var item = floor.GetItem(3, 5);
            //Assert.AreEqual(data, floor.Show());
            Assert.NotNull(item);

            player.Move(Direction.up);
            floor.Show();

            Assert.AreEqual(item, player.Items[0]);
            Assert.IsNull(floor.GetItem(3, 5));
        }

        [Test]
        public void PlayerUseItem() {
            var floor = FloorMaker.Create(data);
            var player = floor.Player;
            var enemy = floor.GetEnemy(4, 3);
            Assert.NotNull(enemy);

            player.Move(Direction.up);
            player.Use(0);
            floor.Show();
            Assert.AreEqual(0, player.Items.Count);
            Assert.False(player.IsState(State.Dead));

            floor = FloorMaker.Create(data);
            player = floor.Player;
            enemy = floor.GetEnemy(4, 3);

            player.Move(Direction.up);
            player.Move(Direction.right);
            player.Move(Direction.up);
            player.Use(0);
            floor.Show();
            Assert.AreEqual(0, player.Items.Count);
            Assert.True(enemy.IsState(State.Dead));
        }

        [Test]
        public void PlayerHasAttacked() {
            var floor = FloorMaker.Create(data);
            var player = floor.Player;
            player.HP = 0;

            int[] moves = { 2, 2, 2, 2, 2,
                            2, 2, 0, 0, 0,
                            0, 6, 6, 6, 6,
                            6 };
            player.Move(DirectionExtend.GetDirections(moves));
            Debug.Log(player.Position);
            floor.Show();
            Assert.True(player.IsState(State.Dead));
        }

        [Test]
        public void Test_Fei3() {
            var floor = FloorMaker.Create(data);
            var player = floor.Player;
            player.HP = 0;

            int[] moves = { 2, 2, 2, 2, 2,
                            2, 2, 0, 0, 0,
                            0, 6, 6, 6, 6,
                            6, 6, 6, 6, 7,
                            7};
            player.Move(moves);
            Assert.True(player.IsState(State.Dead));
            Assert.AreNotEqual(floor.StairPosition, player.Position);

            floor = FloorMaker.Create(data);
            player = floor.Player;

            moves = new int[] { 0, 2, 0 };
            player.Move(moves);
            player.Use(0);
            floor.Show();

            moves = new int[] { 3, 2, 2, 2, 2,
                                2, 0, 0, 0, 0,
                                6, 6, 6, 6, 6,
                                6, 6, 6, 7 };
            player.Move(moves);
            floor.Show();
            Assert.False(player.IsState(State.Dead));
            Assert.AreEqual(2, floor.NumberOfStairs);
        }
    }
}
