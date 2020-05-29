using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PROPERTIES {
	BLOCK = 0,
	NPC = 1
}
public class QuestProperties : MonoBehaviour
{
	private const float REFLECTION_TIME = 0.1f;

	public PROPERTIES properties;
	public string questNumber = "";

	private QuestManager questManager;
	private PlayerControl playerControl;
	private Vector2 playerDirection;

	private void Start() {
		questManager = FindObjectOfType<QuestManager>();
		playerDirection = Vector2.zero;
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Player")) {
			Debug.Log("onCollider");
			questManager.InsertQuest(questNumber, this);

			playerControl = collision.GetComponent<PlayerControl>();
			playerDirection = playerControl.getDirect();
			Debug.Log(playerDirection);
		}
	}

	public void onSentencesEnter() {


	}
	public void onSentenceExit() {
		switch (properties) {
			case PROPERTIES.BLOCK:
				StartCoroutine("Reflection");
				break;
			case PROPERTIES.NPC:
				Debug.Log("position is " + properties.ToString());
				break;
			default:
				break;
		}
	}
    IEnumerator Reflection() {
		playerControl.isSystemControl = true;
		playerControl.setDirect(playerDirection * -1);

		yield return new WaitForSeconds(REFLECTION_TIME);

		playerControl.isSystemControl = false;
	}
}
