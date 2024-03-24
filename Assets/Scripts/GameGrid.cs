using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexMapTools;

public class GameGrid : MonoBehaviour
{
	private HexCalculator hexCalculator;
	private HexContainer<GameCell> cells;
	
	private int x = 0;
	private int y = 0;
	
	private GameCell[] activeCells;
	
	public Pattern pattern;
	public Game game;
	
	public bool PatternFound
	{
		get;
		private set;
	}
	
    // Start is called before the first frame update
    void Start()
    {
		HexGrid hexGrid = GetComponent<HexGrid>();
		hexCalculator = hexGrid.HexCalculator;
		
		cells = new HexContainer<GameCell>(hexGrid);
		cells.FillWithChildren();
		
		//Count score
		foreach(var pair in cells)
		{
			GameCell innerCell = pair.Value;
			innerCell.Init(pair.Key);
		}
	
		x = -8;
		y = 1;
		// Inited
		activeCells = new GameCell[3];
		GetCellsAt(x, y, activeCells);
		foreach (GameCell element in activeCells)
		{
			element.myIsActive = true;
			Debug.Log(element.GetBlockName());
		}		
    }

    // Update is called once per frame
    void Update()
    {
		bool checkShape = false;
		
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			x = x-1;
			checkShape = true;
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			x = x+1;
						checkShape = true;
		}
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			y=y+1;
						checkShape = true;
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			y=y-1;
						checkShape = true;
		}
		
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Debug.Log("Replace: " + x + " " + y);
			
			GameObject block0 = activeCells[0].transform.GetChild(1).gameObject;
			GameObject block1 = activeCells[1].transform.GetChild(1).gameObject;
			GameObject block2 = activeCells[2].transform.GetChild(1).gameObject;
			
			block1.transform.parent = activeCells[0].transform;
			block1.transform.position = activeCells[0].transform.position;
			
			block2.transform.parent = activeCells[1].transform;
			block2.transform.position = activeCells[1].transform.position;
			
			block0.transform.parent = activeCells[2].transform;
			block0.transform.position = activeCells[2].transform.position;
						checkShape = true;
		}
		else
		{
			foreach (GameCell element in activeCells)
			{
				if(element)
					element.myIsActive = false;
			}		
			GetCellsAt(x, y, activeCells);
			foreach (GameCell element in activeCells)
			{
				if(element)
				{
					element.myIsActive = true;
				}				
			}			
		}

		if(checkShape)
		{
			foreach(var pair in cells)
			{
				GameCell innerCell = pair.Value;
				CheckShape(innerCell.Coords);
			}
		}
    }
	
	GameCell GetFirstCell()
	{
		GameCell cell = this.gameObject.transform.GetChild(0).GetComponent<GameCell>();
		return cell;
	}
	
	void GetCellsAt(int x, int y, GameCell[] arr)
	{	
		HexCoordinates coords = new HexCoordinates(x, y);
		arr[0] = cells.At(coords);
		
		coords = new HexCoordinates(x + 1, y);
		arr[1] = cells.At(coords);
		
		coords = new HexCoordinates(x, y + 1);
		arr[2] = cells.At(coords);
	}
	
	void CheckShape(HexCoordinates coord)
	{
		HashSet<string> blockNames = new HashSet<string>();
		
		int checkedCells = 0;
		foreach (var coordDiff in pattern.diffs)
		{
			HexCoordinates newCoord = coord + coordDiff;
			GameCell cell = cells.At(newCoord);
			if(cell)
			{
				string blockName = cell.GetBlockName();
				blockNames.Add(blockName);
				//Debug.Log(blockName);
				checkedCells = checkedCells + 1;
			}
		}

		PatternFound = (blockNames.Count == 1) && (checkedCells == pattern.diffs.Count);
		
		if(PatternFound)
		{
			Debug.Log("Pattern Found at " + coord);
		
			game.LevelComplete();
			
			foreach (var coordDiff in pattern.diffs)
			{
				HexCoordinates newCoord = coord + coordDiff;
				GameCell cell = cells.At(newCoord);
				if(cell)
				{
					cell.myIsPatternChecked = true;
					cell.gameObject.SetActive(false);
				}
			}
		}
	}
}
