$(document).ready(function () {

    $("#addRowBtn").click(function () {
        var lastRow = $("#qualificationsBody tr:last");
        var newRow = lastRow.clone();

        newRow.find("input").val("");
        newRow.find("span[data-valmsg-for]").empty();

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
            $(this).find("input").each(function () {
                var name = $(this).attr("name");
                if (name) {
                    var newName = name.replace(/Qualifications\[\d+\]/, "Qualifications[" + index + "]");
                    $(this).attr("name", newName);
                }
            });

            $(this).find("span[data-valmsg-for]").each(function () {
                var target = $(this).attr("data-valmsg-for");
                if (target) {
                    var newTarget = target.replace(/Qualifications\[\d+\]/, "Qualifications[" + index + "]");
                    $(this).attr("data-valmsg-for", newTarget);
                }
            });
        });
    }
});
