/*
    Main.js serves as our client side code. This code was written to lightly emulate a client
    side controller system.
*/

// Once jQuery signals document ready
$(document).ready(function () {

    // Although I'm having issue implementing Angular2, I wanted to demonstrate
    // an understanding of the basic concepts of the client side controller.
    var InputController = {
        // AddChipButton click handler
        AddChip: function () {
            $("#AddChipModal").modal("show");
        },

        // ClearStringButton click handler
        ClearInput: function () {
            $("#ConfirmClearModal").modal("show");
        },

        // ValidateInputButton click handler
        ValidateInput: function () {
            $("#OutputString").html("");
            $("#AddChipButton").attr("disabled", "disabled");
            $("#ClearStringButton").attr("disabled", "disabled");
            $("#ValidateInputButton").attr("disabled", "disabled");

            $("#OutputStatusMessage").html("Validating input...")
                .removeClass("text-danger")
                .removeClass("text-success")
                .addClass("text-primary");

            var inputString = $("#InputString").val();
            $.ajax({
                url: "/Home/ValidateChips",
                method: "POST",
                dataType: "JSON",
                data: {
                    chipCodes: inputString
                },

                success: function (data) {
                    console.log(data);

                    if (data.success === true) {
                        $("#OutputString").removeClass("text-danger").addClass("text-success");
                        $("#OutputStatusMessage").html("Chips Validated, System Unlocked!")
                            .removeClass("text-danger")
                            .removeClass("text-primary")
                            .addClass("text-success");
                    } else {
                        $("#OutputString").removeClass("text-success").addClass("text-danger");
                        $("#OutputStatusMessage").html("Chips are invalid, engaging counter measures!")
                            .removeClass("text-primary")
                            .removeClass("text-success")
                            .addClass("text-danger");
                    }

                    $("#OutputString").html(data.message);

                    $("#AddChipButton").removeAttr("disabled");
                    $("#ClearStringButton").removeAttr("disabled");
                    $("#ValidateInputButton").removeAttr("disabled");
                },

                error: function (jqXhr, textStatus, errorMessage) {
                    console.lof(jqXhr);
                    alert(errorMessage);
                }
            });
        }
    }

    // Controller for the Confirm Clear String modal
    var ConfirmClearModalController = {
        // ConfirmClearStringButton click handler
        Confirm: function () {
            $("#InputString").val("");
            $("#ConfirmClearModal").modal("hide");
        },

        // CancelClearStringButton click handler
        Cancel: function () {
            $("#ConfirmClearModal").modal("hide");
        }
    }

    // Controller for the Add Chip modal
    var AddChipModalController = {
        // ConfirmAddButton click handler
        Confirm: function () {
            $("#AddChipErrorMessage").html("");

            var firstColor = $("#AddChipFirstColor").val();
            var secondColor = $("#AddChipSecondColor").val();

            if (!firstColor || firstColor === "") {
                $("#AddChipErrorMessage").html("You must provide the first color of the chip to proceed");
                $("#AddChipFirstColor").focus();
                return;
            }

            if (!secondColor || secondColor === "") {
                $("#AddChipErrorMessage").html("You must provide the second color of the chip to proceed");
                $("#AddChipSecondColor").focus();
                return;
            }

            $("#InputString").val(
                $("#InputString").val() +
                firstColor + " " + secondColor +
                "\n"
            );

            $("#AddChipModal").modal("hide");

            $("#AddChipFirstColor").val("");
            $("#AddChipSecondColor").val("");
        },

        // CancelAddButton click handler
        Cancel: function () {
            $("#AddChipModal").modal("hide");
        }
    }

    // These are our basic routes for the input editor
    $("#AddChipButton").on("click", InputController.AddChip);
    $("#ClearStringButton").on("click", InputController.ClearInput);
    $("#ValidateInputButton").on("click", InputController.ValidateInput);

    // These are our basic routes for the confirm clear modal controller
    $("#ConfirmClearStringButton").on("click", ConfirmClearModalController.Confirm);
    $("#CancelClearStringButton").on("click", ConfirmClearModalController.Cancel);

    // These are our basic routes for the add chip modal controller
    $("#ConfirmAddButton").on("click", AddChipModalController.Confirm);
    $("#CancelAddButton").on("click", AddChipModalController.Cancel);
});
