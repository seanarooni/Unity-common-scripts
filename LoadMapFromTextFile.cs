using UnityEngine;
using System.Collections;
using System.IO;

public class LoadMaps : MonoBehaviour {

	public Vector2 GetMapSize(TextAsset file)
	{ //returns the size of the map as a Vector 2, needs to be converted (shown below).
		StringReader reader = new StringReader(file.text);

		int xSize = 0;
		int ySize = 0;

		if (reader == null)
		{
			print ("read failed");
		}
		else 
		{
			for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
			{
				if (xSize == 0)
				{
					foreach (char c in line)
					{
						xSize++;
					}
				}
				ySize++;
			}
		}
		return new Vector2(xSize, ySize);
	}

	public TileType[,] Read(TextAsset file) 
	{//option to call Read without size information.
		Vector2 size = GetMapSize(file);
		//convert Vector2 from GetMapSize into integers
		return Read(file, (int) size.x, (int) size.y); 
	}

	public TileType[,] Read(TextAsset file, int xSize, int ySize) 
	{ 
		StringReader reader = new StringReader(file.text);

		TileType[,] ttmap = new TileType[xSize, ySize];

		if (reader == null)
		{
			print ("read failed");
			return null;
		} 
		else 
		{
			int x = 0; 
			int y = ySize - 1;
			for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
			{
				//PrintMapLine(line, row);
				x = 0;
				foreach(char c in line)
				{
					//print ("" + x + " " + y);
					ttmap[x,y] = ConvertToTileTypeFromChar(c, x, y);
					x++;
				}

				y--;
			}
			return ttmap;
		}
	}

	/*----------------------------------------------------------------------------
	 * This method is an example of one way to process the char data into your game:
	 * This mess of a method converts the individual characters from the text file 
	 * into BlockTypes tType enums from the TileType class.  
	 * Most of the remaining properties (movementCost, visualPrefab) of the newly 
	 * constructed TileTypes are defined in TileMap's SetUpMap().
	 * the inUse property is set to false in the constructors run by the function.*/
	TileType ConvertToTileTypeFromChar(char c, int x, int y)
	{ 
		TileType tt;

		switch(c)
		{
			
		case '0': //normal
			tt = new TileType(TileType.BlockTypes.Dirt);
			break;
		case '1': //stone 
			tt = new TileType(TileType.BlockTypes.Edge); //impassable rock
			break;
		case '2': //filled in space
			tt = new TileType(TileType.BlockTypes.Filled); //impassable except by player
			break;
		case 'e':
			tt = new TileType(TileType.BlockTypes.Dirt);
			Instantiate(Resources.Load("Enemy") as GameObject, new Vector2(x, y), Quaternion.identity);
			break;
		case 'p':
			tt = new TileType(TileType.BlockTypes.Dirt);
			Instantiate(Resources.Load("Player") as GameObject, new Vector2(x, y), Quaternion.identity);
			break;
		default:
			tt = new TileType(TileType.BlockTypes.Dirt);
			break;
		}
	
		return tt;
	}
	
	/*this method instantiates tiles passed from TileMap's GenerateVisual function,
	* then sets its parent in the Unity Hierarcy to the GameController. 
	* Consider adding these GOs to an object pool for more efficient reuse later.	*/
	public void CreateTile(TileType str, int x, int y)
	{  
		GameObject tt = Instantiate(str.visualPrefab, new Vector2(x,y), Quaternion.identity) as GameObject;
		tt.transform.SetParent(GameObject.FindGameObjectWithTag(Tags.gameController).transform);
	}
}
