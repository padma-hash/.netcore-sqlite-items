itemUpdateScript = {
	updateAjax(itemId) {
		let baseUri = window.itemUpdateScript.getBaseURI();
		const htmlSwal = 'Item Name:&nbsp;<input id="updateName" type="text"><br/><br/>' +
			'Item Price:&nbsp;&nbsp;<input id="updatePrice" type="text">';
		swal.fire({
			showCancelButton: true,
			title: 'Update Items',
			html: htmlSwal,
			showCloseButton: true,
			showCancelButton: true,
			icon: 'info',
			preConfirm: function() {
				 in1 = $('#updateName').val();
				in2 = $('#updatePrice').val();
			},
			onOpen: function () {
				$('#updateName').focus();
			}
		}).then((result) => {
			if (result.value && result.dismiss != 'cancel') {
				const myId = itemId;
				console.log(result);
				$.ajax({
					url: baseUri + "Home/UpdateItem",
					type: "POST",
					data: {
						ItemId: myId,
						ItemName: in1,
						itemPrice: in2
					},

					success: function(result1) {
						if (result1.success === true) {
							Swal.fire("Saved", "Added to API", "success");
							window.itemShowScript.showAllItems();
						} else {
							Swal.fire(result1.responseText, "Retry Again - API refused to save", "error");
						}
					},
					error: function(result1) {
						Swal.fire("Error", "Could not cannot to API", "error");

					}
				});
			}


		});

			
	},
	getBaseURI: function () {
		let baseUrl = window.location.origin + window.location.pathname;
		//console.log("HI" + baseUrl);
		if (baseUrl.includes("https://localhost"))
			baseUrl = "/";
		return baseUrl;
	}

}

