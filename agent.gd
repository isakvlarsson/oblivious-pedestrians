extends Node2D

@export var id: int
@export var pref_vel: Vector2 
@export var movement_speed = 10.0
@export var target: Node2D
@export var oblivious: bool = false

var target_reached: bool = false

@onready var navigation_agent: NavigationAgent2D = $NavigationAgent2D

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	navigation_agent.path_max_distance = 2
	set_movement_target(target.global_position)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
	
func set_movement_target(movement_target: Vector2):
	navigation_agent.target_position = movement_target

func get_radius() -> float:
	return scale.x/2

func _physics_process(delta):
	# Wait for the first physics frame so the NavigationServer can sync.
	await get_tree().physics_frame
	if navigation_agent.is_navigation_finished() and !target_reached:
		target_reached = true
		print(name + ": Target reached!")
		pref_vel = Vector2(0,0)
		return
	
	var current_agent_position: Vector2 = global_position
	var next_path_position: Vector2 = navigation_agent.get_next_path_position()

	pref_vel = current_agent_position.direction_to(next_path_position) * movement_speed
