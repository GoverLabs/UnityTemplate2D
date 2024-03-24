using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	public GameObject lvlStart;
	public GameObject lvlComplete;
	public GameObject lvlFailed;
	
	public List<GameObject> levels = new List<GameObject>();
	
	private int index;
	private bool needSwitch;
	
    // Start is called before the first frame update
    void Start()
    {
		index = 0;
		needSwitch = false;
		
        lvlStart.SetActive(false);
		lvlComplete.SetActive(false);
		lvlFailed.SetActive(false);
		
		foreach(GameObject lvl in levels)
		{
			lvl.SetActive(false);
		}
		
		StartLevel();
    }

    // Update is called once per frame
    void Update()
    {
	
        if (Input.GetKeyDown(KeyCode.Space))
		{
			lvlStart.SetActive(false);
			lvlComplete.SetActive(false);
			lvlFailed.SetActive(false);	
			
			if(needSwitch)
			{
				needSwitch = false;
				SwitchLevel();
			}
		}
    }
	
	public void LevelComplete()
	{
		lvlComplete.SetActive(true);
		needSwitch = true;
	}
	
	void StartLevel()
	{
		//string levelName = "Level" + i;
		levels[index].SetActive(true);
		
		lvlStart.SetActive(true);
	}
	
	void SwitchLevel()
	{
		levels[index].SetActive(false);
		index++;
		
		if(index>=levels.Count)
			index=0;
		
		StartLevel();
	}
}
