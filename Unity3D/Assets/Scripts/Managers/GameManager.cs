﻿using UnityEngine;
using System.Collections;

/*
 * Input Manager. Handles all inputs in the system.
 *
 * @author Alejandro Mostajo <amostajo@gmail.com>
 */
public class GameManager : MonoBehaviour {

	// Minigame
	public enum Minigame {none = -1, toys = 0, guitar = 1, fire = 2, pencil = 3, hacky = 4};

	// Character
	public static string tagCharacter = "character";

	// Mian candle
	public static string tagCandle = "candle";

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
     * Sound Effects for the game, when correct input
     */
    public AudioClip clipSuccess;

    /**
    * Sound Effects for the game, when incorrect input
    */
    public AudioClip clipFail;

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
	 * GUI manager reference.
	 */
	[HideInInspector]
	public GUIManager GUI;

	/**
	 * Flag that indicates if game hasFinished.
	 */
	[HideInInspector]
	public bool hasFinished;

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
		this.GUI = FindObjectOfType<GUIManager>();
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
			if (hasGlobalTimer && started) {
				this.timer += Time.deltaTime;
				On_TimerChange(1f - (timer / globalTime));
				if (this.timer >= globalTime) {
					End();
				}
			}
		}
	}

    /**
    * Play Sound Effects command
    */
    public void PlaySFX () {
      if (audio) {
        audio.clip = clipSuccess;
        audio.Play();
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
	 * Called when game has hasFinished.
	 */
	public virtual void Finish () {
		if (!hasFinished) {
			hasFinished = true;
	    if (audio) {
	      audio.clip = clipFail;
	      audio.Play();
	    }
			PlayerPrefs.SetInt("score", score);
			// Check end
			switch (GameManager.minigame) {
				case GameManager.Minigame.toys:
					paused = true;
					On_TimerChange(0f);
					PlayerPrefs.SetInt(minigame.ToString(), 1);
					StartCoroutine(LateEnd("room", 1f));
					break;
				case GameManager.Minigame.guitar:
					paused = true;
					On_TimerChange(0f);
					Animator aux = GameObject.FindWithTag(GameManager.tagCharacter).GetComponentInChildren<Animator>();
					aux.SetBool("playGuitar", false);
					PlayerPrefs.SetInt(minigame.ToString(), 1);
					StartCoroutine(LateEnd("room", 2f));
					break;
				case GameManager.Minigame.fire:
					paused = true;
					On_TimerChange(0f);
					PlayerPrefs.SetInt(minigame.ToString(), 1);
					StartCoroutine(LateEnd("room", 1f));
					break;
				case GameManager.Minigame.pencil:
					paused = true;
					On_TimerChange(0f);
	  			GUI.ChangeTo(GUIManager.State.loading);
					PlayerPrefs.SetInt(minigame.ToString(), 1);
					StartCoroutine(LateEnd("room", 1f));
					break;
				case GameManager.Minigame.hacky:
					paused = true;
					On_TimerChange(0f);
					aux = GameObject.FindWithTag(GameManager.tagCharacter).GetComponentInChildren<Animator>();
					aux.SetBool("playHacky", false);
					aux = GameObject.FindWithTag("ball").GetComponentInChildren<Animator>();
					aux.SetBool("playHacky", false);
					PlayerPrefs.SetInt(minigame.ToString(), 1);
					StartCoroutine(LateEnd("room", 2f));
					break;
			}
		}
	}

	/**
	 * To be called when game is over..
	 */
	public virtual void End () {
		hasFinished = true;
	}

	/**
	 * To be called by extended classes.
	 *
	 * @param float ratio Time change ratio where 1f is full and 0f is not.
	 */
	public void TimerChange (float ratio) {
		On_TimerChange(ratio);
	}

  /**
   * Transitions to next level after animations have occured.
   *
   * @param string levelName Level name.
   */
  public virtual IEnumerator LateEnd (string levelName, float seconds) {
  	if (seconds > 0f)
  		yield return new WaitForSeconds(seconds);
  	GUI.ChangeTo(GUIManager.State.loading);
  	yield return new WaitForSeconds(1f);
  	paused = false;
  	Application.LoadLevel(levelName);
  }
}
