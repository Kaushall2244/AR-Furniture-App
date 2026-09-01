using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private Button btn;

    public GameObject furniture;

    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(SelectObject);
        }
    }

    void SelectObject()
    {
        if (DataHandler.Instance != null)
        {
            DataHandler.Instance.furniture = furniture;
        }
    }
}