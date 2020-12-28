window.itemDelScript = {
	delAjax(itemId) {

		swal.fire({
			title: 'Do you wish to Delete',
			icon: 'info',
			showCancelButton: true,
			showCloseButton: true,
			confirmButtonText: 'Yes!',
			allowOutsideClick: false,
			cancelButtonText: 'No',
			reverseButtons: true
		}).then((result) => {
			if (result.value) {

				//AJAX CALL TO DELETE ITEM
				$.ajax({
					url: "https://localhost:44317/api/Items/" + itemId,
					type: "DELETE",
					contentType: "application/json",
					success: function(result, status, xhr) {
						window.itemShowScript.showAllItems();
					},
					error: function(xhr, status, error) {
						console.log(xhr);
					}
				});
			}
		});
	}
}