$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/post/getall' },
        "columns": [
            { data: 'title', "width": "35%" },
            { data: 'description', "width": "40%" },
            { data: 'category.name', "width": "25%" },
            { data: 'Image', "width": "25%" }
        ]
    });
}
