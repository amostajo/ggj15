using UnityEngine;
using System.Collections;

/*
 * Listes to game state to change lighting in scene.
 *
 * @author Alejandro Mostajo <amostajo@gmail.com>
 */
public class GameStateListener : MonoBehaviour {

	/**
	 * Tween color;
	 */
	private TweenColor tween;

	/**
	 * Unity Awake
	 */
	public void Awake () {
		tween = GetComponent<TweenColor>();
	}

  /**
   * On Enable, turn on events.
   */
  public void OnEnable () {
    GameManagerRoom.On_GameBegin += OnGameBegin;
    GameManagerRoom.On_GameEnd += OnGameEnd;
  }

  /**
   * On Disable, turn off events.
   */
  public void OnDisable (){
    GameManagerRoom.On_GameBegin -= OnGameBegin;
    GameManagerRoom.On_GameEnd -= OnGameEnd;
  }

  /**
   * Event when game begins
   */
  private void OnGameBegin () {
  	if (tween) {
  		tween.enabled = false;
  	}
  	if (light) {
  		light.enabled = false;
  	}
  	if (audio) {
  		audio.Stop();
  	}
  }

  /**
   * Event when game ends
   */
  private void OnGameEnd () {
  	if (tween) {
  		tween.enabled = true;
  	}
  	if (light) {
  		light.enabled = true;
  	}
  }

}
