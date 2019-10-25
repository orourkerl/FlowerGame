using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBehavior : MonoBehaviour
{
	[SerializeField] public bool findDistance = false;
	public int rows = 10;
	public int columns = 10;
	public int scale = 1;
	public GameObject gridPrefab;
	public GameObject characterPrefab;
	public GameObject character;
	public Vector3 leftBottomLocation = new Vector3(0, 0, 0);
	public GameObject[,] gridArray;
	public int startX = 0; 
	public int startY = 0;
	public int endX = 2;
	public int endY = 2;
	public List<GameObject> path = new List<GameObject>();

	void Awake()
	{
		gridArray = new GameObject[columns, rows];
        if (gridPrefab)
		{
			GenerateGrid();
		}
        else
		{
			print("Missing prefab. Please assign!");
		}
	}

    // Start is called before the first frame update
    void Start()
    {
		character = Instantiate(characterPrefab);
		character.transform.SetParent(gameObject.transform);
		Vector3 currentLocation = gridArray[startX, startY].transform.position;
		character.transform.position = currentLocation;
    }

    // Update is called once per frame
    void Update()
    {
		if (findDistance)
		{
			SetDistance();
			SetPath();
			findDistance = false;
			startX = endX;
			startY = endY;
			character.transform.position = gridArray[endX, endY].transform.position;
		}
	}

    void GenerateGrid()
	{
        for (int i = 0; i < columns; i++)
		{
            for (int j = 0; j < rows; j++)
			{
				GameObject obj = Instantiate(gridPrefab, new Vector3(leftBottomLocation.x + scale * i, leftBottomLocation.y + scale * j, leftBottomLocation.z + scale * j), Quaternion.identity);
				obj.transform.SetParent(gameObject.transform);
				obj.GetComponent<GridStat>().x = i;
				obj.GetComponent<GridStat>().y = j;
				gridArray[i, j] = obj;
			}
		}
	}

	void InitialSetup()
	{
        foreach(GameObject obj in gridArray)
		{
			obj.GetComponent<GridStat>().visited = -1;
		}
		gridArray[startX, startY].GetComponent<GridStat>().visited = 0;
	}

    bool TestDirection(int x, int y, int step, int direction)
	{
        //int direction tells which case to use. 1 is up, 2 is right, 3 is down, 4 is left
        switch(direction)
		{
			case 1:
				if (y + 1 < rows && gridArray[x, y + 1] && gridArray[x, y + 1].GetComponent<GridStat>().visited == step)
					return true;
				else
					return false;
			case 2:
				if (x + 1 < columns && gridArray[x + 1, y] && gridArray[x + 1, y].GetComponent<GridStat>().visited == step)
					return true;
				else
					return false;
			case 3:
				if (y - 1 > -1 && gridArray[x, y - 1] && gridArray[x, y - 1].GetComponent<GridStat>().visited == step)
					return true;
				else
					return false;
			case 4:
				if (x - 1 > -1 && gridArray[x - 1, y] && gridArray[x - 1, y].GetComponent<GridStat>().visited == step)
					return true;
				else
					return false;
		}
		return false;
	}

    void SetVisited(int x, int y, int step)
	{
        if (gridArray[x, y])
		{
			gridArray[x, y].GetComponent<GridStat>().visited = step;
		}
	}

    GameObject FindClosest(Transform targetLocation, List<GameObject> list)
	{
		float currentDistance = scale * rows * columns;
		int indexNumber = 0;
        for (int i = 0; i < list.Count; i++)
		{
            if(Vector3.Distance(targetLocation.position, list[i].transform.position) < currentDistance)
			{
				currentDistance = Vector3.Distance(targetLocation.position, list[i].transform.position);
				indexNumber = i;
			}
		}
		return list[indexNumber];
	}

    void SetDistance()
	{
		InitialSetup();
		int x = startX;
		int y = startY;
		int[] testArray = new int[rows * columns];
        for(int step = 1; step < rows * columns; step++)
		{
            foreach(GameObject obj in gridArray)
			{
				if (obj && obj.GetComponent<GridStat>().visited == step - 1)
					TestFourDirections(obj.GetComponent<GridStat>().x, obj.GetComponent<GridStat>().y, step);
			}
		}
	}

    void TestFourDirections(int x, int y, int step)
	{
		if (TestDirection(x, y, -1, 1))
			SetVisited(x, y + 1, step);
		if (TestDirection(x, y, - 1, 2))
			SetVisited(x + 1, y, step);
		if (TestDirection(x, y, -1, 3))
			SetVisited(x, y - 1, step);
		if (TestDirection(x, y, -1, 4))
			SetVisited(x - 1, y, step);
	}

    void SetPath()
	{
		int step;
		int x = endX;
		int y = endY;
		List<GameObject> tempList = new List<GameObject>();
		path.Clear();
		if (gridArray[endX, endY] && gridArray[endX, endY].GetComponent<GridStat>().visited > 0)
		{
			path.Add(gridArray[x, y]);
			step = gridArray[x, y].GetComponent<GridStat>().visited - 1;
		}
        else
		{
			print("Can't reach the desired location");
			return;
		}
        for(int i = step; step > -1; step--)
		{
            if(TestDirection(x, y, step, 1))
            {
				tempList.Add(gridArray[x, y + 1]);
			}
			if (TestDirection(x, y, step, 2))
			{
				tempList.Add(gridArray[x + 1, y]);
			}
			if (TestDirection(x, y, step, 3))
			{
				tempList.Add(gridArray[x, y - 1]);
			}
			if (TestDirection(x, y, step, 4))
			{
				tempList.Add(gridArray[x - 1, y]);
			}
			GameObject tempObj = FindClosest(gridArray[endX, endY].transform, tempList);
			path.Add(tempObj);
            //change the color of each element in the path
            foreach(GameObject element in path)
			{
				element.GetComponent<SpriteRenderer>().color = Color.green;
			}
           	x = tempObj.GetComponent<GridStat>().x;
			y = tempObj.GetComponent<GridStat>().y;
			tempList.Clear();
		}
	}

    public void LerpToDestination(GameObject obj)
	{
		endX = obj.GetComponent<GridStat>().x;
		endY = obj.GetComponent<GridStat>().y;
		findDistance = true;
	}
}
