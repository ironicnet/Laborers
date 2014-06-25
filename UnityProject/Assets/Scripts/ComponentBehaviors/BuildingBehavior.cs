using UnityEngine;
using System.Collections;
using Laborers.Behaviors.Buildings;
using Laborers;

public class BuildingBehavior : MonoBehaviour {
	
	public Laborers.Behaviors.Buildings.Building Building;
	public Color BuildingColor;
	public Color BuiltColor;
	public BuildingBehavior ()
	{
	}
	void Awake()
	{
		Building = new Laborers.Behaviors.Buildings.Building();
		Building.Position = new Position(transform.position.x, transform.position.y, transform.position.z);
	}
	// Use this for initialization
	void Start () {
		Building.Init();
	}
	
	// Update is called once per frame
	void Update () {
		
		Building.Update();
		if (transform != null)
		{
			transform.position = new Vector3(Building.Position.X, Building.Position.Y, Building.Position.Z);
		}
		Transform graphics = this.transform.GetChild(0);
		if (Building.IsBuilt)
		{
			graphics.renderer.material.color = BuiltColor;
		}
		else
		{
			graphics.renderer.material.color = BuildingColor;
		}
	}
}
