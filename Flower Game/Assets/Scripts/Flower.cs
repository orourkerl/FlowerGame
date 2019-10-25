using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
	public int flowerLevel = 1;
    public enum FlowerNeeds { Shovel, Water, Sunlight, Fertilizer, Nothing};

	public GameObject toolSpawner;
	public GameObject flowerSpawner;
	public GameObject gameManager;
	public Sprite shovelSprite;
	public Sprite waterSprite;
	public Sprite sunSprite;
	public Sprite fertilizerSprite;
	public Sprite flowerLevel1Sprite;
	public Sprite flowerLevel2Sprite;
	public Sprite flowerLevel3Sprite;
	public Sprite flowerLevel4Sprite;
	public Sprite flowerLevel5Sprite;
	public FlowerNeeds currentNeeds = FlowerNeeds.Nothing;
	public static FlowerNeeds[] needsArray = {
		FlowerNeeds.Shovel,
		FlowerNeeds.Water,
		FlowerNeeds.Sunlight,
		FlowerNeeds.Fertilizer
	};
    LinkedList<FlowerNeeds> needsLinkedList = new LinkedList<FlowerNeeds>(needsArray);
	public GameObject thoughtBubble;

    int timeBeforeNeeding;
	public int timeBeforeDying;

    void OnTriggerEnter2D(Collider2D collider)
	{
		print("collided with " + collider.gameObject.name);
		if (currentNeeds == FlowerNeeds.Fertilizer && collider.gameObject.tag == "Fertilizer")
		{
			currentNeeds = FlowerNeeds.Nothing;
			Destroy(collider.gameObject);
		}
		if (currentNeeds == FlowerNeeds.Water && collider.gameObject.tag == "Water")
		{ 
			currentNeeds = FlowerNeeds.Nothing;
			Destroy(collider.gameObject);
		}
		if (currentNeeds == FlowerNeeds.Shovel && collider.gameObject.tag == "Shovel")
		{
			currentNeeds = FlowerNeeds.Nothing;
			Destroy(collider.gameObject);
		}
		if (currentNeeds == FlowerNeeds.Sunlight && collider.gameObject.tag == "Sunlight")
		{
			currentNeeds = FlowerNeeds.Nothing;
			Destroy(collider.gameObject);
		}
	}

    void Update()
	{ 
        switch(flowerLevel)
		{
			case 1:
				GetComponent<SpriteRenderer>().sprite = flowerLevel1Sprite;
				break;
			case 2:
				GetComponent<SpriteRenderer>().sprite = flowerLevel2Sprite;
				break;
			case 3:
				GetComponent<SpriteRenderer>().sprite = flowerLevel3Sprite;
				break;
			case 4:
				GetComponent<SpriteRenderer>().sprite = flowerLevel4Sprite;
				break;
			case 5:
				GetComponent<SpriteRenderer>().sprite = flowerLevel5Sprite;
				break;
		}
        switch(currentNeeds)
		{
			case FlowerNeeds.Nothing:
				thoughtBubble.SetActive(false);
				break;
			case FlowerNeeds.Fertilizer:
				thoughtBubble.SetActive(true);
				thoughtBubble.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = fertilizerSprite;
				break;
			case FlowerNeeds.Shovel:
				thoughtBubble.SetActive(true);
				thoughtBubble.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = shovelSprite;
				break;
			case FlowerNeeds.Sunlight:
				thoughtBubble.SetActive(true);
				thoughtBubble.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sunSprite;
				break;
			case FlowerNeeds.Water:
				thoughtBubble.SetActive(true);
				thoughtBubble.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = waterSprite;
				break;
		}
	}

    void Start()
	{
		timeBeforeNeeding = Random.Range(0, 10);
		StartCoroutine(FlowerRoutine(needsLinkedList.First));
	}

    IEnumerator FlowerRoutine(LinkedListNode<FlowerNeeds> needLink)
	{
		yield return new WaitForSeconds(timeBeforeNeeding);
		currentNeeds = needLink.Value;
        for (float timer = timeBeforeDying; timer >= 0; timer -= Time.deltaTime)
		{
            if (currentNeeds == FlowerNeeds.Nothing)
			{
				flowerLevel++;
				break;
			}
			yield return null;
		}
	    if (currentNeeds != FlowerNeeds.Nothing)
	    {
		    Die();
	    }
		if (needLink.Next != null)
		{
			StartCoroutine(FlowerRoutine(needLink.Next));
		}

	}

    void Die()
	{
		print(this.ToString() + " has died");
		//penalize player
		Destroy(this.gameObject);
	}
    
}
