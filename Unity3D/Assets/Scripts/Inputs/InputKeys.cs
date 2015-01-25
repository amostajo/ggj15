using UnityEngine;
using System.Collections;


/**
 * GUI input flags.
 */
[System.Serializable]
public struct InputKeys {

	/**
	 * Key that needs to be pressed.
	 */
	public int correct;

	/**
	 * Key pressed by gamer.
	 */
	public int lastUsed;

	/**
	 * Default construct.
	 */
	public InputKeys (int def) {
		this.correct = def;
		this.lastUsed = def;
	}
}
