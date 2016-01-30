using UnityEngine;
using System.Collections;

public class TileManager : MonoBehaviour
{
	public GameObject[,] map;
	public int gridX, gridY;
	// Use this for initialization
	void Start ()
	{
		float tileSize = this.gameObject.GetComponent<MeshFilter> ().mesh.bounds.size.x;
		map = new GameObject[gridX, gridY];
		for (int y =0; y < gridY; y++) {
			for (int x =0; x < gridX; x++) {
				GameObject tile = Instantiate (this.gameObject, new Vector3 (x * tileSize, y * tileSize, 0), Quaternion.identity) as GameObject;
				Debug.Log ("Creating tile:" + x + " " + y);
				map [x, y] = tile;
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
