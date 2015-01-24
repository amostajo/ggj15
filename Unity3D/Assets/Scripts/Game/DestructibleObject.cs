									using UnityEngine;
									using System.Collections;

						public class DestructibleObject : MonoBehaviour {
									
							public float health = 100F;
					
							float lerpTime = 1f;

							float currentLerpTime;
	
							float moveDistance = 10f;
	
							Vector3 startPos;

							Vector3 endPos;

							public float minimum = 10.0F;
							
							public float maximum = 20.0F;


						public void Start(){
							startPos = transform.position;
							endPos = transform.position * moveDistance;
						}

							public void OnCollisionEnter (Collision col)
							{
								Debug.Log("Choque");
								health -= 2F;

								//lerp!
								float perc = currentLerpTime / lerpTime;
								transform.position = Vector3.Lerp(startPos, endPos, perc);
								
							}

						public void Update() {	
							//increment timer once per frame
							currentLerpTime += Time.deltaTime;
							if (currentLerpTime > lerpTime) {
								currentLerpTime = lerpTime;
							}
	
								
						
						}
}

