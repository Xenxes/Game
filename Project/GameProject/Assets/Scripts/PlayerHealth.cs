﻿using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public int maxHealth = 200;
	public int curHealth = 200;

	public float healthBarLength;
	// Use this for initialization
	void Start () {
		healthBarLength = Screen.width / 4;
	}

	// Update is called once per frame
	void Update () {
		AddjustCurrHealth(0);
	}

	void OnGUI(){
		GUI.Box(new Rect(10, 10, healthBarLength, 20), curHealth + "/" + maxHealth);
	}

	public void AddjustCurrHealth(int adj){
		curHealth += adj;

		if(curHealth < 0){
			curHealth = 0;
		}

		if(curHealth > maxHealth){
			curHealth = maxHealth;
		}
		healthBarLength = (Screen.width / 4) * (curHealth / (float)maxHealth);
	}

}