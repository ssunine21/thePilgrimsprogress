using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestProperties : MonoBehaviour
{
	private enum Quest_Properties {
		BLOCK = 0
	}

	public Quest_Properties propertiesList;
	public string questNumber = "";

	private QuestManager questManager;
	private void Start() {
		questManager = FindObjectOfType<QuestManager>();
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Player")) {
			Debug.Log("onCollider");
			questManager.InsertQuest(questNumber);
			Vector3 direction = transform.position - collision.transform.position;
			Debug.Log(direction.normalized);
		}
	}
}
