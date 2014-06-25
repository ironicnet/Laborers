using UnityEngine;
using System.Collections;
using Laborers.Tasks;
using Laborers;

public class PrototypeScene : MonoBehaviour {

	GameObject[] buildings;
	GameObject[] units;
	// Use this for initialization
	void Start () {
		buildings = new GameObject[4];
		var buildingLoad = Resources.Load("Buildings/Building");
		buildings[0] = Instantiate(buildingLoad) as GameObject;
		buildings[0].GetComponent<BuildingBehavior>().Building.Position= new Laborers.Position(4,0,3);
		buildings[1] = Instantiate(buildingLoad) as GameObject;
		buildings[1].GetComponent<BuildingBehavior>().Building.Position= new Laborers.Position(-2,0,-5);
		buildings[2] = Instantiate(buildingLoad) as GameObject;
		buildings[2].GetComponent<BuildingBehavior>().Building.Position= new Laborers.Position(1,0,-2);
		buildings[3] = Instantiate(buildingLoad) as GameObject;
		buildings[3].GetComponent<BuildingBehavior>().Building.Position= new Laborers.Position(-5,0,3);

		units = new GameObject[1];
		units[0]= Instantiate(Resources.Load("Units/Unit")) as GameObject;
		var unit = units[0].GetComponent<UnitBehavior>().Unit;
		
		Laborers.UnitWorkPlan workPlan = new Laborers.UnitWorkPlan();
		for(var i=0; i <buildings.Length; i++)
		{
			var building = buildings[i].GetComponent<BuildingBehavior>().Building;
			workPlan.AddTask(new MoveToPositionTask(building.Position));
			workPlan.AddTask(new EnterBuildingTask(ref building));
			workPlan.AddTask(new BuildBuildingTask());
		}
		workPlan.AddTask(new MoveToPositionTask(new Position(0,0,0)));
		
		unit.AssignWorkPlan(workPlan);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
