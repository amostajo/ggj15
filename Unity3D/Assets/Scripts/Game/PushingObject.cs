using UnityEngine;
using System.Collections;

//@author Daniel Vuurman <danielvuurman@hotmail.com>

public class PushingObject : MonoBehaviour {

    //Assigns horizontal movement to finger
    private Vector3 direction = new Vector3(1f, 0f, 0f);

    //Variables assigned
    public float frequency;

    public float force;

    public float forceFactor;

    private float timer;

    private GameManager game;

    void Awake()
    {
        game = GameManager.Get();
    }

	void Start () {
        timer = 0f;
        if (rigidbody)
        {
            rigidbody.AddForce(direction * force);
        }

	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (!GameManager.paused && rigidbody)
        {
            timer += Time.deltaTime;
            if (timer >= frequency)
            {
                timer = 0f;
                rigidbody.AddForce(direction * force);
                force += force * forceFactor;
            }
        }
	
	}

    //This will cause the colliding object to stop upon contact with the trigger
    void OnTriggerEnter(Collider other)
    {
        game.Finish();
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.isKinematic = true;
    }
}
