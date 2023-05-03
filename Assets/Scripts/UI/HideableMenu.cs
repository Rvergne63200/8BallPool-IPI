using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideableMenu : MonoBehaviour
{
    public void ShowMenu()
    {
        gameObject.SetActive(true);
    }

    public void HideMenu()
    {
        gameObject.SetActive(false);
    }
}
