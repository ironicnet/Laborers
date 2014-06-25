using UnityEngine;
using System.Collections;
using Laborers.Pathfinding;

public class GameManager : MonoBehaviour {
	public GameObject BuildingsContainer;
	public GameObject UnitsContainer;
	// Use this for initialization
	void Start () {
		Laborers.GameManager.PathResolver = new OpenPathResolver();
		Laborers.GameManager.RecipeProvider = new Laborers.BaseRecipeProvider();
		Laborers.GameManager.Init();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
