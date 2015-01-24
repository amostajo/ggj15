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
	 * Inputs
	 */
	[HideInInspector]
	public InputManager inputs;

	/**
	 * Unity Awake
	 */
	public virtual void Awake () {
		inputs = FindObjectOfType<InputManager>();
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
}
