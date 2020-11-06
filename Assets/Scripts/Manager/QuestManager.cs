using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour {
	public static QuestManager init;

	public string questnumber = "101";

	private const string questFileName = "quest";
	private const string no = "no";
	private const string title = "title";
	private const string contents = "contents";
	private const string npcNumber = "character";
	private const string npcName = "name";
	private const string scriptNum = "scriptNum";
	private const string release = "release";

	private List<string> sentences = new List<string> { };
	private ProductionManager[] objectListWithProductionManager;
	private List<Dictionary<string, object>> questDatas;
	private DialogueManager dialogueManager;

	private Text questTitle;
	private Text questContents;

	#region Singleton
	private void Awake() {
		if (init == null) {
			DontDestroyOnLoad(this.gameObject);
			init = this;
		} else {
			Destroy(this.gameObject);
		}

		questDatas = CSVReader.Read(questFileName);
	}
	#endregion Singleton

	private void Start() {
		dialogueManager = FindObjectOfType<DialogueManager>();
		objectListWithProductionManager = FindObjectsOfType(typeof(ProductionManager)) as ProductionManager[];

		try {
			questTitle = transform.GetChild(0).Find("Title").GetComponent<Text>();
			questContents = transform.GetChild(0).Find("Contents").GetComponent<Text>();
		} catch (System.NullReferenceException e) {
			Debug.Log(e.Message + " 퀘스트 제목과 내용 오브젝트를 찾을 수 없습니다.");
		}

		setQuestTitle(questnumber);
	}

	/// <summary>
	/// Shows quest information such as dialogue and progress with production object
	/// </summary>
	/// <param name="questNum"></param>
	/// <param name="currProductionObject">It determines the production of the object</param>
	public string InsertQuest(string questNum) {
		for (var i = 0; i < questDatas.Count; ++i) {
			if (questDatas[i][scriptNum].ToString().Equals(questNum)) {
				string npcName = questDatas[i][QuestManager.npcName].ToString();
				string npcNumber = questDatas[i][QuestManager.npcNumber].ToString();
		
				//To call SendSentences when different the questNum.
				while (i < questDatas.Count) {
					if (!questDatas[i][scriptNum].ToString().Equals(questNum) && !questDatas[i][scriptNum].ToString().Equals("")) {
						SendSentences(npcName);
						return npcNumber;
					}
					sentences.Add((string)questDatas[i]["script"]);
					++i;
				}
				SendSentences(npcName);
				return npcNumber;
			}
		}
		return "";
	}

	private void SendSentences(string name) {
		dialogueManager.ShowDialogue(sentences, name);
		sentences.Clear();
	}

	public void nextQuest(string nextQuestNumber) {
		foreach (Dictionary<string, object> questData in questDatas) {
			if (questData[no].ToString().Equals(nextQuestNumber)) {
				setQuestTitle(questData[title], questData[contents]);
				releaseQuestObject(questData[release]);
				break;
			}
		}
	}

	private void setQuestTitle(object title, object contents) {
		questTitle.text = title.ToString();
		questContents.text = contents.ToString();
	}

	private void setQuestTitle(string nextQuestNumber) {
		foreach (Dictionary<string, object> questData in questDatas) {
			if (questData[no].ToString().Equals(nextQuestNumber)) {
				setQuestTitle(questData[title], questData[contents]);
				break;
			}
		}
	}

	private void releaseQuestObject(object relaseNumber) {
		string[] relaseNumbers = relaseNumber.ToString().Replace(" ", "").Split(',');

		foreach (string relase in relaseNumbers) {
			foreach (ProductionManager production in objectListWithProductionManager) {
				if (production.questNumber.Equals(relase))
					production.gameObject.SetActive(false);
			}
		}
    }

	public void releaseQuestObject(string questNumber) {
		nextQuest(questNumber);
	}

	public List<string> getQuestNumbers() {
		List<string> questNumbers = new List<string>();

		for(int i = 0; i < questDatas.Count; ++i) {
			if (!questDatas[i][no].ToString().Equals(""))
				questNumbers.Add(questDatas[i][no].ToString());
		}

		return questNumbers;
	}
}