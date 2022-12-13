extends Control

func self_direct():
	await get_tree().create_timer(1).timeout
	ProjectSettings.load_resource_pack("C:/Users/Tommaso/Documents/Synced Documents/Projects/Godot/Figments/Builds/James/James.zip")
	await Overlay.fade_to_black()
	await Director.Root.load_and_add_scene("res://Entry.tscn")
	await Overlay.fade_from_black()
	Director.Root.remove_scene("MainMenu")
