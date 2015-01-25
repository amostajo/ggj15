using UnityEngine;
using System.Collections;

/**
 * Handles and manages the game's GUI
 *
 * @author Alejandro Mostajo
 * @copyright 2014 Amsgames, LLC <amsgames.com>
 *
 * THIS SCRIPT WAS PROVIDED BY AMSGAMES, LLC.
 * YOU CAN USE THIS SCRIPT FREELY IF YOU GIVE ATTRIBUTION AND CREDIT TO ITS CREATOR (AMSGAMES).
 */
public class GUIManager : MonoBehaviour {

  /**
   * Enum with the different states available in GUI manager,
   */
  public enum State {
      welcome = 0, loading = 1, gameplay = 2, message = 3, logo = 4
  };

  /**
   * Current state
   */
  //[HideInInspector]
  public GUIManager.State state = State.welcome;

	/**
	 * Delegate on turbo cool down.
	 *
	 * @param float value Cooldown value from 1 to 0.
	 */
  public delegate void OnStateChange (GUIManager.State state);
  /**
   * Delegate event when turbo cooldown is happening.
   */
  public static event OnStateChange On_StateChange;

  /**
   * Changes GUI to specific state.
   */
  public void ChangeTo (GUIManager.State state) {
  	this.state = state;
    On_StateChange(state);
  }

}
