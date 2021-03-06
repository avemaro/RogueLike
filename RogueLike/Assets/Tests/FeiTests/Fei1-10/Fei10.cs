﻿using System.Collections;
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
            floor = FloorMaker.Create(data);
            player = floor.Player;
            player.HP = 1;
        }

        //[Test]
        //public void Test_floorHasPrinted() {
        //    //Assert.AreEqual(data, floor.Show());
        //}

        [Test]
        public void Test_Fail() {
            var hp = player.HP;
            player.Move(Direction.right);
            Assert.Less(hp, player.HP);
        }

        [Test]
        public void Test_Pass() {
            player.Move(4, 0, 4);
            floor.Show();
            player.Move(6);
            floor.Show();
            player.Move(3, 7);
            floor.Show();
            Assert.False(player.IsState(State.Dead));
        }
    }
}


