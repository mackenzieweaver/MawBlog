// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('.myTables').DataTable();
});


function ToggleEdit(comment) {
    var visible = document.getElementById('editForm-' + comment);
    if (visible.classList.contains('editForm')) {
        visible.classList.remove('editForm');
    } else {
        visible.classList.add('editForm');
    }
}