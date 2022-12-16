extends AudioStreamPlayer
class_name MusicPlayerNode

func play_ost(music_name : String):
	stream = load("res://Assets/Music/" + music_name + ".wav")
	play()
