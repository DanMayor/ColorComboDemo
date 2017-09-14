namespace ColorComboDemo.Models {
    /// <summary>
    ///     SecurityChip represents one of the family members' security chips
    /// </summary>
    public class SecurityChip {
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
            string[] colors = definition.Split('|');
            ColorA = colors[0];
            ColorB = colors[1];
            ChipID = chipID;
        }
    }
}
