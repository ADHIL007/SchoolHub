$(document).ready(function () {

    $("#addRowBtn").click(function () {
        var lastRow = $("#qualificationsBody tr:last");
        var newRow = lastRow.clone();

        newRow.find("input").val("");

        $("#qualificationsBody").append(newRow);
        reindexRows();
    });

    $("#qualificationsBody").on("click", ".removeRowBtn", function () {
        var rowCount = $("#qualificationsBody tr").length;

        if (rowCount > 1) {
            $(this).closest("tr").remove();
            reindexRows();
        } else {
            $(this).closest("tr").find("input").val("");
        }
    });

    function reindexRows() {
        $("#qualificationsBody tr").each(function (index) {
            console.log("reindexing row " + index);
            $(this).find("input").each(function () {
                var name = $(this).attr("name");
                if (name) {
                    // var newName = name.replace("Qualifications[0]", "Qualifications[" + index + "]");
                    var newName = name.replace(/Qualifications\[\d+\]/, "Qualifications[" + index + "]");
                    $(this).attr("name", newName);
                }
            });
        });
    }
});
