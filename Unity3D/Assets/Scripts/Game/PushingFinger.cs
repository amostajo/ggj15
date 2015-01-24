using UnityEngine;
using System.Collections;

//@author Daniel Vuurman <danielvuurman@hotmail.com>

public class PushingFinger : MonoBehaviour {

    //Assigns horizontal movement to finger
    private static Vector3 direction = new Vector3(1f, 0f, 0f);

    //Variables assigned
    public float frequency;

    public float force;

    public float forceFactor;

    private float timer;

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
}
