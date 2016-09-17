﻿using UnityEngine;
using System.Collections;
using System;

public class BaseCharacter : MonoBehaviour {
	private string _name;
	private int _level;
	private uint _freeExp;

	private Attribute[] _primaryAttribute;
	private Vital[] _vital;
	private Skill[] _skill;

	void Awake(){
		_name = string.Empty;
		_level = 0;
		_freeExp = 0;

		_primaryAttribute = new Attribute[Enum.GetValues(typeof(AttributeName)).Length];
		_vital = new Vital[Enum.GetValues(typeof(VitalName)).Length];
		_skill = new Skill[Enum.GetValues(typeof(SkillName)).Length];

		SetupPrimaryAttributes();
		SetupVitals();
		SetupSkills();
	}

	public string Name{
		get{ return _name; }
		set{ _name = value; }
	}

	public int Level{
		get{ return _level; }
		set{ _level = value; }
	}

	public uint FreeExp{
		get{ return _freeExp; }
		set{ _freeExp = value; }
	}

	public void AddExp(uint exp){
		_freeExp += exp;

		CalculateLevel();
	}

	public void CalculateLevel(){

	}

	private void SetupPrimaryAttributes(){
		for(int cnt = 0; cnt < _primaryAttribute.Length; cnt++){
			_primaryAttribute[cnt] = new Attribute();
		}
	}

	private void SetupVitals(){
		for(int cnt = 0; cnt < _vital.Length; cnt++){
			_vital[cnt] = new Vital();
		}
	}

	private void SetupSkills(){
		for(int cnt = 0; cnt < _skill.Length; cnt++){
			_skill[cnt] = new Skill();
		}
	}

	public Attribute GetPrimaryAttribute(int index){
		return _primaryAttribute[index];
	}

	public Vital GetVital(int index){
		return _vital[index];
	}

	public Skill GetSkill(int index){
		return _skill[index];
	}

	private void SetupVitalModifiers(){
		//Heatlh
		GetVital((int)VitalName.Health).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Vitality), 1.5f));
		//Mana
		GetVital((int)VitalName.Mana).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Intelligence), 1f));
		//Energy
		GetVital((int)VitalName.Energy).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Agility), 1f));
	}

	private void SetupSkillModifiers(){
		GetSkill((int)SkillName.Melee).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Vitality), 0.33f));
		//Ranged
		GetSkill((int)SkillName.Ranged).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Agility), 0.5f));
		//Magic
		GetSkill((int)SkillName.Magic).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Intelligence), 0.7f));
	}

	public void StatUpdate(){
		for(int cnt = 0; cnt < _vital.Length; cnt++)
			_vital[cnt].Update();
		for(int cnt = 0; cnt < _skill.Length; cnt++)
			_skill[cnt].Update();
	}

}
