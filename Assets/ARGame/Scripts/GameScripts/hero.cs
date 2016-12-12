using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class hero : MonoBehaviour {

	public GameObject target=null, fireBall=null;

	public Slider healthBar;

	public float speed;

	public int fireRate, maxDamage, minDamage;

	public ParticleSystem attackEffect;

	/// <summary>
	/// Public Yerel Değişken Ayracı
	/// </summary>

	List<GameObject> fires, firesYedek;

	GameObject fire= null, gameController;

	float nextFire, nextFireDistance, normalizedTime;

	Animator animator;

	bool isDead, saved;

	int hash;

	string currentAnimation = "Idle";

	attackSMB animSM;

	void Awake(){

		animator = GetComponent<Animator> ();
	}

	void Start () {
		
		fires = new List<GameObject> ();
		firesYedek = new List<GameObject> ();

		gameController = GameObject.Find ("Scene root");

		nextFire = Time.time + fireRate;
		nextFireDistance = nextFire - Time.time;


	}

	void Update() {
		
		normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;

		if (gameController.GetComponent<gameController> ().isGameReady ()) {

			if (target != null && fireBall != null && !target.GetComponent<hero> ().isDeadHero () && !isDead) {

				if (Time.time > nextFire) {

					animator.SetTrigger ("attack");

				} 
					
			}

			if (fires.Count > 0) {
				
				moveAttack ();

			}

			nextFireDistance = nextFire - Time.time;

		} else {
			
			nextFire = Time.time + nextFireDistance;
		}
	}

	public void newAttack(){

		int damage = Random.Range (minDamage, maxDamage);

		fire = (GameObject) Instantiate (fireBall, transform.position, Quaternion.identity);

		fire.GetComponent<attack> ().setDamage (damage);

		fires.Add (fire);
	}

	public void setNextFire(float duration){
		nextFire = Time.time + fireRate + duration;
	}

	void moveAttack(){
		
		foreach(GameObject fire in fires){

			if (fire != null) {
				float step = speed * Time.deltaTime;
				fire.transform.position = Vector3.MoveTowards (fire.transform.position, target.transform.position, step);
				firesYedek.Add (fire);
			}
		}

		fires.Clear ();

		foreach (GameObject fire in firesYedek) {
			fires.Add(fire);
		}

		firesYedek.Clear ();
	}


	void reduceHealth(Collider other){
		
		healthBar.value -= other.GetComponent<attack> ().getDamage ();

		animator.SetTrigger ("damaged");

		Destroy (other.gameObject);

		if(healthBar.value <= 0 && !isDead){
			animator.SetTrigger ("DamageDown");
			isDead = true;
			currentAnimation = "DamageDown";
		}
		
	}

	void OnEnable(){
		if(animator !=null && !isDead){
			animSM = animator.GetBehaviour<attackSMB> ();
			animSM.mnBehaviour = this;

			animator.Play (hash, 0, normalizedTime);
		}

		if(isDead){
			animator.Play (currentAnimation,0,1.0f);
		}
	}

	void OnDisable(){
		
		if (gameController != null) {
			gameController.GetComponent<gameController> ().setGameReadyToFalse ();
		}


	}

	public bool isDeadHero(){
		return isDead;
	}

	void OnTriggerEnter(Collider other ){
		if((target!=null && other.gameObject != fire.gameObject) || target==null){

			reduceHealth(other);

			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward, out hit))
			{
				GameObject newAttackParticle = (GameObject)Instantiate (attackEffect,hit.point,Quaternion.identity);
				//newAttackParticle.transform.parent = gameObject.transform;
			}
		}

	}

	public void setCurrentAnim (int fullPathHash)
	{
		hash = fullPathHash;
	}
}
