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

public float timeToLose = 1.5F;

public float changeTime = 1F;

public float timelimit = 0.5F;

private float changeTimer = 1F;

public override void Awake()
{
    base.Awake();
    pusher = FindObjectOfType<PushingObject>();
	destruct = FindObjectOfType<DestructibleObject>();
}

public override void Start()
{
    GameManager.paused = true;
    inputs.RequestKey();
    if (GameManager.minigame == 0){
        timer = 0f;
    }
    //Fixing needed, Error on recieving value from  Game Manager On_TimerChange(0f);
}

//Update checks for Key sent and Key Pressed
//Ends game on mistake
public void Update()
{
    if (GameManager.paused && this.inputs.keyCheck)
    {
        if (this.inputs.keySuccess)
        {
            GameManager.paused = false;
            if (pusher && pusher.rigidbody)
            {
                pusher.force = -5;
            }
        }
    }
		/*
		 * if game is not paused and theres a input check
		 */
    if  (!GameManager.paused && this.inputs.keyCheck) {
        if (this.inputs.keySuccess)
        {
			/*for finger mini game*/
            AddScore(1);
            if (pusher && pusher.rigidbody)
            {
                pusher.rigidbody.AddForce(direction*force);
                force += force*forceFactor;
            }
			/*for toys mini game */
			else if(destruct && destruct.health <= 0){
					destruct.isDestroyed = true;
					AddScore(lastPoints);
					Finish();
			}
			else{
				if(destruct && destruct.health >= 0 && !destruct.isDestroyed){
					destruct.rigidbody.AddForce(destruct.direction * force);
					force += force*forceFactor;
						//if there 
                    if (destruct.collisionExist){
                        timer = 0f;
                        destruct.collisionExist = false;
                   }
				}
			}
        }
        else
        {
            Finish();
        }
    }
      
}

public void FixedUpdate()
{
    if (!GameManager.paused)
    {
        timer += Time.deltaTime;
        changeTimer += Time.deltaTime;
        TimerChange(1f - (timer / timeToLose));

        if (timer >= timeToLose)
        {
            this.Finish();
        }

            if (timeToLose < timelimit)
            {
                timeToLose = timelimit;
            }
        }
    }
}
