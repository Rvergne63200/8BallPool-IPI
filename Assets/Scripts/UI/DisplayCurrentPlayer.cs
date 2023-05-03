using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCurrentPlayer : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textUI;

    [SerializeField]
    Image playerSpriteUI; 
    
    public void updatePlayer(Player player)
    {
        textUI.SetText(player.Name);
        playerSpriteUI.sprite = player.PlayerSprite;
    }
}
