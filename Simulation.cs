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
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()

	{	
		Simulator.Instance.setAgentDefaults(10.0f, 10, 10.0f, 5.0f, 1.0f, 1.0f, new RVO.Vector2(0.0f, 0.0f));


				/*
			* Add (polygonal) obstacles, specifying their vertices in
			* counterclockwise order.
			*/
		IList<RVO.Vector2> obstacle1 = new List<RVO.Vector2>();
		obstacle1.Add(new RVO.Vector2(10.0f, 10.0f));
		obstacle1.Add(new RVO.Vector2(30.0f, 10.0f));
		obstacle1.Add(new RVO.Vector2(30.0f, 20.0f));
		obstacle1.Add(new RVO.Vector2(10.0f, 20.0f));
		Simulator.Instance.addObstacle(obstacle1);

		/*
			* Process the obstacles so that they are accounted for in the
			* simulation.
			*/
		Simulator.Instance.processObstacles();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Simulator.Instance.doStep();
		//GD.Print(Simulator.Instance.getAgentPosition(0));
	}

	public static int AddAgent(Node2D agent){
		int id = Simulator.Instance.addAgent((RVO.Vector2)agent.Position/SCALE);
		return id;
	}

	public static Godot.Vector2 GetAgentPosition(int id) {
		return (Godot.Vector2)Simulator.Instance.getAgentPosition(id)*SCALE;
	}

	public static void SetAgentPreferedVelocity(int id, Godot.Vector2 vel){
		RVO.Vector2 velocity = (RVO.Vector2)vel; 
		Simulator.Instance.setAgentPrefVelocity(id, velocity);
	}


}
