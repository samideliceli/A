using UnityEngine;
using System.Collections;

public class gameController : MonoBehaviour {

	public GameObject hero1,hero2, hero3, hero4,hero5, hero6;

	bool isGameReadyToGo = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (hero1.activeSelf && hero2.activeSelf && hero3.activeSelf && hero4.activeSelf && hero5.activeSelf && hero6.activeSelf) {

			isGameReadyToGo = true;

		} else {
			isGameReadyToGo = false;
		}
	}

	public bool isGameReady(){
		return isGameReadyToGo;
	}

	public void setGameReadyToFalse(){
		isGameReadyToGo = false;
	}
}
