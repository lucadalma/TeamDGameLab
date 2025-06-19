using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorialPage : MonoBehaviour
{
    [SerializeField] List<GameObject> tutorialPanels;

    [SerializeField] Transform displaySlot;
    [SerializeField] List<Transform> rightHiddenSlots;
    [SerializeField] List<Transform> leftHiddenSlots;





    public void SetMainPanel(GameObject targetPanel)
    {
        foreach (var tutorialPanel in tutorialPanels)
        {
            tutorialPanel.transform.GetChild(0).gameObject.SetActive(true);
        }

        targetPanel.transform.GetChild(0).gameObject.SetActive(false);

        targetPanel.transform.parent = displaySlot;

        SortOtherPanels(targetPanel);
    }

    void SortOtherPanels(GameObject currentPanel)
    {
        int CurrentPanelValue = tutorialPanels.IndexOf(currentPanel);

        if (CurrentPanelValue < 5)
        {
            for (int i = 0; i < 5 - CurrentPanelValue; i++)
            {
                tutorialPanels[i + CurrentPanelValue + 1].transform.parent = rightHiddenSlots[i];
            }
        }

        if (CurrentPanelValue > 0)
        {
            for (int i = 0; i < 0 + CurrentPanelValue; i++)
            {
                tutorialPanels[i].transform.parent = leftHiddenSlots[i];
            }
        }

    }



    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
