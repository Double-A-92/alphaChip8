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

        /// <summary> Counter of the delay timer </summary>
        private byte delayTimer = 0;

        /// <summary> Clears the display </summary>
        public void Clear()
        {
            Array.Clear(this.displayMemory, 0, this.displayMemory.Length);
        }

        // TODO: Programs may also refer to a group of sprites representing the hexadecimal digits 0 through F. 
        // These sprites are 5 bytes long, or 8x5 pixels. The data should be stored in the interpreter area of Chip-8 memory (0x000 to 0x1FF).
        //  Fx29 - LD F, Vx
        //  Dxyn - DRW Vx, Vy, nibble
    }
}
