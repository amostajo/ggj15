using UnityEngine;
using System.Collections;

/*
 * Input Manager. Handles all inputs in the system.
 *
 * @author Alejandro Mostajo <amostajo@gmail.com>
 */
public class GameManager : MonoBehaviour {

	/**
	 * Flag that indicates if game is paused.
	 */
	public static bool paused = false;

	/**
	 * Flag that indicates if game should count game timer.
	 */
	public bool hasGameTimer;

	/**
	 * Global time of the game. Not to use in mini games.
	 */
	public float globalTime;

	/**
	 * Inputs
	 */
	[HideInInspector]
	public InputManager inputs;

	/**
	 * Score.
	 */
	[HideInInspector]
	public int score;

	/**
	 * Game time.
	 */
	[HideInInspector]
	public float timer;

	/** 
	 * Returns game manager in scene.
	 */
	public static GameManager Get () {
		return FindObjectOfType<GameManager>();
	}

	/**
	 * Unity Awake
	 */
	public virtual void Awake () {
		this.inputs = FindObjectOfType<InputManager>();
		this.score = PlayerPrefs.GetInt("score");
	}

	/**
	 * Unity start.
	 */
	public virtual void Start () {
		if (hasGameTimer) {
			this.timer = PlayerPrefs.GetFloat("gameTime");
		}
	}

	/**
	 * Fixed update.
	 */
	public virtual void FixedUpdate () {
		if (hasGameTimer && !GameManager.paused) {
			this.timer += Time.deltaTime;
			if (this.timer >= globalTime) {
				End();
			}
		}
	}

	/**
	 * Called when game is paused.
	 */
	public virtual void OnPause () {
		// TODO
	}

	/**
	 * Called when game is un paused.
	 */
	public virtual void OnUnPause () {
		// TODO
	}

	/**
	 * Adds score points.
	 *
	 * @param int points.
	 */
	public void AddScore (int points) {
		this.score += points;
	}

	/**
	 * Called when game has finished.
	 */
	public virtual void Finish () {
		PlayerPrefs.SetInt("score", score);
		if (hasGameTimer) {
			PlayerPrefs.SetFloat("gameTime", timer);
		}
	}

	/**
	 * To be called when game is over..
	 */
	public virtual void End () {
		// TODO
	}
}
