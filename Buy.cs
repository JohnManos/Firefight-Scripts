using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buy : MonoBehaviour {

	public GameObject shop;
	public GameObject notEnough;
	public GameObject confirmation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void BuyItem()
	{
		if (PlayerStats.GetPoints() >= 4)
		{
			shop.SetActive(false);
			confirmation.SetActive(true);
		}
		else
		{
			shop.SetActive(false);
			notEnough.SetActive(true);
		}
	}
}
