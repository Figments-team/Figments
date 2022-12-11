extends AudioStreamPlayer
class_name MusicPlayerNode

func play_ost(name : String, from : float = 0.0):
	stream = load("res://Assets/Music/" + name + ".wav")
	play()
