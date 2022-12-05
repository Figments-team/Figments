extends Node

enum state { UNKNOWN, STARTING, WAITING, WORKING, IDLE }
signal director_state_changed(old_state, new_state)
var status = state.UNKNOWN : set = state_set

var current_task : Callable
var tasks : Array

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
	
	direct(await opening())

func _process(delta):
	match status:
		state.IDLE:
			if tasks.size() > 0:
				status = state.WORKING
				current_task = tasks.pop_front().call_func()
		state.WORKING, state.WAITING:
			if ! current_task.is_valid():
				current_task = null
				status = state.IDLE

func direct(what: Callable):
	tasks.append(what)

func opening():
	print("lol")
	await get_tree().create_timer(5).timeout
	print("test")
