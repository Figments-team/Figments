extends FileDialog

func _ready():
	connect("file_selected", on_file_selected)

func on_file_selected(path):
	ProjectSettings.load_resource_pack(path)
	$"../Explore".visible = true
