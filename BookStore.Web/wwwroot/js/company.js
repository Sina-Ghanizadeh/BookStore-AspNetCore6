
var dataTable;

$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Company/GetAll"
        },
        "columns": [

            { "data": "name", "width": "15%" },
            { "data": "streetAddress", "width": "15%" },
            { "data": "city", "width": "15%" },
            { "data": "state", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            {
                "data": "id", "render": function (data) {
                    return `

                        <div class="w-75 btn-group" role="group">
                            <a class="btn btn-warning mx-2" href="/Admin/Company/Edit?id=${data}"><i class="bi bi-pencil-square"></i> Edit</a>
                            <a class="btn btn-danger mx-2" onClick="Delete('/Admin/Company/Delete/${data}')"><i class="bi bi-trash"></i> Delete</a>

                        </div>
                    `
                }, "width": "15%"
            },



        ]

    });
}

function Delete(url) {

   
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            debugger;
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        Swal.fire(
                            'Deleted!',
                            data.message,
                            'success'
                        )
                        dataTable.ajax.reload();
                        
                    }
                    else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: data.message,
                        })
                    }
                }
            })
            
        }
    })

}