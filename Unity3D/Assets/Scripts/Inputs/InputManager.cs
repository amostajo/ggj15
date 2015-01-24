using UnityEngine;
using System.Collections;

/*
 * Input Manager. Handles all inputs in the system.
 *
 * @author Alejandro Mostajo <amostajo@gmail.com>
 */
public class InputManager : MonoBehaviour {

  /**
   * On score update event.
   */
  public delegate void OnKeyUpdate (string key);
  public static event OnKeyUpdate On_KeyUpdate;

  // UP
  private static Vector3 moveUp = new Vector3(0f, 0f, 1f);
  // DOWN
  private static Vector3 moveDown = new Vector3(0f, 0f, -1f);
  // LEFT
  private static Vector3 moveLeft = new Vector3(-1f, 0f, 0f);
  // RIGHT
  private static Vector3 moveRight = new Vector3(1f, 0f, 0f);

  // Input scheme
  public enum Scheme {mobile = 0, desktop = 1, console = 2};
  // Gui actions
  public enum GUIAction {turbo = 0, fire = 1, ammo = 2, next = 3, back = 4, option = 5};

  /**
   * Game inputs.
   */
  private InputKeys keys;

  /**
   * Flag that indicates if player can move
   */
  [HideInInspector]
  public bool move;

  /**
   * Movement
   */
  [HideInInspector]
  public Vector3 movement;

  /**
   * Flag that indicates if a key was pressed.
   */
  [HideInInspector]
  public bool keyCheck;

  /**
   * Flag that indicates if pressed key is correct.
   */
  [HideInInspector]
  public bool keySuccess;

  /**
   * Game's input scheme.
   */
  public Scheme scheme;

  /**
   * Unity's Awake.
   */
  public void Awake () {
    this.SetCurrentScheme();
    this.keys = new InputKeys(0);
  }
  
  /**
   * Unity's Update.
   */
  public void Update () {
    switch (scheme) {
      case Scheme.desktop:
        this.UpdateDesktop();
        break;
        break;
      default:
        break;
    }
  }

  /**
   * Process desktop Input.
   */
  public void UpdateDesktop () {

    this.move = false;
    this.keyCheck = false;
    this.movement = Vector3.zero;
    if (Input.anyKey) {
      // UP - W
      if (Input.GetKey(KeyCode.UpArrow)) {
        this.move = true;
        this.movement += moveUp;
      }
      // DOWN - S
      if (Input.GetKey(KeyCode.DownArrow)) {
        this.move = true;
        this.movement += moveDown;
      } 
      // LEFT - A
      if (Input.GetKey(KeyCode.LeftArrow)) {
        this.move = true;
        this.movement += moveLeft;
      }
      // RIGHT - D
      if (Input.GetKey(KeyCode.RightArrow)) {
        this.move = true;
        this.movement += moveRight;
      }
      this.keyCheck = true;
      this.keySuccess = Input.GetKeyDown((KeyCode)keys.correct);
    } 

    // Back
    /**
    if (Input.GetKeyDown(KeyCode.Escape)) {
      this.GUI.back = true;
    } else if (this.GUI.back && Input.GetKeyUp(KeyCode.Escape)) {
      this.GUI.back = false;
    }
    */

  }

  /**
   * Sets ups a correct key.
   */
  public void RequestKey () {
    switch (scheme) {

      // Generate correct key for Desktop
      case Scheme.desktop:
        keys.correct = (int)Random.Range(97f, 123f);
        On_KeyUpdate(((KeyCode)keys.correct).ToString());
        break;

    }
  }

  /**
   * On button press.
   *
   * @param InputManager.GUIAction action GUI action to make.
   * @param bool                   isDown Indicates if value is down.
   * @param int                    value  Value related to button.
   */
  public void OnButtonPress (InputManager.GUIAction action, bool isDown, int value) {
    // TODO
  }

  /**
   * On Enable, turn on events.
   */
  public void OnEnable () {
    //ButtonListener.On_ButtonPress += OnButtonPress;
  }

  /**
   * On Disable, turn off events.
   */
  public void OnDisable (){
    //ButtonListener.On_ButtonPress -= OnButtonPress;
  }

  /**
   * Sets current scheme based on the current running platform.
   */
  private void SetCurrentScheme () {
    switch (Application.platform) {
      case RuntimePlatform.Android:
      case RuntimePlatform.IPhonePlayer:
        scheme = Scheme.mobile;
        break;
      case RuntimePlatform.PS3:
      case RuntimePlatform.XBOX360:
        scheme = Scheme.console;
        break;
      case RuntimePlatform.OSXEditor:
      case RuntimePlatform.WindowsEditor:
        break;
      default:
        scheme = Scheme.desktop;
        break;
    }
  }

}
