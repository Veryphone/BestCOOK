﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AchievementsPanel : MonoBehaviour {


	public Text totalCoinsText;
	
	public Text totalGoldText;

	// Use this for initialization
	void Start () {
		totalGoldText.text = MenuManager.golds.ToString ();
		totalCoinsText.text = MenuManager.totalscore.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void Close()
	{

		if(MenuManager._instance != null)
			MenuManager._instance.EnableFadePanel ();
		else
		{
			UIManager._instance.gameOverPanel.SetActive (true);
			UIManager._instance.EnableFadePanel ();
		}
		Destroy (gameObject);
	}
}
