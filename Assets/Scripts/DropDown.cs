using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDown : MonoBehaviour
{
    public Dropdown dropdown;
    public Button startButton;

    private List<string> buildingNames = new List<string>();

    void Start()
    {
        setDropDown();
    }

    private void setDropDown() {
        dropdown.ClearOptions();

        buildingNames = QuestManager.init.getQuestNumbers();
        dropdown.AddOptions(buildingNames);
    }

    public void setQuest() {
        int index = dropdown.value;
        QuestManager.init.allActiveQuest();
        QuestManager.init.releaseQuestObject(buildingNames[index]);
    }
}
