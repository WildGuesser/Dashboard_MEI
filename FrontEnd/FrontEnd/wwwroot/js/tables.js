
//Procurar por elemento
//document.addEventListener("DOMContentLoaded", function () {
//    var table = document.getElementById("myTable");
//    var searchInput = document.getElementById("search-input");
//    var columnSelect = document.getElementById("column-select");

//    searchInput.addEventListener("keyup", function () {
//        var searchValue = this.value.toLowerCase();
//        var columnIndex = columnSelect.selectedIndex;

//        for (var i = 1; i < table.rows.length; i++) {
//            var row = table.rows[i];

//                var cell = row.cells[columnIndex];
//                var cellValue = cell.innerHTML.toLowerCase();

//                if (cellValue.indexOf(searchValue) > -1) {
//                    row.style.display = "";
//                } else {
//                    row.style.display = "none";
//                }

//        }
//    });

//    columnSelect.addEventListener("change", function () {
//        searchInput.dispatchEvent(new Event("keyup"));
//    });
//});

//Procura global
document.addEventListener("DOMContentLoaded", function () {
    var table = document.getElementById("myTable");
    var searchInput = document.getElementById("search-input");
    var columnSelect = document.getElementById("column-select");

    searchInput.addEventListener("keyup", function () {
        var searchValue = this.value.toLowerCase();

        for (var i = 1; i < table.rows.length; i++) {
            var row = table.rows[i];
            var rowText = "";

            for (var j = 0; j < row.cells.length; j++) {
                rowText += row.cells[j].textContent.toLowerCase();
            }

            if (rowText.includes(searchValue)) {
                row.style.display = "";
            } else {
                row.style.display = "none";
            }
        }
    });
});

//Comfimação de apagar multiplas entradas
document.addEventListener('DOMContentLoaded', function () {
    var deleteBtn = document.getElementById('delete-btn');

    deleteBtn.addEventListener('click', function (e) {
        e.preventDefault();

        if (confirm("Are you sure you want to delete the selected items?")) {
            document.getElementById('delete-form').submit();
        }
    });
});

//Selecionar todas as entradas
document.addEventListener('DOMContentLoaded', function () {
    var deleteBtn = document.getElementById('select-all-checkbox');
    var cboxes = document.getElementsByClassName('select-checkbox');

    deleteBtn.addEventListener('click', function () {
        for (var i = 0; i < cboxes.length; i++) {
            cboxes[i].checked = deleteBtn.checked;
        }
    });
});