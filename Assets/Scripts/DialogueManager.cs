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

	private readonly int appearAnimId = Animator.StringToHash("appear");
	private readonly int dis_appearAnimId = Animator.StringToHash("dis_appear");

	public Text dialogText;
	public Text _nameTag;
	public string nameTag {
		set {
			if (value.Equals("나레이션"))
				_nameTag.rectTransform.parent.gameObject.SetActive(false);
			else {
				_nameTag.rectTransform.parent.gameObject.SetActive(true);
				_nameTag.text = value;
			}
		}
	}

	private List<string> listSentences;

	private int count; //대화 진행 카운드

	public Animator animDialogueWindow;

	public bool talking = false;
	public bool productionTalking = false;

	private void Start() {
		count = 0;
		dialogText.text = "";
		listSentences = new List<string>();
	}

	private void setDialog(List<string> sentences) {

		for (int i = 0; i < sentences.Count; ++i) {
			listSentences.Add(sentences[i]);
		}

		//대화창 이미지
		animDialogueWindow.SetBool(appearAnimId, true);
		StartCoroutine("StartDialogueCoroutine");
	}

	public void ShowDialogue(List<string> sentences, string name) {
		productionTalking = true;
		nameTag = name;

		setDialog(sentences);
	}

	public void ExitDialogue() {

		dialogText.text = "";
		count = 0;
		listSentences.Clear();

		animDialogueWindow.SetBool(appearAnimId, false);
		talking = false;
	}

	public void ExitProductiondialogue() {

		dialogText.text = "";
		count = 0;
		listSentences.Clear();

		animDialogueWindow.SetBool(appearAnimId, false);
		productionTalking = false;
    }

	IEnumerator StartDialogueCoroutine() {

		for(int i = 0; i < listSentences[count].Length; ++i) {
			if ("\n".Equals(listSentences[count][i]))
				dialogText.text += System.Environment.NewLine;
			else dialogText.text += listSentences[count][i];

			yield return new WaitForSeconds(0.01f);
		}
	}

	private void Update() {

		if (talking) {
			if (Input.GetMouseButtonDown(0)) {
				count++;
				dialogText.text = "";

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
				dialogText.text = "";

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