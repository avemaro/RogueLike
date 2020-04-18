using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ScrollTests
    {
        [Test]
        public void TestScrollOfConfusion() {
            var floor = FloorMaker.Create();
            var player = floor.Player;
            var room = floor.GetRoom(player.Position);
            var item = ItemMaker.Create("ScrollOfConfusion");
            foreach (var enemy in floor.GetEnemies(room))
                Assert.False(enemy.IsState(State.Confusion));
            player.Use(item);
            foreach (var enemy in floor.GetEnemies(room))
                Assert.True(enemy.IsState(State.Confusion));
        }
    }
}
