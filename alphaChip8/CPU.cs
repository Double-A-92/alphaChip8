// http://www.multigesture.net/articles/how-to-write-an-emulator-chip-8-interpreter/
// http://devernay.free.fr/hacks/chip8/C8TECH10.HTM#00E0
namespace AlphaChip8
{
    public class CPU
    {
        /// <summary> 4K memory </summary>
        private byte[] memory = new byte[4096];

        /// <summary> Data registers V0 to VF </summary>
        private byte[] registers = new byte[16];

        /// <summary> Memory address register I </summary>
        private ushort index = 0;

        /// <summary> Program counter </summary>
        private ushort programCounter = 0;

        /// <summary> Stack used to remember current location before performing a jump </summary>
        private ushort[] stack = new ushort[16];

        /// <summary> Stack pointer </summary>
        private ushort stackPointer = 0;

        /// <summary> Counter of the sound timer </summary>
        private byte soundTimer = 0;

        /// <summary> Instance of the Display </summary>
        private Display display;

        /// <summary> Initializes a new instance of the <see cref="CPU"/> class. </summary>
        public CPU()
        {
            this.display = new Display(this.memory);
        }

        /// <summary> Enables to access the memory externally. </summary>
        /// <returns>The current 4K of memory</returns>
        public byte[] GetMemory()
        {
            return this.memory;
        }

        #region Instructions

        /// <summary>
        /// 0nnn - SYS addr
        /// Jump to a machine code routine at nnn.
        /// </summary>
        /// <param name="address">nnn</param>
        public virtual void SYS(ushort address)
        {
            // This instruction is only used on the old computers on which Chip-8 was originally implemented.
        }

        /// <summary>
        /// 00E0 - CLS
        /// Clear the display.
        /// </summary>
        public virtual void CLS()
        {
            this.display.Clear();
        }

        /// <summary>
        /// 00EE - RET
        /// Return from a subroutine.
        /// </summary>
        public virtual void RET()
        {
            this.programCounter = this.stack[this.stackPointer];
            this.stackPointer--;
        }

        /// <summary>
        /// 1nnn - JP addr
        /// Jump to location nnn. Sets the program counter to nnn.
        /// </summary>
        /// <param name="address">nnn</param>
        public virtual void JP(ushort address)
        {
            this.programCounter = address;
        }

        /// <summary>
        /// 2nnn - CALL addr
        /// Call subroutine at nnn.
        /// </summary>
        /// <param name="address">nnn</param>
        public virtual void CALL(ushort address)
        {
            this.stackPointer++;
            this.stack[this.stackPointer] = this.programCounter;
            this.programCounter = address;
        }

        /// <summary>
        /// 3xkk - SE Vx, byte
        /// Skip next instruction if Vx = kk.
        /// </summary>
        /// <param name="registerIndex">V register to compare (x)</param>
        /// <param name="value">value to compare (kk)</param>
        public virtual void SE(int registerIndex, byte value)
        {
            if (this.registers[registerIndex] == value)
            {
                this.programCounter += 2;
            }
        }

        /// <summary>
        /// 4xkk - SNE Vx, byte
        /// Skip next instruction if Vx != kk.
        /// </summary>
        /// <param name="registerIndex">V register to compare (x)</param>
        /// <param name="value">value to compare (kk)</param>
        public virtual void SNE(int registerIndex, byte value)
        {
            if (this.registers[registerIndex] != value)
            {
                this.programCounter += 2;
            }
        }

        /// <summary>
        /// 5xy0 - SE Vx, Vy
        /// Skip next instruction if Vx = Vy.
        /// </summary>
        /// <param name="registerIndex1">First V register to compare (x)</param>
        /// <param name="registerIndex2">Second V register to compare (y)</param>
        public virtual void SE(int registerIndex1, int registerIndex2)
        {
            if (this.registers[registerIndex1] == this.registers[registerIndex2])
            {
                this.programCounter += 2;
            }
        }

        /// <summary>
        /// 6xkk - LD Vx, byte
        /// Puts the value kk into register Vx.
        /// </summary>
        /// <param name="registerIndex">V register (x)</param>
        /// <param name="value">value to load (kk)</param>
        public virtual void LD(int registerIndex, byte value)
        {
            this.registers[registerIndex] = value;
        }

        /// <summary>
        /// 7xkk - ADD Vx, byte
        /// Adds the value kk to the value of register Vx, then stores the result in Vx.
        /// </summary>
        /// <param name="registerIndex">V register (x)</param>
        /// <param name="value">value to load (kk)</param>
        public virtual void ADD(int registerIndex, byte value)
        {
            this.registers[registerIndex] += value;
        }

        /// <summary>
        /// Fx29 - LD F, Vx
        /// The value of I is set to the location for the hexadecimal sprite corresponding to the value of Vx.
        /// </summary>
        /// <param name="registerIndex">V register (x)</param>
        public virtual void LD_F(int registerIndex)
        {
            this.index = this.display.GetSpriteLocation(this.registers[registerIndex]);

            // TODO: Add this to InstructionDecoder
        }

        // TODO: Implement all the other instructions
        #endregion
    }
}
