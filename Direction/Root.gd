extends Node

signal scene_load_finished(result)
var loading_scene_path : String

func _ready():
	set_process(false)

func _process(delta):
	var loading_status = ResourceLoader.load_threaded_get_status(loading_scene_path)
	if(loading_status == ResourceLoader.THREAD_LOAD_INVALID_RESOURCE &&
	loading_status == ResourceLoader.THREAD_LOAD_FAILED):
		emit_signal("scene_load_finished", false)
		set_process(false)
	elif(loading_status == ResourceLoader.THREAD_LOAD_LOADED):
		emit_signal("scene_load_finished", ResourceLoader.load_threaded_get(loading_scene_path))
		set_process(false)

func load_scene(path: String):
	loading_scene_path = path
	ResourceLoader.load_threaded_request(loading_scene_path)
	var loading_status = ResourceLoader.load_threaded_get_status(loading_scene_path)
	if(loading_status != ResourceLoader.THREAD_LOAD_INVALID_RESOURCE &&
	loading_status != ResourceLoader.THREAD_LOAD_FAILED):
		set_process(true)
		return true
	else:
		return false
