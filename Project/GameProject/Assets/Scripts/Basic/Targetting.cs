﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Targetting : MonoBehaviour {
	public List<Transform> targets;
	public Transform selectedTarget;

	private Transform myTransform;

	// Use this for initialization
	void Start () {
		targets = new List<Transform>();
		selectedTarget = null;
		myTransform = transform;

		AddAllEnemies();
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Tab)){
			TargetEnemy();
		}
	}

	public void AddAllEnemies(){
		GameObject[] go = GameObject.FindGameObjectsWithTag("Enemy");

		foreach(GameObject enemy in go){
			AddTarget(enemy.transform);
		}
	}

	public void AddTarget(Transform enemy){
		targets.Add(enemy);
	}


	private void TargetEnemy(){
		if(selectedTarget == null){
			SortTargetsByDistance();
			selectedTarget = targets[0];
			SelectTarget();
		}else{
			int index = targets.IndexOf(selectedTarget);

			if(index < targets.Count - 1){
				index++;
			}else{
				index = 0;
			}
			DeselectTarget();
			selectedTarget = targets[index];
			SelectTarget();
		}
	}

	private void SortTargetsByDistance(){
		if(targets.Count>0){
			targets.Sort(
				delegate(Transform t1, Transform t2){
					return Vector3.Distance(t1.position, myTransform.position).CompareTo(Vector3.Distance(t2.position, myTransform.position));
					}
				);
		}else{
			AddAllEnemies();
			SortTargetsByDistance();
		}
	}

	private void SelectTarget(){
		//Renderer er = (Renderer)selectedTarget.GetComponent("Renderer");
		//er.material.color = Color.green;

		PlayerAttack pa = (PlayerAttack)GetComponent("PlayerAttack");
		pa.target = selectedTarget.gameObject;
	}

	private void DeselectTarget(){
		selectedTarget = null;
	}

}