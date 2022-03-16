
var dataTable;

$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAll"
        },
        "columns": [

            { "data": "title", "width": "15%" },
            { "data": "isbn", "width": "15%" },
            { "data": "price", "width": "15%" },
            { "data": "author", "width": "15%" },
            { "data": "category", "width": "15%" },
            { "data": "coverType", "width": "15%" },
            {
                "data": "id", "render": function (data) {
                    return `

                        <div class="w-75 btn-group" role="group">
                            <a class="btn btn-warning mx-2" href="/Admin/Product/Edit?id=${data}"><i class="bi bi-pencil-square"></i> Edit</a>
                            <a class="btn btn-danger mx-2" onClick="Delete('/Admin/Product/Delete/${data}')"><i class="bi bi-trash"></i> Delete</a>

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