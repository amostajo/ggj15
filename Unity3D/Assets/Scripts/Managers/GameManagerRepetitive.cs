using UnityEngine;
using System.Collections;

//@author Daniel Vuurman <danielvuurman@hotmail.com>

public class GameManagerRepetitive : GameManager {

    //Call PushingObject Script, set pusher
    private PushingObject pusher;

	//Call PushingObject Script, set pusher
	private DestructibleObject destruct;

    //Direction Vector to return Finger
    public static Vector3 direction = new Vector3(-1f, 0f, 0f);

    public float force;

    public float forceFactor;

	public int lastPoints=5;

    public override void Awake()
    {
        base.Awake();
        pusher = FindObjectOfType<PushingObject>();
		destruct = FindObjectOfType<DestructibleObject>();
    }

    public override void Start()
    {
        inputs.RequestKey();
        //Fixing needed, Error on recieving value from  Game Manager On_TimerChange(0f);
    }

    //Update checks for Key sent and Key Pressed
    //Ends game on mistake
    public void Update()
    {
        if (!GameManager.paused && this.inputs.keyCheck) {
            if (this.inputs.keySuccess)
            {
                AddScore(1);
                if (pusher && pusher.rigidbody)
                {
                    pusher.rigidbody.AddForce(direction*force);
                    force += force*forceFactor;
                }
				else if(destruct && destruct.health <= 0){
						destruct.isDestroyed = true;
						AddScore(lastPoints);
						Finish();
				}
				else{
					if(destruct && destruct.health >= 0 && !destruct.isDestroyed){
						destruct.rigidbody.AddForce(destruct.direction * force);
						force += force*forceFactor;
					}
				}
            }
            else
            {
                Finish();
            }
        }
      
    }


}
