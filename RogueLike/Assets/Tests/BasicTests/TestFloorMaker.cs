using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestFloorMaker
    {
        [Test]
        public void MakeFloorWithOneRoom() {
            var floor = FloorMaker.Create();
            floor.Show();
        }

        [Test]
        public void MakeFloorWithTwoRoom() {
            var floor = FloorMaker.Create();
            floor.Show();
        }

        [Test]
        public void MakeFloorWithThreeRoom() {
            var floor = FloorMaker.Create();
            floor.Show();
        }

        [Test]
        public void MakeFloorWithFourRoom() {
            var floor = FloorMaker.Create();
            floor.Show();
        }
    }
}
