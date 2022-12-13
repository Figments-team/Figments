class_name IO

static func get_scene_path(scene_name: String, game_name: String = "") -> String:
	if(game_name != ""):
		return "res://" + game_name + "/" + scene_name
	else:
		return "res://" + scene_name

static func search_scene_path(scene_name: String) -> String:
	var dirs_to_explore: Array
	var current_dir : DirAccess = DirAccess.open("res://")
	var scene_found : bool = false
	while ! scene_found:
		for file in current_dir.get_files():
			if file == (scene_name + ".tscn"):
				scene_found = true
		dirs_to_explore.append_array(current_dir.get_directories())
		current_dir.change_dir(dirs_to_explore.pop_back())
	
	if scene_found:
		return current_dir.get_current_dir() + "/" + scene_name + ".tscn"
	else:
		return ""
