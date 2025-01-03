extends Node2D

var agents
@onready var sim = $Simulation

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	agents = get_children()
	agents = agents.filter(is_agent)
	for agent in agents:
		var id = sim.AddAgent(agent)
		agent.id = id
		sim.SetAgentPreferedVelocity(id, agent.pref_vel)

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	for agent in agents:
		var pos = sim.GetAgentPosition(agent.id)
		agent.position = pos
	
func is_agent(node: Node2D) -> bool:
	return node.is_in_group("agent")
