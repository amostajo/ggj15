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
	 * Turn speed.
	 */
	public float turnTime;

	/**
	 * Movement
	 */
	private Vector3 movement;

	/**
	 * Movement
	 */
	private bool onLookAt;

	/**
	 * Helper
	 */
	private float timer;

	/**
	 * Character controller reference.
	 */
	private CharacterController controller;

	/**
	 * Old rotation reference.
	 */
	private Quaternion oldRotation;

	/**
	 * New rotation reference.
	 */
	private Quaternion newRotation;

	/**
	 * Animator.
	 */
	private Animator animator;

	/**
	 * Candle object.
	 */
	private GameObject candle;

	/**
	 * Unity Awake
	 */
	public void Awake () {
		controller = GetComponent<CharacterController>();
		animator = GetComponentInChildren<Animator>();
		candle = GameObject.FindWithTag(GameManager.tagCandle);
	}

	/**
	 * Unity start
	 */
	public void Start () {
		timer = 0f;
	}

	/**
	 * Unity update
	 */
	public void FixedUpdate () {
		if (!GameManager.paused) {
			if (controller && controller.enabled) {
				controller.Move(movement * speed * Time.deltaTime);
				// Animation
				animator.SetBool("walk", movement != Vector3.zero);
				if (onLookAt) {
					timer += Time.deltaTime;
					transform.rotation = Quaternion.Lerp(oldRotation, newRotation, timer / turnTime);
					if (timer >= turnTime) {
						onLookAt = false;
					}
				}
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
		onLookAt = true;
		timer = 0f;
		oldRotation = transform.rotation;
		transform.LookAt(transform.localPosition + direction);
		newRotation = transform.rotation;	
	}

	/**
	 * Makes character sit.
	 */
	public void Sit () {
		if (animator) {
			animator.SetBool("sit", true);
			animator.SetLayerWeight(1, 0f);
		}
		HideCandle();
	}

	/**
	 * Makes character unsit.
	 */
	public void Unsit () {
		if (animator) {
			animator.SetBool("sit", false);
			animator.SetLayerWeight(1, 1f);
		}
	}

	/**
	 * Hides candle
	 */
	public void HideCandle () {
		if (candle) {
			candle.SetActive(false);
		}
	}

	/**
	 * Show candle
	 */
	public void ShowCandle () {
		if (candle && !candle.activeSelf) {
			candle.SetActive(true);
		}
	}

}
