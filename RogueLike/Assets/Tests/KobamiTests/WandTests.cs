using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests {
    public class WandTests {
        Floor floor;
        Player player;

        [SetUp]
        public void SetUp() {
            floor = FloorMaker.Create();
            player = floor.Player;
        }
    }
}
