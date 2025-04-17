using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public enum AnimState
    {
        Open,
        CloseAll,
        CloseOptions,
        CloseSpesific,

    }

    AnimState[] animStateValues = Enum.GetValues(typeof(AnimState)) as AnimState[];

    enum AnimTarget
    {
        Menu,
        Options,
        spescific
    }



    public GameObject MenuTab;
    public GameObject OptionsTab;
    public List<GameObject> SpescificTabs;
    GameObject currentSpescificTab;

    public List<Transform> MenuPos;
    public List<Transform> OptionsPos;
    public List<Transform> SpescificTabsPos;

    public float animSpeed;
    bool OptionsOpened;

    // Start is called before the first frame update
    void Start()
    {
        MenuTab.transform.position = MenuPos[0].position;
        OptionsTab.transform.position = new Vector3(MenuTab.transform.position.x, OptionsPos[0].position.y, MenuTab.transform.position.z);

        foreach (GameObject tab in SpescificTabs)
        {
            tab.transform.position = SpescificTabsPos[0].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        OptionsTab.transform.position = new Vector2(MenuTab.transform.position.x, OptionsTab.transform.position.y);
    }


    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenuScene");
    }


    public void CloseMenuButton()
    {
        CloseMenu(AnimState.CloseAll);
    }

    public void CloseOptionsButton()
    {
        CloseOptions(AnimState.CloseOptions);
    }

    public void CloseSpesificButton()
    {
        CloseSpescific(AnimState.CloseSpesific);
    }
    public void OpenMenu()
    {
        StartCoroutine(MoveTab(MenuPos[1].position, MenuTab, AnimState.Open, AnimTarget.Menu));
    }

    void CloseMenu(AnimState state)
    {
        if (OptionsOpened)
        {
            CloseOptions(state);
        }
        else
        {
            StartCoroutine(MoveTab(MenuPos[0].position, MenuTab, state, AnimTarget.Menu));
        }

    }

    public void OpenOptions()
    {
        OptionsOpened = true;
        StartCoroutine(MoveTab(OptionsPos[1].position, OptionsTab, AnimState.Open, AnimTarget.Options));
    }

    void CloseOptions(AnimState state)
    {
        if (currentSpescificTab != null)
        {
            CloseSpescific(state);
        }
        else
        {
            OptionsOpened = false;
            StartCoroutine(MoveTab(OptionsPos[0].position, OptionsTab, state, AnimTarget.Options));
        }
    }

    public void ControllSpescific(int index)
    {
        if (currentSpescificTab == null)
        {
            OpenSpescific(index);
        }
        else
        {
            SwitchSpescific(index);
        }
    }




    public void SwitchSpescific(int index)
    {
        Debug.Log("Opening spescific" + currentSpescificTab.name + "tab");
        StartCoroutine(SiwchTab(SpescificTabsPos[0].position, currentSpescificTab, AnimState.Open, AnimTarget.spescific, index));
    }

    public void OpenSpescific(int index)
    {
        currentSpescificTab = SpescificTabs[index];

        StartCoroutine(MoveTab(SpescificTabsPos[1].position, currentSpescificTab, AnimState.Open, AnimTarget.spescific));
    }

    void CloseSpescific(AnimState state)
    {
        Debug.Log("Cloasing spescific" + currentSpescificTab.name + "tab");
        StartCoroutine(MoveTab(SpescificTabsPos[0].position, currentSpescificTab, state, AnimTarget.spescific));
    }

    IEnumerator MoveTab(Vector2 endPos, GameObject obj, AnimState stete, AnimTarget target)
    {
        Vector2 startPos;
        startPos = obj.transform.position;

        for (float i = 0; i < 1; i += animSpeed * Time.deltaTime)
        {
            if (obj != null)
            {
                obj.transform.position = Vector2.Lerp(startPos, endPos, i);
            }
            yield return new WaitForEndOfFrame();
        }

        obj.transform.position = endPos;

        switch (stete)
        {
            case AnimState.Open:
                break;
            case AnimState.CloseAll:
                switch (target)
                {
                    case AnimTarget.Options:
                        CloseMenu(AnimState.CloseAll);
                        break;
                    case AnimTarget.spescific:
                        currentSpescificTab = null;
                        CloseOptions(AnimState.CloseAll);
                        break;
                }
                break;
            case AnimState.CloseOptions:
                switch (target)
                {
                    case AnimTarget.spescific:
                        currentSpescificTab = null;
                        CloseOptions(AnimState.CloseOptions);
                        break;
                }
                break;
            case AnimState.CloseSpesific:
                break;
        }


    }

    IEnumerator SiwchTab(Vector2 endPos, GameObject obj, AnimState stete, AnimTarget target, int SpescificIndex)
    {
        Vector2 startPos;
        startPos = obj.transform.position;

        for (float i = 0; i < 1; i += animSpeed * Time.deltaTime)
        {
            if (obj != null)
            {
                obj.transform.position = Vector2.Lerp(startPos, endPos, i);
            }
            yield return new WaitForEndOfFrame();
        }

        OpenSpescific(SpescificIndex);

    }


}
