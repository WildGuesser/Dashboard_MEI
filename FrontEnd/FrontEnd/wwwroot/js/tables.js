// Wait for the DOM to be fully loaded
document.addEventListener("DOMContentLoaded", function () {
    // Get the table element
    var table = document.getElementById("myTable");

    // Get the search input element
    var searchInput = document.getElementById("search-input");

    // Get the column select element
    var columnSelect = document.getElementById("column-select");

    // Add an event listener to the search input element
    searchInput.addEventListener("keyup", function () {
        // Get the value of the search input
        var searchValue = this.value.toLowerCase();

        // Get the index of the selected column
        var columnIndex = columnSelect.selectedIndex;

        // Loop through the rows of the table
        for (var i = 0; i < table.rows.length; i++) {
            // Get the current row
            var row = table.rows[i];

            // Get the cell in the selected column of the current row
            var cell = row.cells[columnIndex];

            // Get the value of the cell
            var cellValue = cell.innerHTML.toLowerCase();

            // Check if the cell value contains the search value
            if (cellValue.indexOf(searchValue) > -1) {
                // Show the row if the cell value contains the search value
                row.style.display = "";
            } else {
                // Hide the row if the cell value does not contain the search value
                row.style.display = "none";
            }
        }
    });

    // Add an event listener to the column select element
    columnSelect.addEventListener("change", function () {
        // Trigger the keyup event on the search input to update the search results
        searchInput.dispatchEvent(new Event("keyup"));
    });
});