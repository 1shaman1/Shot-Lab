using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using NUnit.Framework;

namespace ShotLab
{
    [TestFixture]
    public class PropsTests
    {
        [Test]
        public void PropsDeath()
        {
            var prop = new Prop() { Health = 0, Position = new Point(1, 1) };
            Assert.IsTrue(prop.IsDead());
        }
    }
}
