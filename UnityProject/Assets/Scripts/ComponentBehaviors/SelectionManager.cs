using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectionManager : MonoBehaviour
{
		public List<GameObject> SelectedObjects = new List<GameObject> ();
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
	
		public virtual void AddToSelection (GameObject objectToAdd)
		{
				SelectedObjects.Add (objectToAdd);
		}

		public virtual void AddToSelection (List<GameObject> objectsToAdd)
		{
				SelectedObjects.AddRange (objectsToAdd);
		}

		public virtual void RemoveFromSelection (GameObject objectToRemove)
		{
				SelectedObjects.Remove (objectToRemove);
		}

		public virtual void RemoveFromSelection (List<GameObject> objectsToRemove)
		{
				SelectedObjects.RemoveAll (o => objectsToRemove.Contains (o));
		}
		
		public void UpdateGUI ()
		{
		}
		void OnGUI ()
		{
				GUI.Label (new Rect (0, 0, 500, 500), string.Format ("{0} entities selected", SelectedObjects.Count));
		}
}
