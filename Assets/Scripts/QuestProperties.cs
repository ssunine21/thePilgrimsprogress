using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PROPERTIES {
	BLOCK = 0,
	NPC = 1
}
public class QuestProperties : MonoBehaviour
{

	public PROPERTIES properties;
	public string questNumber = "";

	private QuestManager questManager;
	private GameObject playerObject;
	private Vector3 playerDirection;

	private void Start() {
		questManager = FindObjectOfType<QuestManager>();
		playerDirection = Vector3.zero;
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Player")) {
			Debug.Log("onCollider");
			questManager.InsertQuest(questNumber, this);

			playerObject = collision.gameObject;
			playerDirection = (this.transform.position - collision.transform.position).normalized;
		}
	}

	public void onSentencesEnter() {

	}
	public void onSentenceExit() {
		switch (properties) {
			case PROPERTIES.BLOCK:

				playerObject.transform.Translate(playerDirection);
				//StartCoroutine("Oppositydirection");
				break;
			case PROPERTIES.NPC:
				Debug.Log("position is " + properties.ToString());
				break;
			default:
				break;
		}
	}

	IEnumerator Oppositydirection() {
		int i = 0;
		while(i < 100) {
			i++;

			playerObject.transform.Translate(playerDirection * Time.deltaTime);
			yield return null;
		}
	}

}
