using UnityEngine;
using System.Collections;

[System.Serializable]
public class Improvements : ScriptableObject {
	public string imprName = "Name here";
	public int cost = 50;
	public string description;

	public bool healthBar = false;
	public bool machineGun = false;
	public float fireRate = 0.5f;
	public int damage = 10;
	public int health = 10;
	public GameObject imprObj;
	public GameObject bow;
	public GameObject arrow;




}
