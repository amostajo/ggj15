using UnityEngine;
using System.Collections;

/**
 * Listens to GUI changes.
 *
 * @author Alejandro Mostajo
 * @copyright 2014 Amsgames, LLC <amsgames.com>
 *
 * THIS SCRIPT WAS PROVIDED BY AMSGAMES, LLC.
 * YOU CAN USE THIS SCRIPT FREELY IF YOU GIVE ATTRIBUTION AND CREDIT TO ITS CREATOR (AMSGAMES).
 */
public class GUIListener : MonoBehaviour {

	private const float hideTime = 0.5f;

	/**
	 * States to listing to.
	 */
	public GUIManager.State[] states;

	/**
	 * Animator
	 */
	private Animator animator;

	/**
	 * Helper
	 */
	private int index;

	/**
	 * Timer for hide
	 */
	private float timer;

	/**
	 * On hide flag
	 */
	private bool onHide;

	/**
	 * Related panel
	 */
	private UIPanel panel;

	/**
	 * Unity Awake
	 */
	public void Awake() {
		animator = GetComponent<Animator>();
		panel = GetComponent<UIPanel>();
	}

	/**
	 * Unity OnEnable
	 */
	public void OnEnable () {
		GUIManager.On_StateChange += OnStateChange;
	}

	/**
	 * Unity OnEnable
	 */
	public void OnDisable () {
		GUIManager.On_StateChange -= OnStateChange;
	}

	/**
	 * Check if needs to hide
	 */
	public void FixedUpdate () {
		if (!GameManager.paused && onHide) {
			timer += Time.deltaTime;
			if (timer >= hideTime) {
				onHide = false;
				panel.enabled = false;
			}
		}
	}

	/**
	 * Listens to event on state change.
	 *
	 * @param GUIManager.State state
	 */
	public void OnStateChange (GUIManager.State state) {
		bool show = false;
		for (index = states.Length - 1; index >= 0; --index) {
			if (states[index] == state) {
				show = true;
				break;
			}
		}
		if (show) {
			onHide = false;
			panel.enabled = true;

		} else {
			onHide = true;
			timer = 0f;
		}
		if (animator) {
			animator.SetBool("show", show);
		} else {
			panel.enabled = show;
		}
	}

}
