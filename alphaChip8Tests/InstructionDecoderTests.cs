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
    public class InstructionDecoderTests
    {
        private InstructionDecoder cut;
        private Mock<CPU> cpuMock;

        [SetUp]
        public void Init()
        {
            cut = new InstructionDecoder();
            cpuMock = new Mock<CPU>();
        }

        [Test]
        public void Test_Decode_0nnn()
        {
            cpuMock.Setup(mock => mock.SYS(0xABC));

            var executor = cut.Decode(0x0ABC);
            executor(cpuMock.Object);

            cpuMock.VerifyAll();
        }

        [Test]
        public void Test_Decode_00E0()
        {
            cpuMock.Setup(mock => mock.CLS());

            var executor = cut.Decode(0x00E0);
            executor(cpuMock.Object);

            cpuMock.VerifyAll();
        }

        [Test]
        public void Test_Decode_00EE()
        {
            cpuMock.Setup(mock => mock.RET());

            var executor = cut.Decode(0x00EE);
            executor(cpuMock.Object);

            cpuMock.VerifyAll();
        }

        [Test]
        public void Test_Decode_1nnn()
        {
            cpuMock.Setup(mock => mock.JP(0xABC));

            var executor = cut.Decode(0x1ABC);
            executor(cpuMock.Object);

            cpuMock.VerifyAll();
        }

        [Test]
        public void Test_Decode_2nnn()
        {
            cpuMock.Setup(mock => mock.CALL(0xABC));

            var executor = cut.Decode(0x2ABC);
            executor(cpuMock.Object);

            cpuMock.VerifyAll();
        }

        [Test]
        public void Test_Decode_3xkk()
        {
            cpuMock.Setup(mock => mock.SE(0xA, (byte)0xBC));

            var executor = cut.Decode(0x3ABC);
            executor(cpuMock.Object);

            cpuMock.VerifyAll();
        }

        [Test]
        public void Test_Decode_4xkk()
        {
            cpuMock.Setup(mock => mock.SNE(0xA, (byte)0xBC));

            var executor = cut.Decode(0x4ABC);
            executor(cpuMock.Object);

            cpuMock.VerifyAll();
        }

        [Test]
        public void Test_Decode_5xy0()
        {
            cpuMock.Setup(mock => mock.SE(0xA, 0xB));

            var executor = cut.Decode(0x5AB0);
            executor(cpuMock.Object);

            cpuMock.VerifyAll();
        }

        [Test]
        public void Test_Decode_6xkk()
        {
            cpuMock.Setup(mock => mock.LD(0xA, (byte)0xBC));

            var executor = cut.Decode(0x6ABC);
            executor(cpuMock.Object);

            cpuMock.VerifyAll();
        }
    }
}