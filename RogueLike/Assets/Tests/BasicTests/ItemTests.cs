using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ItemTests
    {
        Floor floor;
        Player player;

        [SetUp]
        public void SetUp() {
            floor = FloorMaker.Create();
            player = floor.Player;
        }


        [Test]
        public void CreateMedicinalHerb() {
            var item = ItemMaker.Create("MedicinalHerb");
            player.HP -= 5;
            var hp = player.HP;
            player.Use(item);
            Assert.Greater(player.HP, hp);
        }

        [Test]
        public void HPisLessThanMaxHP() {
            player = new Player(floor, 100);
            Assert.AreEqual(100, player.MaxHP);
            Assert.AreEqual(100, player.HP);
            player.HP -= 30;

            var item = ItemMaker.Create("MedicinalHerb");
            player.Use(item);
            Assert.AreEqual(95, player.HP);
            player.Use(item);
            Assert.AreEqual(100, player.HP);
            player.Use(item);
            Assert.AreEqual(101, player.HP);
            Assert.AreEqual(101, player.MaxHP);
        }

        [Test]
        public void TestEyewashHerb() {
            var data = new string[] {
                "◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆",
                "◆　　　　　　罠　　　罠　　　　◆",
                "◆　　　　　　◇　◆　◇　◆　　◆",
                "◆　　　　　　罠　罠　罠　罠　　◆",
                "◆　　　試　　◆　◇　◆　◇　　◆",
                "◆マ　　眼　　　　罠　　　罠階　◆",
                "◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆"
            };
            floor = FloorMaker.Create(data);
            floor.Show();
            player = floor.Player;
            var item = ItemMaker.Create("EyewashHerb");
            player.Use(item);
            floor.Show();
        }
    }
}
