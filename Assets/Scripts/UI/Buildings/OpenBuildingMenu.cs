using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBuildingMenu : MonoBehaviour
{
    // Start is called before the first frame update

    UIManager manager;

    void Start()
    {
        manager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }


    public void OpenBuildingMenuButton()
    {
        manager.PressdOpenBuildingUI(this.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject);
    }
}
