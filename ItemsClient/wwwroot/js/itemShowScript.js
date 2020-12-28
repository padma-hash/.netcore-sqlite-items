window.itemShowScript = {
	/* REFRESH ALL ITEMS**/
	showAllItems() {

		const tbody = document.getElementById("itemsTable").querySelector("tbody");
		tbody.innerHTML = "";
		fetch('https://localhost:44317/api/Items')
			.then(response => response.json())
			.then(data => {
				//console.log(data) 
				for (let i = 0; i < data.length; i++) {
					tbody.innerHTML += `<tr><td>${data[i].itemId}</td><td>${data[i].itemName}</td><td>${data[i].price}</td><td><button id='delBtn' class='btn btn-outline-danger' onclick='delItem("${data[i].itemId
						}");'><i class="fa fa-trash" aria-hidden="true"></i>&nbsp;Delete</button>&nbsp;&nbsp;<button id='updateBtn' class='btn btn-outline-success' onclick='updateItem("${data[i].itemId
						}");'><i class="fa fa-pencil-square-o" aria-hidden="true"></i>&nbsp;update</button> </td ></tr>`;
				}
			})
			.catch(error => console.error(tbody.innerHTML = "No Data Retrieved. Please check again later"));

	}

}
