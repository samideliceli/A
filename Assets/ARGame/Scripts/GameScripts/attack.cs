using UnityEngine;
using System.Collections;

public class attack : MonoBehaviour {

	private int damage;
	// Use this for initialization
	void Start () {
	
	}

	public void setDamage(int dmg){
		damage = dmg;
	}

	public int getDamage(){
		return damage;
	}

}
