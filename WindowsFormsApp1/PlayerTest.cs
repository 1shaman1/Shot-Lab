using System.Drawing;
using NUnit.Framework;

namespace ShotLab
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void WeaponChange()
        {
            var expectedWeapon = new Weapon(500, 30, WeaponType.Knife);
            var player = new Player(100, new Point(0, 0));
            player.ChangeWeapon();
            Assert.AreEqual(expectedWeapon, player.CurrentWeapon);
        }
    }
}
