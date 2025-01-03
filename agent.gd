extends Node2D

@export var id: int
@export var pref_vel: Vector2 
@export var movement_speed = 1.0

@onready var navigation_agent: NavigationAgent2D = $NavigationAgent2D
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
	
func set_movement_target(movement_target: Vector2):
	
	navigation_agent.target_position = movement_target

func _physics_process(delta):
	# Wait for the first physics frame so the NavigationServer can sync.
	await get_tree().physics_frame
	if navigation_agent.is_navigation_finished():
		return

	var current_agent_position: Vector2 = global_position
	var next_path_position: Vector2 = navigation_agent.get_next_path_position()

	pref_vel = current_agent_position.direction_to(next_path_position) * movement_speed
