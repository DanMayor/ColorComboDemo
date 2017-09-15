using System.Collections.Generic;

namespace ColorComboDemo.Models {
    /// <summary>
    ///     ChipSequence represents the current sequence of chips that the
    ///     system is processing
    /// </summary>
    public class ChipSequence {
        /// <summary>
        ///     SuccessChips is the list of successfully linked chips thus far
        /// </summary>
        public List<SecurityChip> SuccessChips { get; set; }

        /// <summary>
        ///     RemainingChips is the list of chips left to match
        /// </summary>
        public List<SecurityChip> RemainingChips { get; set; }

        /// <summary>
        ///     PreviousColor is the "left" side of the last success chip
        /// </summary>
        public string PreviousColor { get; set; }

        /// <summary>
        ///     NextColor is the "right" side of the last success chip
        /// </summary>
        public string NextColor { get; set; }

        /// <summary>
        ///     PreviousID is the unique ID of the chip BEFORE the last success chip (for fall back when first selected chip doesn't work)
        /// </summary>
        public int PreviousID { get; set; }

        /// <summary>
        ///     NextID is the unique ID of the chip AFTER the last success chip (for fall back when first selected chip doesn't work)
        /// </summary>
        public int NextID { get; set; }

        /// <summary>
        ///     Default constructor
        /// </summary>
        /// <param name="successChips">The list of success chips before this sequence</param>
        /// <param name="remainingChips">The list of remaining chips to match</param>
        public ChipSequence(List<SecurityChip> successChips, List<SecurityChip> remainingChips) {
            SuccessChips = successChips;
            RemainingChips = remainingChips;
        }
    }
}
