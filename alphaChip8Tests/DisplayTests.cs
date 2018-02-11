using NUnit.Framework;
using AlphaChip8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace AlphaChip8.Tests
{
    [TestFixture]
    public class DisplayTests
    {
        private Display cut;
        private bool[,] displayMemoryMock;
        private byte[] memoryMock;

        [SetUp]
        public void Init()
        {
            displayMemoryMock = new bool[64, 32];
            memoryMock = new byte[4096];
            cut = new Display(memoryMock, displayMemoryMock);
        }

        [Test]
        public void Test_Clear()
        {
            var random = new Random();
            int i = random.Next(0, 63);
            int j = random.Next(0, 31);
            displayMemoryMock[i, j] = true;
            cut.Clear();
            Assert.IsFalse(displayMemoryMock[i, j]);
        }

        [Test]
        public void Test_Fontset()
        {
            // Check only first and last byte
            Assert.AreEqual(0xF0, memoryMock[0]);
            Assert.AreEqual(0x80, memoryMock[79]);
        }

        [Test]
        public void Test_SpriteLocation()
        {
            // Check only first and last byte
            Assert.AreEqual(15, cut.GetSpriteLocation(0x3));
            Assert.AreEqual(70, cut.GetSpriteLocation(0xE));
        }
    }
}
