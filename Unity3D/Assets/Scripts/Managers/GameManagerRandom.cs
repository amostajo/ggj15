using UnityEngine;
using System.Collections;

/*
 * GameManagerRandom. Sends randoms keys to gamer.
*
* @author Dario Rios <dario.rios@gmail.com>
*/
public class GameManagerRandom : GameManager {

	/*
	*  time pass to lose the game
	*/
	public float timeToLose = 1.5F;
	/*
	 * change the frecuency to show the letters
	*/
	public float changeFrecuency = 0.05F;

	/*
	* time limit put to the user to change 
	*/
	public float changeTime = 1F;

	/*
	* time that user expects to change
	*/
	private float changeTimer = 1F;

	/*
	* max time limit to show a key for the user 
	*/
	public float timelimit = 0.5F;
				
	/*
	* initialize variables
	*/ 
	public override void Start(){
		GameManager.paused = true;
		this.timer = 0F;
		this.inputs.RequestKey();
	}

	public void Update () {
		// if game is not paused and there's a key to check
		if (!GameManager.paused && this.inputs.keyCheck) {
			//if the user gets the inputs success
			if (this.inputs.keySuccess) {
				//add score because user sets the key and send another key to user
			  this.AddScore(1);
				timer = 0;
				this.inputs.RequestKey();
			} else {
				//if not success, end the game
				this.Finish();
			}		
		} else if (GameManager.paused) {
			if (this.inputs.keyCheck)	{
				if (this.inputs.keySuccess) {
				  this.AddScore(1);
					GameManager.paused = false;
					this.inputs.RequestKey();
				} else {
					Finish();
				}
			}
		}
	}

	public override void FixedUpdate ()
	{
	/*
	* if game is not paused
	*/
		if (!GameManager.paused) {
	/*
	* updates the time
	*/
			timer += Time.deltaTime;
			changeTimer += Time.deltaTime;
			TimerChange(1f - (timer / timeToLose));
	/*
	* checks if the timer is the same time to lose when user don't press anything
	*/ 
			if (timer >= timeToLose) {
				this.Finish();
			}
	/*
	* verify the change time from the user. change the frecuency every time the user get a key
	*/
			if(changeTimer >= changeTime) {
				changeTimer = 0f;
				timeToLose -= changeFrecuency;
	/*
	* if the timetolose equals the time limit   
	*/
				if (timeToLose < timelimit) {
					timeToLose = timelimit;
				}
			}
		}
	}

}
