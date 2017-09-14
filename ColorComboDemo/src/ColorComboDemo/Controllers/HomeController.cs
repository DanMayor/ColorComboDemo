using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            if (chipCodes.Length <= 0) {
                return Json(new ApiResponse(false, "Cannot unlock master panel: Must provide valid chip definitions"));
            }

            // Test the format, which in our case is "color a|color b\n..."
            if (!chipCodes.Contains("|") || !chipCodes.Contains("\n")) {
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
                string message = $"{endChip.ColorA} | ";
                foreach (SecurityChip chip in successCombination) {
                    message += $"{chip.ColorA} - {chip.ColorB} | ";
                }

                message += endChip.ColorB;

                return Json(new ApiResponse(true, message));
            }

            return Json(new ApiResponse(false, "Cannot unlock master panel: Invalid chip definitions"));
        }

        protected List<SecurityChip> validateSecurityChips(SecurityChip endChip, List<SecurityChip> chips) {
            return null;
        }
    }
}
