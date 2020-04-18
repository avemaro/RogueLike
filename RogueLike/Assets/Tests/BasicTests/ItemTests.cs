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
            Assert.AreEqual(96, player.HP);
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

        [Test]
        public void TestLifeHerb() {
            Assert.AreEqual(15, player.HP);
            Assert.AreEqual(15, player.MaxHP);
            var item = ItemMaker.Create("LifeHerb");
            player.Use(item);
            Assert.AreEqual(16, player.HP);
            Assert.AreEqual(20, player.MaxHP);
        }

        [Test]
        public void StomachEnlargingSeed() {
            Assert.AreEqual(100, player.Satiation);
            var item = ItemMaker.Create("StomachEnlargingSeed");
            player.Use(item);
            Assert.AreEqual(110, player.MaxSatiation);
        }

        [Test]
        public void WeaponTest() {
            Assert.AreEqual(5, player.AP);
            var item = ItemMaker.Create("SpikedClub");
            player.Equip(item);
            Assert.AreEqual(6, player.AP);
            item = ItemMaker.Create("Glaive");
            player.Equip(item);
            Assert.AreEqual(6, player.AP);
            item = ItemMaker.Create("Katana");
            player.Equip(item);
            Assert.AreEqual(7, player.AP);
        }

        [Test]
        public void KamaitachiTest() {
            player.Position = new Cell(4, 4);
            player.direction = Direction.up;
            var item = ItemMaker.Create(floor, new Cell(0, 0), "Kamaitachi");
            player.Equip(item);
            var enemy0 = Enemy.Create(floor, new Cell(3, 3), '武');
            var enemy1 = Enemy.Create(floor, new Cell(4, 3), '武');
            var enemy2 = Enemy.Create(floor, new Cell(5, 3), '武');
            Assert.False(enemy0.IsState(State.Dead));
            Assert.False(enemy1.IsState(State.Dead));
            Assert.False(enemy2.IsState(State.Dead));
            Debug.Log(player.direction);
            player.Attack();
            Assert.True(enemy0.IsState(State.Dead));
            Assert.True(enemy1.IsState(State.Dead));
            Assert.True(enemy2.IsState(State.Dead));
        }

        [Test]
        public void NotKamaitachiTest() {
            player.Position = new Cell(4, 4);
            player.direction = Direction.up;
            player.weapon = Weapon.Create(floor, player.Position, '拳');
            var enemy0 = Enemy.Create(floor, new Cell(3, 3), '武');
            var enemy1 = Enemy.Create(floor, new Cell(4, 3), '武');
            var enemy2 = Enemy.Create(floor, new Cell(5, 3), '武');
            Assert.False(enemy0.IsState(State.Dead));
            Assert.False(enemy1.IsState(State.Dead));
            Assert.False(enemy2.IsState(State.Dead));
            Debug.Log(player.AP);
            player.Attack();

            Debug.Log(enemy1.HP);
            Assert.False(enemy0.IsState(State.Dead));
            Assert.True(enemy1.IsState(State.Dead));
            Assert.False(enemy2.IsState(State.Dead));
        }
    }
}
