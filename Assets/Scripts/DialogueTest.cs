using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTest : MonoBehaviour
{
	[SerializeField]
	public Dialogue dialogue;

	private DialogueManager dialogueManager;

	private void Start() {
		dialogueManager = FindObjectOfType<DialogueManager>();
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.gameObject.name == "Player") {
			dialogueManager.ShowDialogue(dialogue);
		}
	}
}
