﻿using UnityEngine;
using System.Collections;

/*
 * Triggers minigame selection..
 *
 * @author Alejandro Mostajo <amostajo@gmail.com>
 */
public class MinigameTrigger : MonoBehaviour {

	/**
	 * On score update event.
	 */
  public delegate void OnMinigameTriggered (GameManager.Minigame minigame, string message);
  public static event OnMinigameTriggered On_MinigameTriggered;

	/**
	 * Selected minigame.
	 */
	public GameManager.Minigame minigame;

	/**
	 * Message to display.
	 */
	public string message;

	/**
	 * Game reference.
	 */
	private GameManager game;

	/**
	 * Inits.
	 */
	public void Awake () {
		game = GameManager.Get();
	}

	/**
	 * Unity on trigger enter.
	 */
	public void OnTriggerEnter (Collider collider) {
		if (collider.tag == GameManager.tagCharacter) {
			On_MinigameTriggered(minigame, message);
			game.GUI.ChangeTo(GUIManager.State.message);
		}
	}

	/**
	 * Unity on trigger enter.
	 */
	public void OnTriggerExit (Collider collider) {
		if (collider.tag == GameManager.tagCharacter) {
			game.GUI.ChangeTo(GUIManager.State.gameplay);
		}
	}
}
