extends Node

enum state { UNKNOWN, STARTING, WAITING, WORKING, IDLE }
signal director_state_changed(old_state, new_state)
var status = state.UNKNOWN : set = state_set

var Root : Node

func state_set(value):
	var old_status = status
	status = value
	emit_signal("director_state_changed", old_status, status)

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

func opening():
	Overlay.fade_from_black(5)
