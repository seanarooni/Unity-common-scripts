using UnityEngine;
using System.Collections;
using System.IO;

public class LoadMapFromTextFile : MonoBehaviour {

	private GameObject tile01; 
	private TextAsset MapText;
	private string mapString;

//origin corresponds to the top left corner of the screen
//these correspond to default 2D settings with 8 bit tiles (@ 8 pixels per unit)
	private float originX = -7.5f;
	private float originY = 4.5f;

	public TextAsset Map01Text; //set in the unity editor 

	// Use this for initialization
	void Start () {
		tile01 = Resources.Load("Tile01") as GameObject;
		Read(Map01Text);
	}

	void Read(TextAsset file)
	{

		StringReader reader = new StringReader(file.text);

		if (reader == null)
		{
			print ("Read failed");
		} 
		else
		{
			int row = 0;
			for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
			{
				PrintMapLine(line, row);
				row--; 
			}
		}


	}

	void PrintMapLine(string line, int row)
	{
		for (int i=0; i<line.Length; i++)
		{
			if (line[i].ToString() == "1")
			{
				Instantiate(earthTile01, new Vector2(i + originX, row + originY), Quaternion.identity);
			}
		}
	}
