using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
	public static QuestManager init;

	private const string scriptNum = "scriptNum";

	private List<Dictionary<string, object>> questData;
	private List<string> sentences = new List<string> { };
	private DialogueManager dialogueManager;
	private List<ProductionManager> productionManagers;


	#region Singleton
	private void Awake() {
		if (init == null) {
			DontDestroyOnLoad(this.gameObject);
			init = this;
		} else {
			Destroy(this.gameObject);
		}
	}
	#endregion Singleton

	private void Start() {
		questData = CSVReader.Read("quest");
		dialogueManager = FindObjectOfType<DialogueManager>();

	}

	/// <summary>
	/// Shows quest information such as dialogue and progress with production object
	/// </summary>
	/// <param name="questNum"></param>
	/// <param name="currProductionObject">It determines the production of the object</param>
	public void InsertQuest(string questNum, ObjectControl currProductionObject) {
		for (var i = 0; i < questData.Count; ++i) {
			if (questData[i][scriptNum].ToString().Equals(questNum)) {

				//To call SendSentences when different the questNum.
				while (i < questData.Count) {
					if (!questData[i][scriptNum].ToString().Equals(questNum) && !questData[i][scriptNum].ToString().Equals("")) {
						SendSentences(currProductionObject);
						return;
					}
					sentences.Add((string)questData[i]["script"]);
					++i;
				}
				SendSentences(currProductionObject);
				return;
			}
		}
	}

	private void SendSentences(ObjectControl currProductionObject) {
		dialogueManager.ShowDialogue(sentences, currProductionObject);
		sentences.Clear();
	}
}
