using UnityEngine;
using System.Collections;

public class TriggerListener : MonoBehaviour {
	/**
	 * NGUI label where to display the key
	 */
	private UILabel label;

	/**
	 * Unity Awake
	 */
	public void Awake () {
		label = GetComponent<UILabel>();
	}

	/** 
	 * Unity OnEnable
	 */
	public void OnEnable () {
		MinigameTrigger.On_MinigameTriggered += OnMinigameTriggered;
	}

	/** 
	 * Unity OnDisable
	 */
	public void OnDisable () {
		MinigameTrigger.On_MinigameTriggered -= OnMinigameTriggered;
	}

	/**
	 * On score update.
	 *
	 * @param float score Game Score.
	 */
	public void OnMinigameTriggered (GameManager.Minigame minigame, string message) {
		if (label) {
			label.enabled = true;
			label.text = message;
		}
	}
}
