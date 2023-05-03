using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinnerText : MonoBehaviour
{
    public void SetupWinner(Player winner)
    {
        GetComponent<TextMeshProUGUI>().SetText("Winner is : " + winner.Name + " !");
    }
}
