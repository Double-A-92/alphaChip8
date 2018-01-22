namespace AlphaChip8
{
    using System;

    public class InstructionDecoder
    {
        public delegate void ExecuteInstruction(CPU cpu);

        public ExecuteInstruction Decode(ushort instruction)
        {
            int firstNibble = (0xF000 & instruction) >> 12;
            switch (firstNibble)
            {
                case 0x0:
                    return this.Decode_0(instruction);

                case 0x1:
                case 0x2:
                case 0xA:
                case 0xB:
                    return this.Decode_NNN(instruction);

                case 0x3:
                case 0x4:
                case 0x6:
                case 0x7:
                case 0xC:
                    return this.Decode_XKK(instruction);

                case 0x5:
                case 0x8:
                case 0x9:
                case 0xD:
                    return this.Decode_XY(instruction);

                case 0xE:
                    break;

                case 0xF:
                    break;
            }

            return (cpu) => cpu.ADD(9, 4);
        }

        private ExecuteInstruction Decode_0(ushort instruction)
        {
            switch (instruction)
            {
                case 0x00E0:
                    return (cpu) => cpu.CLS();

                case 0x00EE:
                    return (cpu) => cpu.RET();

                default:
                    return this.Decode_NNN(instruction);
            }
        }

        private ExecuteInstruction Decode_8(int x, int y, byte lastNibble)
        {
            switch (lastNibble)
            {
                //case 0x0:
                //    return (cpu) => cpu.LD(x, y);
                //case 0x1:
                //    return (cpu) => cpu.OR(x, y);
                //case 0x2:
                //    return (cpu) => cpu.AND(x, y);
                //case 0x3:
                //    return (cpu) => cpu.XOR(x, y);
                //case 0x4:
                //    return (cpu) => cpu.ADD(x, y);
                //case 0x5:
                //    return (cpu) => cpu.SUB(x, y);
                //case 0x6:
                //    return (cpu) => cpu.SHR(x, y);
                //case 0x7:
                //    return (cpu) => cpu.SUBN(x, y);
                //case 0xE:
                //    return (cpu) => cpu.SHL(x, y);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private ExecuteInstruction Decode_NNN(ushort instruction)
        {
            ushort nnn = (ushort)(0x0FFF & instruction);

            int firstNibble = (0xF000 & instruction) >> 12;
            switch (firstNibble)
            {
                case 0x0:
                    return (cpu) => cpu.SYS(nnn);

                case 0x1:
                    return (cpu) => cpu.JP(nnn);

                case 0x2:
                    return (cpu) => cpu.CALL(nnn);

                //case 0xA:
                //    return (cpu) => cpu.LD_I(nnn);

                //case 0xB:
                //    return (cpu) => cpu.JP_V0(nnn);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private ExecuteInstruction Decode_XKK(ushort instruction)
        {
            int x = (instruction & 0x0F00) >> 8;
            byte kk = (byte)(instruction & 0xFF);

            int firstNibble = (0xF000 & instruction) >> 12;
            switch (firstNibble)
            {
                case 0x3:
                    return (cpu) => cpu.SE(x, kk);

                case 0x4:
                    return (cpu) => cpu.SNE(x, kk);

                case 0x6:
                    return (cpu) => cpu.LD(x, kk);

                //case 0x7:
                //    return (cpu) => cpu.ADD(x, kk);

                //case 0xC:
                //    return (cpu) => cpu.RND(x, kk);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private ExecuteInstruction Decode_XY(ushort instruction)
        {
            int x = (instruction & 0x0F00) >> 8;
            int y = (instruction & 0x00F0) >> 4;
            byte lastNibble = (byte)(0xF & instruction);

            int firstNibble = (0xF000 & instruction) >> 12;
            switch (firstNibble)
            {
                case 0x5:
                    return (cpu) => cpu.SE(x, y);

                case 0x8:
                    return this.Decode_8(x, y, lastNibble);

                //case 0x9:
                //    return (cpu) => cpu.SNE(x, y);

                //case 0xD:
                //    return (cpu) => cpu.DRW(x, y, lastNibble);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
