using UnityEngine;
using System.Collections;

/*
 * Room game manager.
 *
 * @author Alejandro Mostajo <amostajo@gmail.com>
 */
public class GameManagerRoom : GameManager {

	/**
	 * Event when game begins.
	 */
  public delegate void OnGameBegin ();
  public static event OnGameBegin On_GameBegin;

	/**
	 * Event when game ends.
	 */
  public delegate void OnGameEnd ();
  public static event OnGameEnd On_GameEnd;

	/**
	 * Character.
	 */
	[HideInInspector]
	public Character character;

	/**
	 * Position where character will spawn the first time.
	 */
	public Vector3 startPosition;

	/**
	 * Rotation where character will spawn the first time.
	 */
	public Vector3 startRotation;

	/**
	 * Saved position where character should spawn.
	 */
	public Vector3 introPosition;

	/**
	 * Saved rotation where character should spawn.
	 */
	public Vector3 introRotation;

	/**
	 * Logo duration befor game stars.
	 */
	public float logoTime;

	/**
	 * Audio sound to be played when outage occurs.
	 */
	public AudioClip clipOutage;

	/**
	 * Audio sound to be played when lighter is up.
	 */
	public AudioClip clipLighter;

	/**
	 * Saved position where character should spawn.
	 */
	private Vector3 savedPosition;

	/**
	 * Saved rotation where character should spawn.
	 */
	private Vector3 savedRotation;

	/**
	 * Timer.
	 */
	private float logoTimer;
	/**
	 * Unity Awake
	 */
	public override void Awake () {
		base.Awake();
		this.character = FindObjectOfType<Character>();
		savedPosition = new Vector3(
				PlayerPrefs.GetFloat("savedPositionX"),
				PlayerPrefs.GetFloat("savedPositionY"),
				PlayerPrefs.GetFloat("savedPositionZ")
		);
		savedRotation = new Vector3(
				PlayerPrefs.GetFloat("savedRotationX"),
				PlayerPrefs.GetFloat("savedRotationY"),
				PlayerPrefs.GetFloat("savedRotationZ")
		);
		if (!GameManager.started) {
			savedPosition = introPosition;
			savedRotation = introRotation;
		}
	}

	/** 
	 * Unity start.
	 */
	public override void Start () {
		base.Start();
		GUI.ChangeTo(GUIManager.State.loading);
		if (character) {
			RepositionCharacter();
			if (!GameManager.started) {
				character.Sit();
			}
		}
		StartCoroutine(LateStart());
	}

	/**
	 * Fixed update.
	 */
	public override void FixedUpdate () {
		base.FixedUpdate();
		if (!GameManager.paused) {
			// Character movement
			if (started && character) {
				character.SetMovement(inputs.movement);
			}
		}
		if (GUI.state == GUIManager.State.logo) {
			logoTimer += Time.deltaTime;
			if (logoTimer >= logoTime) {
				StartGame();
			}
		}
	}

  /**
   * On button press.
   *
   * @param InputManager.GUIAction action GUI action to make.
   * @param int                    value  Value related to button.
   */
  public void OnButtonPress (InputManager.GUIAction action, int value) {
  	switch (GUI.state) {
  		case GUIManager.State.gameplay:
		    switch (action) {
		    	case InputManager.GUIAction.next:
		    		switch (GameManager.minigame) {
		    			case GameManager.Minigame.toys:
		    				StartCoroutine(LateEnd("minigameToys", 0f));
		    				break;
		    			case GameManager.Minigame.guitar:
		    				StartCoroutine(LateEnd("minigameGuitar", 0f));
		    				break;
		    			case GameManager.Minigame.fire:
		    				StartCoroutine(LateEnd("minigameFire", 0f));
		    				break;
		    			case GameManager.Minigame.pencil:
		    				StartCoroutine(LateEnd("minigamePencil", 0f));
		    				break;
		    			case GameManager.Minigame.hacky:
		    				StartCoroutine(LateEnd("minigameHacky", 0f));
		    				break;
		    		}
		    		break;
		    }
		    break;
  		case GUIManager.State.welcome:
		    switch (action) {
		    	case InputManager.GUIAction.next:
		    		BeginGame();
		    		break;
		    }
		    break;
	  }
  }

  /**
   * On Enable, turn on events.
   */
  public void OnEnable () {
    ButtonListener.On_ButtonPress += OnButtonPress;
  }

  /**
   * On Disable, turn off events.
   */
  public void OnDisable (){
    ButtonListener.On_ButtonPress -= OnButtonPress;
  }

  /**
   * Starts game
   */
  public void StartGame () {
  	if (audio) {
  		audio.clip = clipLighter;
  		audio.Play();
  	}
  	GUI.ChangeTo(GUIManager.State.gameplay);
		if (!GameManager.started) {
			GameManager.started = true;
			character.ShowCandle();
			savedPosition = startPosition;
			savedRotation = startRotation;
			PlayerPrefs.SetInt(GameManager.Minigame.toys.ToString(), 0);
			PlayerPrefs.SetInt(GameManager.Minigame.fire.ToString(), 0);
			PlayerPrefs.SetInt(GameManager.Minigame.guitar.ToString(), 0);
			PlayerPrefs.SetInt(GameManager.Minigame.pencil.ToString(), 0);
			PlayerPrefs.SetInt(GameManager.Minigame.hacky.ToString(), 0);
			PlayerPrefs.SetInt("score", 0);
			PlayerPrefs.SetFloat("gameTime", 0f);
			this.score = 0;
			this.timer = 0f;
			TimerChange(1f);
		}
		RepositionCharacter();
  }

  /**
   * Transitions to next level after animations have occured.
   *
   * @param string levelName Level name.
   */
  public override IEnumerator LateEnd (string levelName, float seconds) {
		if (hasGlobalTimer) {
			PlayerPrefs.SetFloat("gameTime", timer);
		}
		PlayerPrefs.SetFloat("savedPositionX", character.transform.localPosition.x);
		PlayerPrefs.SetFloat("savedPositionY", character.transform.localPosition.y);
		PlayerPrefs.SetFloat("savedPositionZ", character.transform.localPosition.z);
		PlayerPrefs.SetFloat("savedRotationX", character.transform.rotation.x);
		PlayerPrefs.SetFloat("savedRotationY", character.transform.rotation.y);
		PlayerPrefs.SetFloat("savedRotationZ", character.transform.rotation.z);
  	return base.LateEnd(levelName, seconds);
  }

  /**
   * Transitions to next level after animations have occured.
   *
   * @param string levelName Level name.
   */
  public IEnumerator LateStart () {
		yield return new WaitForSeconds(1f);
		if (GUI) {
			if (GameManager.started) {
				GUI.ChangeTo(GUIManager.State.gameplay);
			} else {
				GUI.ChangeTo(GUIManager.State.welcome);
			}
		}
  }

  /**
   * Begin game
   */
  private void BeginGame () {
  	On_GameBegin();
  	logoTimer = 0f;
		character.Unsit();
  	GUI.ChangeTo(GUIManager.State.logo);
  	if (audio) {
  		audio.clip = clipOutage;
  		audio.Play();
  	}
  }

  /**
   * Repositions character to location and rotation
   */
 	private void RepositionCharacter () {
		character.transform.localPosition = savedPosition;
		character.transform.Rotate(savedRotation);
 	}

}
