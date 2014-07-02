using UnityEngine;
using System.Collections;
using Laborers.Behaviors.Buildings;
using Laborers;
using System.Collections.Generic;

public class BuildingBehavior : MonoBehaviour
{
	
		public Laborers.Behaviors.Buildings.Building Building;
		public Color BuildingColor;
		public Color BuiltColor;
		protected Transform graphics;
		protected Animator animator;
		public float Progress;
		public int stage = 0;
		private int lastStage = 0;
		
		private Mesh mesh;
		private MeshCollider col;
		public BuildingData data;
		private List<Vector3> newVertices = new List<Vector3> ();
		private List<int> newTriangles = new List<int> ();
		private List<Vector2> newUV = new List<Vector2> ();
		private int faceCount;
		private float tUnit = 0.25f;
		private float cubeSize = 1f;

		bool updateMesh;
	
		private void FillData (int y, List<byte[]> rowData)
		{
				for (int x=0; x< data.Width; x++) {
						for (int z = 0; z < data.Width; z++) {
				
								try {
								
										byte[] d = rowData [x];
										byte info = d [z];
								
										data.data [y, x, z] = info;
								} catch {
										Debug.Log (string.Format ("y:{0},x:{1},z:{2}", y, x, z));
										Debug.Log (string.Format ("Data: {0}", rowData [x] [z]));
								}
						}
				}
		}

