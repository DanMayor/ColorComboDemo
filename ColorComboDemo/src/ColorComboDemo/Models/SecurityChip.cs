using System;

namespace ColorComboDemo.Models {
    /// <summary>
    ///     SecurityChip represents one of the family members' security chips
    /// </summary>
    public class SecurityChip {
        /// <summary>
        ///     ChipID is the unique id of this chip
        /// </summary>
        public int ChipID { get; set; }

        /// <summary>
        ///     ColorA is one of the two colors on the chip
        /// </summary>
        public string ColorA { get; set; }

        /// <summary>
        ///     ColorB is the other of the two colors on the chip
        /// </summary>
        public string ColorB { get; set; }

        /// <summary>
        ///     Basic constructor
        /// </summary>
        /// <param name="chipID">The unique ID of this security chip (auto incremented value when parsing the input string)</param>
        /// <param name="definition">The input line of this chip</param>
        public SecurityChip(int chipID, string definition) {
            string[] colors = definition.Split(' ');
            ColorA = colors[0];
            ColorB = colors[1];
            ChipID = chipID;
        }

        /// <summary>
        ///     Matches simply compares this chip to the provided previous color to see if it can appear next in sequence
        /// </summary>
        /// <param name="previousColor">The previous color which must be on at least one side of this chip</param>
        /// <returns>Is this a potential match in the sequence?</returns>
        public bool Matches(string previousColor) {
            if (ColorA.Equals(previousColor, StringComparison.OrdinalIgnoreCase) || ColorB.Equals(previousColor, StringComparison.OrdinalIgnoreCase)) {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Get next color returns ColorA or ColorB, whichever one does NOT match previousColor
        /// </summary>
        /// <param name="previousColor">The previous color which must be on at least one side of this chip</param>
        /// <returns>The opposing color</returns>
        public string GetNextColor(string previousColor) {
            if (!ColorA.Equals(previousColor, StringComparison.OrdinalIgnoreCase)) {
                return ColorA;
            }

            return ColorB;
        }
    }
}
