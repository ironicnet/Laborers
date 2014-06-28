using UnityEngine;
using System.Collections;
using Laborers.Behaviors.Buildings;
using Laborers;

public class BuildingBehavior : MonoBehaviour
{
	
		public Laborers.Behaviors.Buildings.Building Building;
		public Color BuildingColor;
		public Color BuiltColor;
		protected Transform graphics;
		protected Animator animator;
		public float Progress;
		
		public BuildingBehavior ()
		{
		}
		void Awake ()
		{
				Building = new Laborers.Behaviors.Buildings.Building ();
				Building.Position = new Position (transform.position.x, transform.position.y, transform.position.z);
				graphics = this.transform.GetChild (0);
				animator = graphics.GetComponent<Animator> ();
		}
		// Use this for initialization
		void Start ()
		{
				Building.Init ();
		}
	
		// Update is called once per frame
		void Update ()
		{
		
				Building.Update ();
				UpdateProgress ();
				if (transform != null) {
						transform.position = new Vector3 (Building.Position.X, Building.Position.Y, Building.Position.Z);
				}
				if (Building.IsBuilt) {
						graphics.renderer.material.color = BuiltColor;
				} else {
						graphics.renderer.material.color = BuildingColor;
				}
		}

		void UpdateProgress ()
		{
				float total = Building.Requirements.Total;
				float current = Building.Storage.Total;
				
				if (total != 0) {
						Progress = current * 100 / total;
				} else {
						Progress = 100;
				}
				animator.SetFloat ("Progress", Progress);
			
		}
}
