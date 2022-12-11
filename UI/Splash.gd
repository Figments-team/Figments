extends Control

func self_direct():
	await get_tree().create_timer(5).timeout
