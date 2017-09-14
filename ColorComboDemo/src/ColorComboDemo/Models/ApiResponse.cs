namespace ColorComboDemo.Models {
    /// <summary>
    ///     ApiResponse is just a simple object we use to respond to the
    ///     interfaces validation requests.
    /// </summary>
    public class ApiResponse {
        /// <summary>
        ///     Success defines if the chips can be used to unlock the system
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        ///     Message is the raw response per output requirements
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Basic constructor
        /// </summary>
        /// <param name="success">Set if this response indicates successful validation</param>
        /// <param name="message">The response output per requirements</param>
        public ApiResponse(bool success, string message) {
            Success = success;
            Message = message;
        }
    }
}
