extends Node
class_name RootNode

signal scene_load_finished()
var loading_scene_path : String

func _ready():
	set_process(false)

func _process(delta):
	var loading_status = ResourceLoader.load_threaded_get_status(loading_scene_path)
	if loading_status != ResourceLoader.THREAD_LOAD_IN_PROGRESS:
		emit_signal("scene_load_finished")
		set_process(false)

func wait_loading_or_skip():
	var loading_status = ResourceLoader.load_threaded_get_status(loading_scene_path)
	if loading_status == ResourceLoader.THREAD_LOAD_IN_PROGRESS:
		await scene_load_finished

func load_scene(path: String, also_add: bool = true):
	loading_scene_path = path
	ResourceLoader.load_threaded_request(loading_scene_path)
	set_process(true)
	if also_add:
		await scene_load_finished
		add_loaded_scene()

func add_loaded_scene():
	var loading_status = ResourceLoader.load_threaded_get_status(loading_scene_path)
	var loaded_scene : PackedScene = ResourceLoader.load_threaded_get(loading_scene_path)
	add_child(loaded_scene.instantiate())

func remove_scene(name : String):
	for node in get_children():
		if node.name == name:
			node.queue_free()
			return
