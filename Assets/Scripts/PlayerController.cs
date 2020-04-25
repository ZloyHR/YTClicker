using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static Avatar playerAvatar;
    public static int playerAvatarId = 0;
    public AvatarScripts defaultAvatar;
    public GameObject playerIcon;
    private void Awake()
    {
        playerAvatar = new Avatar(defaultAvatar);
        playerAvatar = SaveSystem.Get(playerAvatar, "playerAvatar");
        playerAvatarId = SaveSystem.Get(playerAvatarId, "playerAvatarId");
    }

    public static void Save()
    {
        SaveSystem.Save(playerAvatar,"playerAvatar");
        SaveSystem.Save(playerAvatarId,"playerAvatarId");
    }

    private void Update()
    {
        playerIcon.GetComponent<Image>().sprite = playerAvatar.image;
    }
}
