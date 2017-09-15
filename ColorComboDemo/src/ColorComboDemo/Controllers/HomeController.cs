using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using ColorComboDemo.Models;

namespace ColorComboDemo.Controllers
{
    public class HomeController : Controller {
        /// <summary>
        ///     Index renders the SPA interface to the browser
        /// </summary>
        /// <returns>ViewResult</returns>
        public IActionResult Index() {
            return View();
        }

        /// <summary>
        ///     ValidateChips is intended to be our makeshift API call from the client. For
        ///     the interests of brevity in this sample application, we will simply handle
        ///     an HTTP GET request and return a Json encoded model. In a real scenario, I
        ///     would leverage the .NET data model annotations and validations.
        /// </summary>
        /// <param name="chipCodes">The input string defined in requirements ("color a|color b\n...")</param>
        /// <returns></returns>
        public IActionResult ValidateChips(string chipCodes) {
            // Test the input string length
            if (string.IsNullOrEmpty(chipCodes)) {
                return Json(new ApiResponse(false, "Cannot unlock master panel: Must provide valid chip definitions"));
            }

            // Test the format, which in our case is "color a|color b\n..."
            chipCodes = chipCodes.Trim();
            if (!chipCodes.Contains(" ") || !chipCodes.Contains("\n")) {
                return Json(new ApiResponse(false, "Cannot unlock master panel: Invalid chip definition format"));
            }

            // Split out the input into an array of strings to work with. In a real scenario
            // I would implement more thorough line handling using Environment.NewLine to account
            // for easy integration with third party input sources
            string[] codeLines = chipCodes.Split('\n');
            if (codeLines.Count() <= 1) {
                return Json(new ApiResponse(false, "Cannot unlock master panel: Incorrect number of chips provided"));
            }

            // Here we create the chips input list that we will use to test the combinations
            List<SecurityChip> chips = new List<SecurityChip>();
            int chipID = 1;
            foreach (string chipLine in codeLines) {
                chips.Add(new SecurityChip(chipID, chipLine));
                chipID++;
            }

            // Here we pop the endChip off the front of our chips list
            SecurityChip endChip = chips[0];
            chips.RemoveAt(0);

            // Here we test the chips list against the end chip to validate the provided chips
            // can unlock our security system
            List<SecurityChip> successCombination = validateSecurityChips(endChip, chips);
            if (successCombination != null) {
                string message = $"{endChip.ColorA}\n ";
                foreach (SecurityChip chip in successCombination) {
                    message += $"{chip.ColorA} {chip.ColorB}\n";
                }

                message += endChip.ColorB;

                return Json(new ApiResponse(true, message.Replace("\n ", "\n")));
            }

            return Json(new ApiResponse(false, "Cannot unlock master panel: Invalid chip definitions"));
        }

        /// <summary>
        ///     validateSecurityChips is used to iterate over the provided chips to determine
        ///     if they can unlock the master panel
        /// </summary>
        /// <param name="endChip">The first chip provided, which defines the start and end colors</param>
        /// <param name="chips">The remaining chips that should connect to unlock the panel</param>
        /// <returns>The sequence of "family" chips in order to unlock the panel</returns>
        protected List<SecurityChip> validateSecurityChips(SecurityChip endChip, List<SecurityChip> chips) {
            string previousColor = endChip.ColorA;
            SecurityChip nextChip = chips.FirstOrDefault(chip => chip.Matches(previousColor));

            if (nextChip != null && nextChip.ChipID > 0) {
                chips.Remove(nextChip);

                ChipSequence sequence = new ChipSequence(new List<SecurityChip> { nextChip }, chips);
                sequence.PreviousColor = previousColor;
                sequence.PreviousID = 0;

                sequence.NextColor = nextChip.GetNextColor(previousColor);
                sequence.NextID = 0;

                ChipSequence successSequence = validateSequence(sequence);
                if (successSequence != null) {
                    return successSequence.SuccessChips;
                }
            }

            return null;
        }

        /// <summary>
        ///     validateSequence is used to step through the provided chips and make the sequential matches.
        /// </summary>
        /// <param name="sequence">The sequence to validate</param>
        /// <returns>If valid, the sequence defining the successful series of chips to render output string from</returns>
        protected ChipSequence validateSequence(ChipSequence sequence) {
            string previousColor = sequence.NextColor;
            SecurityChip nextChip = sequence.RemainingChips.FirstOrDefault(chip => chip.Matches(previousColor));

            if (nextChip != null && nextChip.ChipID > 0) {
                List<SecurityChip> remainingChips = sequence.RemainingChips;
                remainingChips.Remove(nextChip);

                List<SecurityChip> successChips = sequence.SuccessChips;
                successChips.Add(nextChip);

                ChipSequence nextSequence = new ChipSequence(successChips, remainingChips);
                nextSequence.PreviousColor = previousColor;
                nextSequence.PreviousID = sequence.SuccessChips.Last().ChipID;
                nextSequence.NextColor = nextChip.GetNextColor(previousColor);

                sequence.NextID = nextChip.ChipID;

                // if no chips remain here, all have been successfully matched
                if (nextSequence.RemainingChips.Count() <= 0) {
                    return nextSequence;
                }

                // Otherwise we step into the remaining chips, if sequenceResult is not null
                // it means we have a full success sequence, we'll return it
                ChipSequence sequenceResult = validateSequence(nextSequence);
                if (sequenceResult != null) {
                    return sequenceResult;
                }

                // Finally, we'll try to step back one and try again with a different "nextChip"
                // TODO: This :(
            }

            return null;
        }
    }
}
