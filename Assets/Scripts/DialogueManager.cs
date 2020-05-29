using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
	private int PLAYER_MOVE_SPEED;

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

	private QuestProperties currQuestProperties;
	private ObjectControl currProductionObject;

	private List<string> listSentences;
	public Sprite listDialogueWindows;

	private int count; //대화 진행 카운드

	public Animator animDialogueWindow;

	public bool talking = false;
	public bool productionTalking = false;

	private void Start() {
		count = 0;
		text.text = "";
		listSentences = new List<string>();

		PLAYER_MOVE_SPEED = PlayerControl.init.moveSpeed;
	}

	public void ShowDialogue(List<string> sentences, QuestProperties currQuest) {
		if (currQuest) currQuestProperties = currQuest;

		onSentencesEnter();

		talking = true;

		for(int i = 0; i < sentences.Count; ++i) {
			listSentences.Add(sentences[i]);
		}
		//대화창 이미지
		animDialogueWindow.SetBool("appear", true);
		StartCoroutine("StartDialogueCoroutine");
	}

	public void ShowDialogue(List<string> sentences, ObjectControl currProductionObject) {
		if (currProductionObject) this.currProductionObject = currProductionObject;

		productionTalking = true;

		for (int i = 0; i < sentences.Count; ++i) {
			listSentences.Add(sentences[i]);
		}
		//대화창 이미지
		animDialogueWindow.SetBool("appear", true);
		StartCoroutine("StartDialogueCoroutine");
	}

	public void ExitDialogue() {
		onSentencesExit();

		text.text = "";
		count = 0;
		listSentences.Clear();

		animDialogueWindow.SetBool("appear", false);
		talking = false;
	}

	public void ExitProductiondialogue() {
		this.currProductionObject.isDialogue = false;

		text.text = "";
		count = 0;
		listSentences.Clear();

		animDialogueWindow.SetBool("appear", false);
		productionTalking = false;
    }

	IEnumerator StartDialogueCoroutine() {

		if (count > 0) {

		} else {
			dialogueRenderer.GetComponent<SpriteRenderer>().sprite = listDialogueWindows;
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
		else if (productionTalking) {
			if (Input.GetMouseButtonDown(0)) {
				count++;
				text.text = "";

				if (count == listSentences.Count) {
					StopAllCoroutines();
					ExitProductiondialogue();
				} else {
					StopAllCoroutines();
					StartCoroutine("StartDialogueCoroutine");
				}
			}
		}
	}

	private void onSentencesEnter() {
		PlayerControl.init.moveSpeed = 0;
		currQuestProperties.onSentencesEnter();
	}

	private void onSentencesExit() {
		PlayerControl.init.moveSpeed = PLAYER_MOVE_SPEED;
		currQuestProperties.onSentenceExit();
	}
}