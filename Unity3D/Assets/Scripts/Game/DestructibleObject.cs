using UnityEngine;
using System.Collections;

public class DestructibleObject : MonoBehaviour {
	
	
	private GameManager game;


	public float health=10F;
	
	public float reduceHealth=1F;

	public bool isDestroyed = false;
	
	public float force;
	
	public float forceFactor;

	public float impact;

	public float impactFactor;

	public bool collisionWall;

	//Assigns horizontal movement to finger
	public Vector3 direction = new Vector3(-1f, 0f, 0f);
	
	public void Awake () {
		game = GameManager.Get();
	}

	// Use this for initialization
	public void Start () {
		game.inputs.RequestKey ();
	}
	
	// Update is called once per frame
	public void Update () {
				if (!GameManager.paused && game.inputs.keyCheck) {
						if (game.inputs.keySuccess) {
								rigidbody.AddForce (-direction * force);
								force += force * forceFactor;
						}
				}
		}

	public void FixedUpdate () {
		/*rigidbody.AddForce(direction * force);
		force += force * forceFactor;*/
	}

	public void OnCollisionEnter(){
		this.health -= reduceHealth;
		rigidbody.AddForce (-direction * impact);
		impact += impact * impactFactor;
	}

	public void OnTriggerEnter(Collider other) {
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
	}



}
