namespace AlphaChip8
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Display
    {
        /// <summary> Memory that holds the state of the monochrome display (64×32 pixels) </summary>
        private bool[,] displayMemory = new bool[64, 32];

        /// <summary> Reference to the 4K memory </summary>
        private byte[] memory;

        /// <summary> Counter of the delay timer </summary>
        private byte delayTimer = 0;

        /// <summary> Initializes a new instance of the <see cref="Display"/> class. </summary>
        /// <param name="memory">The system memory</param>
        public Display(byte[] memory)
        {
            this.memory = memory;
            this.LoadFontset();
        }

        /// <summary> Constructor needed for unit testing </summary>
        /// <param name="memory"> Mock 4K memory </param>
        /// <param name="displayMemory"> Mock display memory </param>
        public Display(byte[] memory, bool[,] displayMemory)
            : this(memory)
        {
            this.displayMemory = displayMemory;
        }

        /// <summary> Clears the display </summary>
        public void Clear()
        {
            Array.Clear(this.displayMemory, 0, this.displayMemory.Length);
        }

        /// <summary> Returns the location for the hexadecimal sprite corresponding to the value of digitValue. </summary>
        public ushort GetSpriteLocation(byte digitValue)
        {
            ushort baseAdress = 0;
            return (ushort)(baseAdress + ((digitValue * 5) % 80));
        }

        /// <summary> Loads the default Chip-8 fontset (digit sprites) into the 4k memory.</summary>
        /// <remarks>
        /// The data should be stored in the interpreter area of Chip-8 memory (0x000 to 0x1FF).
        /// This implementation chose the area from 0x00 to 0x4F.
        /// </remarks>
        private void LoadFontset()
        {
            byte[] fontset =
            {
              0xF0, 0x90, 0x90, 0x90, 0xF0, // 0
              0x20, 0x60, 0x20, 0x20, 0x70, // 1
              0xF0, 0x10, 0xF0, 0x80, 0xF0, // 2
              0xF0, 0x10, 0xF0, 0x10, 0xF0, // 3
              0x90, 0x90, 0xF0, 0x10, 0x10, // 4
              0xF0, 0x80, 0xF0, 0x10, 0xF0, // 5
              0xF0, 0x80, 0xF0, 0x90, 0xF0, // 6
              0xF0, 0x10, 0x20, 0x40, 0x40, // 7
              0xF0, 0x90, 0xF0, 0x90, 0xF0, // 8
              0xF0, 0x90, 0xF0, 0x10, 0xF0, // 9
              0xF0, 0x90, 0xF0, 0x90, 0x90, // A
              0xE0, 0x90, 0xE0, 0x90, 0xE0, // B
              0xF0, 0x80, 0x80, 0x80, 0xF0, // C
              0xE0, 0x90, 0x90, 0x90, 0xE0, // D
              0xF0, 0x80, 0xF0, 0x80, 0xF0, // E
              0xF0, 0x80, 0xF0, 0x80, 0x80, // F
            };

            Array.Copy(fontset, 0, this.memory, 0, fontset.Length);
        }

        // TODO: Dxyn - DRW Vx, Vy, nibble
    }
}
