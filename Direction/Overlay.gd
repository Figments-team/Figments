extends CanvasLayer

@onready var panel = $"BlackPanel"
@onready var loading = $"Loading"

func show_loader():
	loading.visible = true

func hide_loader():
	loading.visible = false

func fade_to_black(duration: float):
	var tween = create_tween()
	tween.tween_property(panel, "modulate:a", 255, duration)
	await tween.finished

func fade_from_black(duration: float): # Gets called with a duration of 5
	var tween = create_tween()
	tween.tween_property(panel, "modulate:a", 0, duration)
	await tween.finished
