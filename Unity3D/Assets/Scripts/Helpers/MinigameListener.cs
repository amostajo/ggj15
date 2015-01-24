using UnityEngine;
using System.Collections;

/*
 * Listens to unlocked minigames.
 *
 * @author Alejandro Mostajo <amostajo@gmail.com>
 */
public class MinigameListener : MonoBehaviour {

	/**
	 * Mini game to listen to.
	 */
	public GameManager.Minigame minigame; 

	/**
	 * Unity Awake.
	 * Checks on played minigame for disabling.
	 */
	public void Awake () {
		if (PlayerPrefs.GetInt(minigame.ToString()) != 1) {
			gameObject.SetActive(false);
		}
	}

}