		public BuildingBehavior ()
		{
				data = new BuildingData ();
				data.Width = 10;
				data.Height = 5;
				data.data = new byte[data.Height, data.Width, data.Width];
		
				var rowData = new List<byte[]> (){
			new byte[11] {0,0,0,0,0,0,0,0,0,0,0},
			new byte[11] {0,1,1,1,2,2,1,1,1,1,0},
			new byte[11] {0,1,0,0,0,0,0,0,0,1,0},
			new byte[11] {0,1,0,0,0,0,0,0,0,1,0},
			new byte[11] {0,1,0,0,0,0,0,0,0,1,0},
			new byte[11] {0,1,0,0,0,0,0,0,0,1,0},
			new byte[11] {0,1,0,0,0,0,0,0,0,1,0},
			new byte[11] {0,1,0,0,0,0,0,0,0,1,0},
			new byte[11] {0,1,1,1,1,1,1,1,1,1,0},
			new byte[11] {0,0,0,0,0,0,0,0,0,0,0}};
				FillData (0, rowData);
			
		
				rowData = new List<byte[]> (){
			new byte[11] {0,0,0,0,0,0,0,0,0,0,0},
			new byte[11] {0,0,0,0,0,0,0,0,0,0,0},
			new byte[11] {0,1,0,1,2,2,1,0,0,1,0},
			new byte[11] {0,1,0,0,0,0,0,0,0,1,0},
			new byte[11] {0,0,0,0,0,0,0,0,0,0,0},
			new byte[11] {0,0,0,0,0,0,0,0,0,0,0},
			new byte[11] {0,0,0,0,0,0,0,0,0,0,0},
			new byte[11] {0,1,0,0,0,0,0,0,0,1,0},
			new byte[11] {0,1,0,0,0,0,0,0,0,1,0},
			new byte[11] {0,1,1,1,1,1,1,1,1,1,0},
			new byte[11] {0,0,0,0,0,0,0,0,0,0,0}};
				FillData (1, rowData);
		
		
		
				rowData = new List<byte[]> (){
			new byte[11] {0,0,0,0,0,0,0,0,0,0,0},
			new byte[11] {0,1,1,1,1,1,1,1,1,1,0},
			new byte[11] {0,1,0,0,0,0,0,0,0,1,0},
			new byte[11] {0,1,0,0,0,0,0,0,0,1,0},
			new byte[11] {0,1,0,0,0,0,0,0,0,1,0},
			new byte[11] {0,1,0,0,0,0,0,0,0,1,0},
			new byte[11] {0,1,0,0,0,0,0,0,0,1,0},
			new byte[11] {0,1,0,0,0,0,0,0,0,1,0},
			new byte[11] {0,1,1,1,1,1,1,1,1,1,0},
			new byte[11] {0,0,0,0,0,0,0,0,0,0,0}};
				FillData (2, rowData);
				rowData = new List<byte[]> (){
			new byte[11] {0,0,0,0,0,0,0,0,0,0,0},
			new byte[11] {0,0,0,0,0,0,0,0,0,0,0},
			new byte[11] {0,0,1,1,1,1,1,1,0,0,0},
			new byte[11] {0,0,1,0,0,0,0,1,0,0,0},
			new byte[11] {0,0,1,0,0,0,0,1,0,0,0},
			new byte[11] {0,0,1,0,0,0,0,1,0,0,0},
			new byte[11] {0,0,1,0,0,0,0,1,0,0,0},
			new byte[11] {0,0,1,1,1,1,1,1,0,0,0},
			new byte[11] {0,0,0,0,0,0,0,0,0,0,0},
			new byte[11] {0,0,0,0,0,0,0,0,0,0,0}};
				FillData (3, rowData);
				rowData = new List<byte[]> (){
			new byte[11] {0,0,0,0,0,0,0,0,0,0,0},
			new byte[11] {0,0,0,0,0,0,0,0,0,0,0},
			new byte[11] {0,0,0,0,0,0,0,0,0,0,0},
			new byte[11] {0,0,0,1,1,1,1,0,0,0,0},
			new byte[11] {0,0,0,1,0,0,1,0,0,0,0},
			new byte[11] {0,0,0,1,0,0,1,0,0,0,0},
			new byte[11] {0,0,0,1,1,1,1,0,0,0,0},
			new byte[11] {0,0,0,0,0,0,0,0,0,0,0},
			new byte[11] {0,0,0,0,0,0,0,0,0,0,0},
			new byte[11] {0,0,0,0,0,0,0,0,0,0,0}};
				FillData (4, rowData);
				rowData = new List<byte[]> (){
			new byte[11] {0,0,0,0,0,0,0,0,0,0,0},
			new byte[11] {0,0,0,0,0,0,0,0,0,0,0},
			new byte[11] {0,0,0,0,0,0,0,0,0,0,0},
			new byte[11] {0,0,0,0,0,0,0,0,0,0,0},
			new byte[11] {0,0,0,0,0,0,0,0,0,0,0},
			new byte[11] {0,0,0,0,1,1,0,0,0,0,0},
			new byte[11] {0,0,0,0,1,1,0,0,0,0,0},
			new byte[11] {0,0,0,0,0,0,0,0,0,0,0},
			new byte[11] {0,0,0,0,0,0,0,0,0,0,0},
			new byte[11] {0,0,0,0,0,0,0,0,0,0,0}};
				FillData (5, rowData);
			
		
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
		
				mesh = GetComponent<MeshFilter> ().mesh;
				col = GetComponent<MeshCollider> ();
				col.sharedMesh = null;
				col.sharedMesh = mesh;
				GenerateMesh ();
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
		void LateUpdate ()
		{
				if (updateMesh) {
						GenerateMesh ();
						updateMesh = false;
				}
		}
		float lastProgress = 0f;
		void UpdateProgress ()
		{
				if (!Building.IsBuilt) {
						float total = Building.Requirements.Total;
						float current = Building.Storage.Total;
						lastProgress = Progress;
						lastStage = stage;
						if (total != 0) {
								Progress = current * 100 / total;
						} else {
								Progress = 100;
						}
						stage = Mathf.FloorToInt (Progress / 10);
						
						animator.SetFloat ("Progress", Progress);
						if (lastStage != stage) {
								updateMesh = true;
						}
				}
				
		}
	
		public void GenerateMesh ()
		{
		
				for (int y=0; y<=data.Height; y++) {
						if (y > stage)
								continue;
								
						for (int x=0; x<data.Width; x++) {
								for (int z=0; z<data.Width; z++) {
										//This code will run for every block in the chunk
					
										if (IsSolid (x, y, z)) {
												//If the block is solid
						
												if (!IsSolid (x, y + 1, z)) {
														//Block above is air
														CubeTop (x, y, z, Block (x, y, z));
												}
						
												if (!IsSolid (x, y - 1, z)) {
														//Block below is air
														CubeBot (x, y, z, Block (x, y, z));
							
												}
						
												if (!IsSolid (x + 1, y, z)) {
														//Block east is air
														CubeEast (x, y, z, Block (x, y, z));
							
												}
						
												if (!IsSolid (x - 1, y, z)) {
														//Block west is air
														CubeWest (x, y, z, Block (x, y, z));
							
												}
						
												if (!IsSolid (x, y, z + 1)) {
														//Block north is air
														CubeNorth (x, y, z, Block (x, y, z));
							
												}
						
												if (!IsSolid (x, y, z - 1)) {
														//Block south is air
														CubeSouth (x, y, z, Block (x, y, z));
							
												}
						
										}
					
								}
						}
				}
		
				UpdateMesh ();
		}
	
		void UpdateMesh ()
		{
		
				mesh.Clear ();
				mesh.vertices = newVertices.ToArray ();
				mesh.uv = newUV.ToArray ();
				mesh.triangles = newTriangles.ToArray ();
				mesh.Optimize ();
				mesh.RecalculateNormals ();
		
				col.sharedMesh = null;
				col.sharedMesh = mesh;
		
				newVertices.Clear ();
				newUV.Clear ();
				newTriangles.Clear ();
		
				faceCount = 0; //Fixed: Added this thanks to a bug pointed out by ratnushock!
		
		}

		bool IsSolid (int x, int y, int z)
		{
				if (x < 0 || y < 0 || z < 0)
						return false;
				if (y > stage)
						return false;
				if (data.data.GetLength (0) < y)
						return false;
				if (data.data.GetLength (1) < x)
						return false;
				if (data.data.GetLength (2) < z)
						return false;
				try {
						return data.data [y, x, z] != 0;
				} catch {
						Debug.LogWarning (string.Format ("IsSolid y:{0},x:{1},z:{2}", y, x, z));
						//throw;
						return false;
				}
		}
	
		byte Block (int x, int y, int z)
		{
				return data.data [y, x, z];
		}

		void Cube (Vector2 texturePos)
		{
				newTriangles.Add (faceCount * 4); //1
				newTriangles.Add (faceCount * 4 + 1); //2
				newTriangles.Add (faceCount * 4 + 2); //3
				newTriangles.Add (faceCount * 4); //1
				newTriangles.Add (faceCount * 4 + 2); //3
				newTriangles.Add (faceCount * 4 + 3); //4
		
				newUV.Add (new Vector2 (tUnit * texturePos.x + tUnit, tUnit * texturePos.y));
				newUV.Add (new Vector2 (tUnit * texturePos.x + tUnit, tUnit * texturePos.y + tUnit));
				newUV.Add (new Vector2 (tUnit * texturePos.x, tUnit * texturePos.y + tUnit));
				newUV.Add (new Vector2 (tUnit * texturePos.x, tUnit * texturePos.y));
		
				faceCount++; // Add this line
		}
	
		void CubeTop (int x, int y, int z, byte block)
		{
				newVertices.Add (new Vector3 (x, y, z + cubeSize));
				newVertices.Add (new Vector3 (x + cubeSize, y, z + cubeSize));
				newVertices.Add (new Vector3 (x + cubeSize, y, z));
				newVertices.Add (new Vector3 (x, y, z));
		
				var texturePos = DecideTexture (x, y, z, true);
				Cube (texturePos);
		}
	
		void CubeNorth (int x, int y, int z, byte block)
		{
				newVertices.Add (new Vector3 (x + cubeSize, y - cubeSize, z + cubeSize));
				newVertices.Add (new Vector3 (x + cubeSize, y, z + cubeSize));
				newVertices.Add (new Vector3 (x, y, z + cubeSize));
				newVertices.Add (new Vector3 (x, y - cubeSize, z + cubeSize));
		
				var texturePos = DecideTexture (x, y, z, false);
		
				Cube (texturePos);
		}

		void CubeEast (int x, int y, int z, byte block)
		{
				newVertices.Add (new Vector3 (x + cubeSize, y - cubeSize, z));
				newVertices.Add (new Vector3 (x + cubeSize, y, z));
				newVertices.Add (new Vector3 (x + cubeSize, y, z + cubeSize));
				newVertices.Add (new Vector3 (x + cubeSize, y - cubeSize, z + cubeSize));
		
				var texturePos = DecideTexture (x, y, z, false);
		
				Cube (texturePos);
		}

		void CubeSouth (int x, int y, int z, byte block)
		{
				newVertices.Add (new Vector3 (x, y - cubeSize, z));
				newVertices.Add (new Vector3 (x, y, z));
				newVertices.Add (new Vector3 (x + cubeSize, y, z));
				newVertices.Add (new Vector3 (x + cubeSize, y - cubeSize, z));
		
				var texturePos = DecideTexture (x, y, z, false);
		
				Cube (texturePos);
		}

		void CubeWest (int x, int y, int z, byte block)
		{
				newVertices.Add (new Vector3 (x, y - cubeSize, z + cubeSize));
				newVertices.Add (new Vector3 (x, y, z + cubeSize));
				newVertices.Add (new Vector3 (x, y, z));
				newVertices.Add (new Vector3 (x, y - cubeSize, z));
		
				var texturePos = DecideTexture (x, y, z, false);
		
				Cube (texturePos);
		}

		void CubeBot (int x, int y, int z, byte block)
		{
				newVertices.Add (new Vector3 (x, y - cubeSize, z));
				newVertices.Add (new Vector3 (x + cubeSize, y - cubeSize, z));
				newVertices.Add (new Vector3 (x + cubeSize, y - cubeSize, z + cubeSize));
				newVertices.Add (new Vector3 (x, y - cubeSize, z + cubeSize));
		
				var texturePos = DecideTexture (x, y, z, false);
		
				Cube (texturePos);
		}
	
		private Vector2 tAir = new Vector2 (3, 0);
		private Vector2 tStone = new Vector2 (3, 2);
		private Vector2 tDoor = new Vector2 (1, 3);

		Vector2 DecideTexture (int x, int y, int z, bool isTop)
		{
				Vector2 texturePos = new Vector2 (0, 0);
				if (Block (x, y, z) == 0) {
						texturePos = tAir;
				}
				if (Block (x, y, z) == 1) {
						texturePos = tStone;
				} else if (Block (x, y, z) == 2) {
						texturePos = tDoor;
				}
				return texturePos;
		}
}
