extends CanvasLayer
class_name OverlayNode

@onready var panel : Panel = $"BlackPanel"
@onready var loading : Control = $"Loading"

func show_loader():
	loading.visible = true

func hide_loader():
	loading.visible = false

func fade_to_black(duration: float = 1.0, force_initial_value: bool = true, \
ease_type : Tween.EaseType = Tween.EASE_IN_OUT):
	var tween = create_tween()
	tween.set_ease(ease_type)
	tween.tween_property(panel, "modulate", Color.WHITE, duration) \
	.from(Color.TRANSPARENT if force_initial_value else panel.modulate)
	await tween.finished

func fade_from_black(duration: float = 1.0, force_initial_value: bool = true, \
ease_type : Tween.EaseType = Tween.EASE_IN_OUT):
	var tween = create_tween()
	tween.set_ease(ease_type)
	tween.tween_property(panel, "modulate", Color.TRANSPARENT, duration) \
	.from(Color.WHITE if force_initial_value else panel.modulate)
	await tween.finished

func cut_to_black():
	panel.modulate = Color.WHITE

func cut_from_black():
	panel.modulate = Color.TRANSPARENT
