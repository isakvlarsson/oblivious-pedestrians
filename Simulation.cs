using Godot;
using System;
using RVO;
using System.Runtime.CompilerServices;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Godot.Collections;

public partial class Simulation: Node2D
{
	const float SCALE = 20;
	const float defaultNeighborDist = 50.0f;
	const int defaultMaxNeighbors = 10;
	const float defaultTimeHorizon = 10.0f;
	const float defaultTimeHorizonObst = 5.0f;
	const float defaultRadius = 10.0f;
	const float defaultMaxSpeed = 40.0f;

	public static float GetDefaultNeighborDist(){
		return defaultNeighborDist;
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()

	{	
		/* Specify the global time step of the simulation. */
        Simulator.Instance.setTimeStep(0.1f);
		Simulator.Instance.setAgentDefaults(defaultNeighborDist, defaultMaxNeighbors, defaultTimeHorizon, defaultTimeHorizonObst, defaultRadius, defaultMaxSpeed, new RVO.Vector2(0.0f, 0.0f));

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Simulator.Instance.doStep();
		//GD.Print(Simulator.Instance.getAgentPosition(0));
	}

	public static int AddAgent(Node2D agent){
		int id = Simulator.Instance.addAgent((RVO.Vector2)agent.Position);
		return id;
	}
	public static int AddAgent(Godot.Vector2 position, float neighborDist, float radius, float maxSpeed){
		int id = Simulator.Instance.addAgent((RVO.Vector2)position, neighborDist, defaultMaxNeighbors, defaultTimeHorizon, defaultTimeHorizonObst, radius, maxSpeed, new RVO.Vector2(0.0f, 0.0f));
		return id;
	}

	public static Godot.Vector2 GetAgentPosition(int id) {
		return (Godot.Vector2)Simulator.Instance.getAgentPosition(id);
	}

	public static void SetAgentPreferedVelocity(int id, Godot.Vector2 vel){
		RVO.Vector2 velocity = (RVO.Vector2)vel; 
		Simulator.Instance.setAgentPrefVelocity(id, velocity);
	}

	public static void AddObstacle(Array<Godot.Vector2> vertices){
		IList<RVO.Vector2> obstacle = new List<RVO.Vector2>();
		foreach (Godot.Vector2 v in vertices) {
			obstacle.Add((RVO.Vector2)v);
		}
		Simulator.Instance.addObstacle(obstacle);
		Simulator.Instance.processObstacles();
		//GD.Print("Obstacle added: " + obstacle[0] + " " + obstacle[1]);
	}


}
