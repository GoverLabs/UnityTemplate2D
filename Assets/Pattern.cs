using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexMapTools;

public class Pattern : MonoBehaviour
{
	private HexCalculator hexCalculator;
	private HexContainer<GameCell> cells;
	
	public List<HexCoordinates> diffs;
	
	public GameCell pivotCell;
	
    // Start is called before the first frame update
    void Start()
    {
        HexGrid hexGrid = GetComponent<HexGrid>();
		hexCalculator = hexGrid.HexCalculator;
		
		diffs = new List<HexCoordinates>();
		cells = new HexContainer<GameCell>(hexGrid);
		cells.FillWithChildren();
		
		//Count score
		foreach(var pair in cells)
		{
			GameCell innerCell = pair.Value;
			innerCell.Init(pair.Key);
		}
		
		pivotCell = this.transform.GetChild(1).gameObject.GetComponent<GameCell>();
		
		foreach(var pair in cells)
		{
			GameCell innerCell = pair.Value;
			HexCoordinates innerCoord = pair.Key;
			
			HexCoordinates diff = (pivotCell.Coords - innerCoord) * -1;
			diffs.Add(diff);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
