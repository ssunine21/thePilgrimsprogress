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
            init = this;
		} else {
            Destroy(this.gameObject);
		}
	}
	#endregion Singleton

	public Text text;
	private ObjectControl currProductionObject;
	private List<string> listSentences;

	private int count; //대화 진행 카운드

	public Animator animDialogueWindow;

	public bool talking = false;
	public bool productionTalking = false;

	private void Start() {
		count = 0;
		text.text = "";
		listSentences = new List<string>();
	}

	private void setDialog(List<string> sentences) {

		for (int i = 0; i < sentences.Count; ++i) {
			listSentences.Add(sentences[i]);
		}

		//대화창 이미지
		animDialogueWindow.SetBool("appear", true);
		StartCoroutine("StartDialogueCoroutine");
	}

	public void ShowDialogue(List<string> sentences, ObjectControl currProductionObject) {
		if (currProductionObject)
			this.currProductionObject = currProductionObject;

		productionTalking = true;
		setDialog(sentences);
	}

	public void ExitDialogue() {

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

		for(int i = 0; i < listSentences[count].Length; ++i) {
			if ("\n".Equals(listSentences[count][i]))
				text.text += System.Environment.NewLine;
			else text.text += listSentences[count][i];

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
}