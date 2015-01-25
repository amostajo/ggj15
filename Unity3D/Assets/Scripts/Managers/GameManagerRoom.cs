using UnityEngine;
using System.Collections;

/*
 * Room game manager.
 *
 * @author Alejandro Mostajo <amostajo@gmail.com>
 */
public class GameManagerRoom : GameManager {

	/**
	 * Character.
	 */
	[HideInInspector]
	public Character character;

	/**
	 * Position where character will spawn the first time.
	 */
	public Vector3 startPosition;

	/**
	 * Rotation where character will spawn the first time.
	 */
	public Vector3 startRotation;

	/**
	 * Saved position where character should spawn.
	 */
	private Vector3 savedPosition;

	/**
	 * Saved rotation where character should spawn.
	 */
	private Vector3 savedRotation;

	/**
	 * Unity Awake
	 */
	public override void Awake () {
		base.Awake();
		this.character = FindObjectOfType<Character>();
		savedPosition = new Vector3(
				PlayerPrefs.GetFloat("savedPositionX"),
				PlayerPrefs.GetFloat("savedPositionY"),
				PlayerPrefs.GetFloat("savedPositionZ")
		);
		savedRotation = new Vector3(
				PlayerPrefs.GetFloat("savedRotationX"),
				PlayerPrefs.GetFloat("savedRotationY"),
				PlayerPrefs.GetFloat("savedRotationZ")
		);
		if (!GameManager.started) {
			GameManager.started = true;
			savedPosition = startPosition;
			savedRotation = startRotation;
			PlayerPrefs.SetInt(GameManager.Minigame.toys.ToString(), 0);
			PlayerPrefs.SetInt(GameManager.Minigame.fire.ToString(), 0);
			PlayerPrefs.SetInt(GameManager.Minigame.guitar.ToString(), 0);
			PlayerPrefs.SetInt(GameManager.Minigame.pencil.ToString(), 0);
			PlayerPrefs.SetInt(GameManager.Minigame.haki.ToString(), 0);
			PlayerPrefs.SetInt("score", 0);
			PlayerPrefs.SetFloat("gameTime", 0f);
			this.score = 0;
		}
	}

	/** 
	 * Unity start.
	 */
	public override void Start () {
		base.Start();
		if (character) {
			character.transform.localPosition = savedPosition;
			character.transform.Rotate(savedRotation);
		}
	}

	/**
	 * Fixed update.
	 */
	public override void FixedUpdate () {
		base.FixedUpdate();
		if (!GameManager.paused) {
			// Character movement
			if (character) {
				character.SetMovement(inputs.movement);
			}
		}
	}

}
