using UnityEngine;
using System.Collections;

/*
 * Listens to game score and displays in label.
 *
 * @author Alejandro Mostajo <amostajo@gmail.com>
 */
public class ScoreListener : MonoBehaviour {

	/**
	 * Prefix to attach to score.
	 */
	public string prefix = string.Empty;
	/**
	 * NGUI label where to display the score
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
		GameManager.On_ScoreUpdate += OnScoreUpdate;
	}

	/** 
	 * Unity OnDisable
	 */
	public void OnDisable () {
		GameManager.On_ScoreUpdate -= OnScoreUpdate;
	}

	/**
	 * On score update.
	 *
	 * @param float score Game Score.
	 */
	public void OnScoreUpdate (int score) {
		if (label) {
			label.text = string.Format("{0} {1}", prefix, score.ToString());
		}
	}
}
