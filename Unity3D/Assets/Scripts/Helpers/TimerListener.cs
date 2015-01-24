using UnityEngine;
using System.Collections;

/*
 * Listens to game timer and displays in label.
 *
 * @author Alejandro Mostajo <amostajo@gmail.com>
 */
public class TimerListener : MonoBehaviour {
	/**
	 * NGUI progress bar
	 */
	private UISlider slider;

	/**
	 * Unity Awake
	 */
	public void Awake () {
		slider = GetComponent<UISlider>();
	}

	/** 
	 * Unity OnEnable
	 */
	public void OnEnable () {
		GameManager.On_TimerChange += OnTimerChange;
	}

	/** 
	 * Unity OnDisable
	 */
	public void OnDisable () {
		GameManager.On_TimerChange -= OnTimerChange;
	}

	/**
	 * On key timer change.
	 *
	 * @param float ratio.
	 */
	public void OnTimerChange (float ratio) {
		if (slider) {
			slider.value = ratio;
		}
	}
}
