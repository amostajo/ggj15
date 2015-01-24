using UnityEngine;
using System.Collections;

/*
 * Scene character.
 *
 * @author Alejandro Mostajo <amostajo@gmail.com>
 */
public class Character : MonoBehaviour {

	/**
	 * Movement Speed.
	 */
	public float speed;

	/**
	 * Movement
	 */
	[HideInInspector]
	private Vector3 movement;
	/**
	 * Character controller reference.
	 */
	private CharacterController controller;

	/**
	 * Unity Awake
	 */
	public void Awake () {
		controller = GetComponent<CharacterController>();
	}

	/**
	 * Unity update
	 */
	public void FixedUpdate () {
		if (!GameManager.paused) {
			if (controller && controller.enabled) {
				controller.Move(movement * speed * Time.deltaTime);
			}
		}
	}

	/**
	 * Sets movement.
	 *
	 * @param Vector3 direction.
	 */
	public void SetMovement (Vector3 direction) {
		this.movement = direction;
	}
}
