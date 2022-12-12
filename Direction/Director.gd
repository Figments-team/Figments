extends Node
class_name DirectorNode

enum state { UNKNOWN, STARTING, WAITING, WORKING, IDLE }
signal state_changed(old_state, new_state)
var status = state.UNKNOWN : set = state_set

var Root : RootNode

func state_set(value):
	var old_status = status
	status = value
	emit_signal("state_changed", old_status, status)

func _ready():
	status = state.STARTING
	Root = get_tree().current_scene #get_node("/root/Root")
	status = state.WAITING
	await Root.ready
	status = state.IDLE
	direct(opening)

func direct(coroutine: Callable):
	status = state.WORKING
	await coroutine.call()
	status = state.IDLE

func self_direct(node_name: String):
	await direct(Root.get_node(node_name).self_direct)

func opening():
	await get_tree().create_timer(1).timeout
	await Root.load_scene("res://UI/Splash.tscn")
	Root.load_scene("res://UI/MainMenu.tscn", false)
	await Overlay.fade_from_black()
	await self_direct("Splash")
	await Overlay.fade_to_black()
	Root.remove_scene("Splash")
	await Root.wait_loading_or_skip()
	Root.add_loaded_scene()
	MusicPlayer.play_ost("Figments")
	await Overlay.fade_from_black()
