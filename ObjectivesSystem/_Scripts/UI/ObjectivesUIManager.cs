using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ObjectiveTreeController))]
public class ObjectivesUIManager : MonoBehaviour {

    public Text nameText;
    public Text descriptionText;
    public Text messageText;

	void Update () {
        SetInfo();
	}

    //sets information for focused objective tree
    public void SetInfo()
    {
        if (ObjectiveTreeController.trees[ObjectiveTreeController.focusedTree].currentObjective != null)
        {
            nameText.text = ObjectiveTreeController.trees[ObjectiveTreeController.focusedTree].currentObjective.GetName();
            descriptionText.text = ObjectiveTreeController.trees[ObjectiveTreeController.focusedTree].currentObjective.GetDescription();
        }
        else
        {
            nameText.text = "";
            descriptionText.text = "";
        }
    }

    //sets message text and runs animation
    public void Message(string objName)
    {
        messageText.text = (objName);
        messageText.GetComponent<Animator>().Play("MessageText");
    }

    public void NextObjective()
    {
        if (ObjectiveTreeController.focusedTree > 0)
        {
            ObjectiveTreeController.focusedTree--;
        }
    }
    public void PreviousObjective()
    {
        if (ObjectiveTreeController.focusedTree > ObjectiveTreeController.trees.Length - 1)
        {
            ObjectiveTreeController.focusedTree++;
        }
    }
}
