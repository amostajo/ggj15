using UnityEngine;
using System.Collections;

/**
 * GameObject script. Script to be attached to a hidden gameobject in the game scene. 
 * Controls and hangles game time.
 *
 * @author Alejandro Mostajo <amostajo@amsgames.com>
 * @copyright 2014 Amsgames, LLC
 */

public class Timer : MonoBehaviour {
  /**
   * Current game time. (Paused time is subtracted)
   */
  public static float time;
  /**
   * Current game time without pause subtraction.
   */
  public static float unpausedTime;
  /**
   * Time loaded from save file. Helps adjust timer time related variables in game objects.
   */
  public static float loadedTime = 0f;
  /**
   * Flag that indicates if the timer is paused.
   */
  [HideInInspector]
  public bool paused;
  /**
   * Amount of paused time.
   */
  private float pausedTime;
  /**
   * Time when the timer was paused.
   */
  private float timeWhenPaused;
  /**
   * Creation time.
   */
  [HideInInspector]
  public float creationTime;

  /**
	 * Unity start
	 */
  public void Start () {
    paused = false;
    creationTime = pausedTime = 0.0f;
  }

  /**
	 * Unity update
	 */
  private void Update () {
    unpausedTime = Time.time - creationTime;
    if (paused) {
      pausedTime += Time.time - timeWhenPaused;
      timeWhenPaused = Time.time;
    } else {
      time = Time.time - creationTime - pausedTime;
    }
  }

  /**
   * Pauses the timer.
   */
  public void Pause () {
    paused = true;
    timeWhenPaused = Time.time;
  }

  /**
   * Unpauses the timer.
   * @function
   */
  public void Resume () {
    paused = false;
  }

  /**
   * Gets the game time taking in consideration the paused time.
   * @function
   *
   * @return float Game time
   */
  public float GetGameTime () {
      return time;
  }

}