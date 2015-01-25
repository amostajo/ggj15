using UnityEngine;
using System.Collections;

public class DestructibleObject : MonoBehaviour {
	
	
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

	// Use this for initialization
	public void Start () {
		game.inputs.RequestKey ();
        timer = 0f;
        if (rigidbody)
        {
            rigidbody.AddForce(-direction*impact);
        }
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

	public void FixedUpdate () 
    {
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
