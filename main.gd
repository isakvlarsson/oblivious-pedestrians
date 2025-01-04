extends Node2D

var agents
var obstacles
@onready var sim = $Simulation
@onready var nav_region = $NavigationRegion2D

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	agents = get_children().filter(is_agent)
	for agent in agents:
		# (Godot.Vector2 position, float neighborDist, float radius, float maxSpeed)
		var neigbourDist = sim.GetDefaultNeighborDist()
		if agent.oblivious:
			neigbourDist = neigbourDist/2.0
			
		var id = sim.AddAgent(agent.global_position, neigbourDist, agent.get_radius(), agent.movement_speed*4)
		agent.id = id
	
	obstacles = nav_region.get_children().filter(is_obstacle)
	for obstacle in obstacles:
		print(obstacle.polygon)
		var vertices = Array(obstacle.polygon).map(obstacle.to_global)
		vertices.reverse()
		print(vertices)
		sim.AddObstacle(vertices)
		
# Called every frame. 'delta' is the elapsed time since the previous frame.
func _physics_process(delta: float) -> void:
	for agent in agents:
		var pos = sim.GetAgentPosition(agent.id)
		agent.position = pos
		sim.SetAgentPreferedVelocity(agent.id, agent.pref_vel)
	
func is_agent(node: Node2D) -> bool:
	return node.is_in_group("agent")
	
func is_obstacle(node: Node2D) -> bool:
	return node.is_in_group("obstacle")
