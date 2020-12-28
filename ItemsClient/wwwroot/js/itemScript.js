$(document).ready(function() {
	$("#addItemDiv").hide();
	window.itemShowScript.showAllItems();

	$("#addBtn").click(function() {
		$("#addItemDiv").toggle();

	});

	/* CREATE AN ITEM AJAX CALL*/
	$("#itemSubmit").click(function (e) {
		const baseURI = window.itemUpdateScript.getBaseURI();
		$.ajax({
			type: "POST",
			url: baseURI  + "Home/createItem",
			data: {
				itemName: $("#itemName").val(),
				itemPrice: $("#itemPrice").val()
			},
			success: function (result) {
				if (result.success === true) {
					Swal.fire("Saved", "Added to API","success");
					$('#itemName').val("");
					$('#itemPrice').val("");
					$("#addItemDiv").hide("slow");
					window.itemShowScript.showAllItems();
				} else {
					Swal.fire(result.responseText, "Retry Again - API refused to save", "error");
				}
			},
			error: function (result) {
				Swal.fire("Error", "Could not cannot to API", "error");

			}
		});
	});

});