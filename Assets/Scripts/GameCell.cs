using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexMapTools;

public class GameCell : MonoBehaviour
{
	public GameObject myBlock;
	public GameBlockTemplate myBlockTemplate;
		
	public bool myIsActive = false;
	public bool myIsPatternChecked = false;
	
    // Start is called before the first frame update
    void Start()
    {
		myBlock = Instantiate(myBlockTemplate.myList[Random.Range (0, myBlockTemplate.myList.Count)], this.transform);
		
		//myCoord = new HexCoordinates(this.transform.position.x, this.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {

    }
	
	public HexCoordinates Coords
	{
		get;
		private set;
	}
	
	public void Init(HexCoordinates coords)
	{
		Coords = coords;
	}
	
	public string GetBlockName()
	{
		GameObject hegaxon = this.transform.GetChild(1).gameObject;
		if(!hegaxon)
			return "";
		
		GameObject block = hegaxon.transform.GetChild(0).gameObject;
		if(!block)
			return "";
		
		return block.name;
		
	}
	
	void OnDrawGizmos()
    {
			
		if(myIsPatternChecked)
		{
					        // Draw a yellow sphere at the transform's position
		Color color = Color.red;
		color.a = 0.3f;
        Gizmos.color = color;
        Gizmos.DrawCube(transform.position /*+ new Vector3(0.5f, 0.5f, 0.5f)*/, new Vector3(1, 1, 1));	
		}
		
		if(myIsActive)
		{
			        // Draw a yellow sphere at the transform's position
		Color color = Color.yellow;
		color.a = 0.3f;
        Gizmos.color = color;
        Gizmos.DrawCube(transform.position /*+ new Vector3(0.5f, 0.5f, 0.5f)*/, new Vector3(1, 1, 1));
		}


    }
}
