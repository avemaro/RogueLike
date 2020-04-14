using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ItemTests
    {
        [Test]
        public void CreateMedicinalHerb() {
            var floor = FloorMaker.Create();
            var player = floor.Player;

            var item = ItemMaker.Create("MedicinalHerb");
            player.HP -= 5;
            var hp = player.HP;
            player.Use(item);
            Assert.Greater(player.HP, hp);
        }
    }
}
