using UnityEngine;
using System.Collections;

/*
 * Listens to NGUI button press and throws event.
 *
 * @author Alejandro Mostajo
 * @copyright 2014 Amsgames, LLC
 *
 * THIS SCRIPT WAS PROVIDED BY AMSGAMES, LLC.
 * YOU CAN USE THIS SCRIPT FREELY IF YOU GIVE ATTRIBUTION AND CREDIT TO ITS CREATOR (AMSGAMES).
 */
public class ButtonListener : MonoBehaviour {

	/**
	 * Delegate on button press.
	 *
	 * @param InputManager.GUIAction action GUI action to make.
	 * @param bool 									 isDown Indicates if value is down.
	 * @param int 									 value  Value related to button.
	 */
  public delegate void OnButtonPress (InputManager.GUIAction action, int value);
  /**
   * Delegate function.
   */
  public static event OnButtonPress On_ButtonPress;
  /**
   * GUI action related to button.
   */
  public InputManager.GUIAction action;
  /**
   * Value related to button.
   */
  public int value = 0;

  /**
   * Calls event onbutton press.
   *
   * @param boolean isDown
   */
  public void OnPress (bool isDown) {
  	if (!isDown) {
  		return;
  	}
  	On_ButtonPress(action, value);
  }
}
