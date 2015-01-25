using UnityEngine;
using System.Collections;

public class DestructibleObject : MonoBehaviour {
	
	//Assigning variables
	private GameManager game;

	public float health=10F;
	
	public float reduceHealth=1F;

	public bool isDestroyed = false;

    public float frequency;
	
	public float force;
	
	public float forceFactor;

	public float impact;

	public float impactFactor;

    private float timer;

	public bool collisionWall;

	//Assigns horizontal movement to finger
	public Vector3 direction = new Vector3(0f, 0f, 1f);
	
	public void Awake () {
		game = GameManager.Get();
	}

	// Sends initial key and starts timer. Also sets the initial impact force
	public void Start () {
		game.inputs.RequestKey ();
        timer = 0f;
        if (rigidbody)
        {
            rigidbody.AddForce(-direction*impact);
        }
	}
	
	// increases force per correct input
	public void Update () {
				if (!GameManager.paused && game.inputs.keyCheck) {
						if (game.inputs.keySuccess) {
								rigidbody.AddForce (-direction * force);
								force += force * forceFactor;
						}
				}
		}

	public void FixedUpdate () 
    {
        //Sets timer for game end and frequency for impact gain
        if (!GameManager.paused && rigidbody)
        {
            timer += Time.deltaTime;
            if (timer >= frequency)
            {
                timer = 0f;
                rigidbody.AddForce(-direction * impact);
                impact += impact * impactFactor;
            }
        }
        //This is for the game to end if the player doesn't manage contact between the blocks
        if (!OnCollisionEnter && timer = timer + 10f)
        {
            Finish();
        }
	}


    //This method causes damage to be inflicted to both blocks and the forces to become stronger on contact
	public void OnCollisionEnter(){
		this.health -= reduceHealth;
		rigidbody.AddForce (-direction * impact);
		impact += impact * impactFactor;
	}

    //Sets velocity to zero on Walls to give more chance to players and attempt to avoid wall passing glitch
	public void OnTriggerEnter(Collider other) {
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
	}



}
