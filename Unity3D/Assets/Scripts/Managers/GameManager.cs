﻿using UnityEngine;
using System.Collections;

/*
 * Input Manager. Handles all inputs in the system.
 *
 * @author Alejandro Mostajo <amostajo@gmail.com>
 */
public class GameManager : MonoBehaviour {

	// Minigame
	public enum Minigame {none = -1, toys = 0, guitar = 1, fire = 2, pencil = 3, haki = 4};

	/**
	 * On score update event.
	 */
  public delegate void OnScoreUpdate (int score);
  public static event OnScoreUpdate On_ScoreUpdate;

	/**
	 * On key timer change event.
	 */
  public delegate void OnTimerChange (float ratio);
  public static event OnTimerChange On_TimerChange;

	/**
	 * Flag that indicates if game is paused.
	 */
	public static bool paused = false;

	/**
	 * Flag that indicates if game is started.
	 */
	public static bool started = false;

	/**
	 * Current minigame selected.
	 */
	public static GameManager.Minigame minigame;

	/**
	 * Flag that indicates if game should count game timer.
	 */
	public bool hasGlobalTimer;

	/**
	 * Global time of the game. Not to use in mini games.
	 */
	public float globalTime;

	/**
	 * Inputs
	 */
	[HideInInspector]
	public InputManager inputs;

	/**
	 * Score.
	 */
	[HideInInspector]
	public int score;

	/**
	 * Game time.
	 */
	[HideInInspector]
	public float timer;

	/** 
	 * Returns game manager in scene.
	 */
	public static GameManager Get () {
		return FindObjectOfType<GameManager>();
	}

	/**
	 * Unity Awake
	 */
	public virtual void Awake () {
		this.inputs = FindObjectOfType<InputManager>();
		this.score = PlayerPrefs.GetInt("score");
	}

	/**
	 * Unity start.
	 */
	public virtual void Start () {
		if (hasGlobalTimer) {
			this.timer = PlayerPrefs.GetFloat("gameTime");
			On_TimerChange(0f);
		}
		On_ScoreUpdate(this.score);
	}

	/**
	 * Fixed update.
	 */
	public virtual void FixedUpdate () {
		if (!GameManager.paused) {
			// Global time
			if (hasGlobalTimer) {
				this.timer += Time.deltaTime;
				On_TimerChange(1f - (timer / globalTime));
				if (this.timer >= globalTime) {
					End();
				}
			}
		}
	}

	/**
	 * Called when game is paused.
	 */
	public virtual void OnPause () {
		// TODO
	}

	/**
	 * Called when game is un paused.
	 */
	public virtual void OnUnPause () {
		// TODO
	}

	/**
	 * Adds score points.
	 *
	 * @param int points.
	 */
	public void AddScore (int points) {
		this.score += points;
		On_ScoreUpdate(this.score);
	}

	/**
	 * Called when game has finished.
	 */
	public virtual void Finish () {
		PlayerPrefs.SetInt("score", score);
		if (hasGlobalTimer) {
			PlayerPrefs.SetFloat("gameTime", timer);
		}
	}

	/**
	 * To be called when game is over..
	 */
	public virtual void End () {
		// TODO
	}

	/**
	 * To be called by extended classes.
	 *
	 * @param float ratio Time change ratio where 1f is full and 0f is not.
	 */
	public void TimerChange (float ratio) {
		On_TimerChange(ratio);
	}
}
