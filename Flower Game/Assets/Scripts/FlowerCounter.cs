using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowerCounter : MonoBehaviour
{
	GameObject[] flowers;
	int flowerCount = 0;
	public Text flowerText;
	public Text livesLeftText;
	public int startingLives = 3;
	public int livesLeft;
	public GameObject toolSpawner;
	public GameObject flowerSpawner;
	int maxFlowers;
    // Start is called before the first frame update
    void Start()
    {
		livesLeft = startingLives;
		maxFlowers = 0;
    }

    // Update is called once per frame
    void Update()
    {
		flowerCount = 0;
		flowers = GameObject.FindGameObjectsWithTag("Flower");
        foreach(GameObject flower in flowers)
		{
            if (flower.gameObject.GetComponent<Flower>().flowerLevel == 5)
			{
				flowerCount++;
			}
		}
		flowerText.text = "Flowers Collected: " + flowerCount;
		if (flowers.Length > maxFlowers) maxFlowers = flowers.Length;
		if (flowers.Length < maxFlowers)
		{
			livesLeft = livesLeft - (maxFlowers - flowers.Length);
			maxFlowers = flowers.Length;
		}

		livesLeftText.text = "Lives Remaining: " + Mathf.Clamp(livesLeft, 0, startingLives);
        if (livesLeft == 0)
		{
			toolSpawner.GetComponent<Spawner>().stop = true;
			flowerSpawner.GetComponent<Spawner>().stop = true;
		}
    }

    void DecrementLives()
	{
		livesLeft--;
	}
}
