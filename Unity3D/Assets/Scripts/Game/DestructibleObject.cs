using UnityEngine;
using System.Collections;

public class DestructibleObject : MonoBehaviour {
	
	
	private GameManager game;


	public float health=100F;
	
	public float reduceHealth=2F;

	public bool isDestroyed = false;
	
	public float force;
	
	public float forceFactor;

	public float impact;

	public float impactFactor;

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

}
