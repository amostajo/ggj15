using UnityEngine;
using System.Collections;

/*
 * Listens to game key and displays in label.
 *
 * @author Alejandro Mostajo <amostajo@gmail.com>
 */
public class KeyListener : MonoBehaviour {
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
		InputManager.On_KeyUpdate += OnKeyUpdate;
	}

	/** 
	 * Unity OnDisable
	 */
	public void OnDisable () {
		InputManager.On_KeyUpdate -= OnKeyUpdate;
	}

	/**
	 * On score update.
	 *
	 * @param float score Game Score.
	 */
	public void OnKeyUpdate (string key) {
		if (label) {
			label.text = key;
		}
	}
}
