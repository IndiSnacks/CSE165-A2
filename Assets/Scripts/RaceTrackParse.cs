using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class RaceTrackParse : MonoBehaviour {
	[SerializeField] public TextAsset file;
	[SerializeField] public GameObject CheckPoint;

	private List<GameObject> checkpoints = new List<GameObject>();
	public GameObject nextCheckpoint;
	List<Vector3> ParseFile()
	{
		float ScaleFactor = 1.0f / 39.37f;
		List<Vector3> positions = new List<Vector3>();
		string content = file.ToString();
		string[] lines = content.Split('\n');
		for (int i = 0; i < lines.Length; i++)
		{
			string[] coords = lines[i].Split(' ');
			Vector3 pos = new Vector3(float.Parse(coords[0]), float.Parse(coords[1]), float.Parse(coords[2]));
			positions.Add(pos * ScaleFactor);
		}
		return positions;
	}

	private void Start() {
		List<Vector3> positions = ParseFile();
		for(int i = 0; i < positions.Count; i++)
		{
			checkpoints.Add(Instantiate(CheckPoint, positions[i], Quaternion.identity));
		}
		nextCheckpoint = checkpoints[0];
	}

	//check if the player has passed a checkpoint and removes it form the list
	public void CheckPointObserver(GameObject cp){
		if(checkpoints[0].Equals(cp)){
			Destroy(checkpoints[0]);
			checkpoints.RemoveAt(0);
			if(checkpoints.Count > 0){
				nextCheckpoint = checkpoints[0];
			}
		}
	}
}