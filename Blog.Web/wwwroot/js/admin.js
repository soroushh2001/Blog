function toggleDeleteCategory(id, categoryTitle, deleteStatus) {

    let title;
    if (deleteStatus === true) {
        title = 'بازگردانی';

    } else {
        title = 'حذف';
    }

    Swal.fire({
        title: `آیا از ${title} ${categoryTitle} مطمئن هستید؟`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "بله",
        cancelButtonText: "لغو"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/api/admin/ToggleDeleteCategory",
                type: "post",
                data: {
                    id: id
                },
                beforeSend: function () {
                    startWaiting();
                },
                success: function (response) {
                    closeWaiting();
                    if (response.status === 200) {
                        toastr.success('عملیات با موفقیت انجام شد');
                        $("#categories-div").load(location.href + " #categories-div");
                    } else {
                        toastr.error('response.message');
                    }
                },
                error: function () {
                    closeWaiting();
                }
            });
        }
    });
}


function toggleDeletePost(id, postTitle, deleteStatus) {

    let title;
    if (deleteStatus === true) {
        title = 'بازگردانی';

    } else {
        title = 'حذف';
    }

    Swal.fire({
        title: `آیا از ${title} ${postTitle} مطمئن هستید؟`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "بله",
        cancelButtonText: "لغو"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/api/admin/ToggleDeletePost",
                type: "post",
                data: {
                    id: id
                },
                beforeSend: function () {
                    startWaiting();
                },
                success: function (response) {
                    closeWaiting();
                    if (response.status === 200) {
                        toastr.success('عملیات با موفقیت انجام شد');
                        $("#posts-div").load(location.href + " #posts-div");
                    } else {
                        toastr.error('response.message');
                    }
                },
                error: function () {
                    closeWaiting();
                }
            });
        }
    });
}

function deleteRole(id, roleTitle) {

    let title = "حذف";
    Swal.fire({
        title: `آیا از ${title} ${roleTitle} مطمئن هستید؟`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "بله",
        cancelButtonText: "لغو"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/api/admin/DeleteRole",
                type: "post",
                data: {
                    id: id
                },
                beforeSend: function () {
                    startWaiting();
                },
                success: function (response) {
                    closeWaiting();
                    if (response.status === 200) {
                        toastr.success('عملیات با موفقیت انجام شد');
                        $("#posts-div").load(location.href + " #roles-div");
                    } else {
                        toastr.error('response.message');
                    }
                },
                error: function () {
                    closeWaiting();
                }
            });
        }
    });
}

function toggleUserActivationStatus(id, userName, deleteStatus) {

    let title;
    if (deleteStatus === true) {
        title = 'فعال سازی';

    } else {
        title = 'غیر فعالسازی';
    }

    Swal.fire({
        title: `آیا از ${title} ${userName} مطمئن هستید؟`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "بله",
        cancelButtonText: "لغو"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/api/admin/ToggleUserActivationStatus",
                type: "post",
                data: {
                    id: id
                },
                beforeSend: function () {
                    startWaiting();
                },
                success: function (response) {
                    closeWaiting();
                    if (response.status === 200) {
                        toastr.success('عملیات با موفقیت انجام شد');
                        $("#users-div").load(location.href + " #users-div");
                    } else {
                        toastr.error('response.message');
                    }
                },
                error: function () {
                    closeWaiting();
                }
            });
        }
    });
}

function toggleUserBanStatus(id, userName, deleteStatus) {

    let title;
    if (deleteStatus === true) {
        title = 'رفع مسدودیت';

    } else {
        title = 'مسدود کردن';
    }

    Swal.fire({
        title: `آیا از ${title} ${userName} مطمئن هستید؟`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "بله",
        cancelButtonText: "لغو"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/api/admin/ToggleUserBanStatus",
                type: "post",
                data: {
                    id: id
                },
                beforeSend: function () {
                    startWaiting();
                },
                success: function (response) {
                    closeWaiting();
                    if (response.status === 200) {
                        toastr.success('عملیات با موفقیت انجام شد');
                        $("#users-div").load(location.href + " #users-div");
                    } else {
                        toastr.error('response.message');
                    }
                },
                error: function () {
                    closeWaiting();
                }
            });
        }
    });
}