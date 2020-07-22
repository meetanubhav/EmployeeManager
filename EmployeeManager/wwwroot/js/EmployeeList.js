﻿var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#EmployeeDataTable').DataTable({
        "ajax": {
            "url": "/Employee/getall/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name" },
            { "data": "department" },
            { "data": "location" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/Employee/Update?id=${data}" class='btn btn-success text-white'>
                            Edit
                        </a>
                        &nbsp;
                        <a class='btn btn-danger text-white'
                            onclick=Delete('/Employee/Delete?id='+${data})>
                            Delete
                        </a>
                        </div>`;
                }, "width": "40%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}