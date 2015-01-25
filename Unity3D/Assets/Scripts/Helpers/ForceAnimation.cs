using UnityEngine;
using System.Collections;

/*
 * Force animation to start.
 *
 * @author Alejandro Mostajo <amostajo@gmail.com>
 */
public class ForceAnimation : MonoBehaviour {

	/**
	 * Animation start.
	 */
	public string variable;

	/**
	 * Animator
	 */
	private Animator animator;

	/**
	 * Unity Awake
	 */
	public void Awake () {
		animator = GetComponent<Animator>();
	}

	/**
	 * Unity Start
	 */
	public void Start () {
		if (animator) {
			animator.SetBool(variable, true);
		}
	}

}
