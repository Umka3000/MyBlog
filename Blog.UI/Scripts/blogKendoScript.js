$(document).ready(function () {
	$("#grid").kendoGrid({
		dataSource: {
			type: "odata",
			transport: {
				read: "https://samples.databoom.space/api1/sandboxdb/collections/user",
				create: { url: "https://samples.databoom.space/api1/sandboxdb/collections/user" },
				update: { url: "https://samples.databoom.space/api1/sandboxdb/collections/user" },
				destroy: {
					url: function (data) {
						return "https://samples.databoom.space/api1/sandboxdb/collections/user(" + data.id + ")";
					}
				}
			},
			pageSize: 20,
			serverPaging: true,
			serverFiltering: true,
			serverSorting: true,
			schema: { model: { id: "id" } }
		},
		filterable: true, editable: true, toolbar: ["create", "save", "cancel"],
		sortable: true,
		pageable: true,
		columns: ["name", "email", "password",
			{ command: "destroy", title: " ", width: 150 }]
	});
});