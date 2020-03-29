using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager init;

	#region Singleton
	private void Awake() {
        if(init == null) {
            DontDestroyOnLoad(this.gameObject);
            init = this;
		} else {
            Destroy(this.gameObject);
		}
	}
	#endregion Singleton

	public Text text;
	public SpriteRenderer dialogueRenderer;

	private List<string> listSentences;
	private List<Sprite> listDialogueWindows;

	private int count; //대화 진행 카운드

	public Animator animDialogueWindow;

	public bool talking = false;

	private void Start() {
		count = 0;
		text.text = "";
		listSentences = new List<string>();
		listDialogueWindows = new List<Sprite>();
	}

	public void ShowDialogue(Dialogue dialogue) {

		talking = true;

		for(int i = 0; i < dialogue.sentences.Length; ++i) {
			listSentences.Add(dialogue.sentences[i]);
			listDialogueWindows.Add(dialogue.dialogueWindows[i]);
		}
		animDialogueWindow.SetBool("appear", true);
		StartCoroutine("StartDialogueCoroutine");
	}

	public void ExitDialogue() {
		text.text = "";
		count = 0;
		listDialogueWindows.Clear();
		listSentences.Clear();

		animDialogueWindow.SetBool("appear", false);
		talking = false;
	}

	IEnumerator StartDialogueCoroutine() {

		if (count > 0) {

			if (listDialogueWindows[count] != listDialogueWindows[count - 1]) {
				animDialogueWindow.SetBool("appear", false);
				yield return new WaitForSeconds(0.2f);
				dialogueRenderer.GetComponent<SpriteRenderer>().sprite = listDialogueWindows[count];
				animDialogueWindow.SetBool("appear", true);
			}
		} else {
			dialogueRenderer.GetComponent<SpriteRenderer>().sprite = listDialogueWindows[count];
		}

		for(int i = 0; i < listSentences[count].Length; ++i) {
			text.text += listSentences[count][i];
			yield return new WaitForSeconds(0.01f);
		}
	}

	private void Update() {

		if (talking) {
			if (Input.GetMouseButtonDown(0)) {
				count++;
				text.text = "";

				if (count == listSentences.Count) {
					StopAllCoroutines();
					ExitDialogue();
				} else {
					StopAllCoroutines();
					StartCoroutine("StartDialogueCoroutine");
				}
			}
		}
	}
}
