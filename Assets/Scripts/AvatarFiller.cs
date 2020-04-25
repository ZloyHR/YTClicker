using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AvatarFiller : MonoBehaviour
{
    private AvatarScripts[] avatarsScripts;
    public GameObject avatarPrefab;
    public GameObject popUp;
    private Button popUpButton;
    private Button[] _buttons;
    private void Start()
    {
        avatarsScripts = Resources.LoadAll<AvatarScripts>("Avatars");
        popUpButton = popUp.transform.GetChild(2).GetComponent<Button>();
        _buttons = new Button[avatarsScripts.Length];
        for (int i = 0; i < avatarsScripts.Length; ++i)
        {
            var avatar = new Avatar(avatarsScripts[i]);
            if (i == 0) avatar.used = true;
            GameObject current;
            current = Instantiate(avatarPrefab, transform);
            current.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = avatar.name;
            current.transform.GetChild(1).GetComponent<Image>().sprite = avatar.image;
            current.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = avatar.cost.ToString();
            _buttons[i] = current.transform.GetChild(3).GetComponent<Button>();
            Avatar avat = avatar;
            var i1 = i;
            if (avatar.used){
                _buttons[i].GetComponent<Image>().color = new Color(0.2971698f,0.6548494f,1,1);
                if (PlayerController.playerAvatarId == i)
                {
                    OnUseClicked(i,avat);
                    _buttons[i].onClick.AddListener(delegate { OnUseClicked(i1, avat); });
                }
                else
                {
                    _buttons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "USE";
                    _buttons[i].onClick.AddListener(delegate { OnUseClicked(i1, avat); });
                }
            }
            else
            {
                _buttons[i].onClick.AddListener(delegate { OnClicked(i1, avat); });
            }
    }
    }

    public void OnClicked(int id,Avatar avatar)
    {
        if (MoneyController.money >= avatar.cost)
        {
            popUp.active = true;
            popUp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = avatar.name;
            popUp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = avatar.description;
            popUp.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = avatar.cost.ToString();
            popUpButton.onClick.AddListener(delegate { OnBuyClicked(id,avatar); });
        }
        else
        {    
            _buttons[id].GetComponent<Animator>().Play("buttonTilt");
        }
    }
    
    public void OnBuyClicked(int id,Avatar avatar)
    {
        if (MoneyController.g.BuyProduct(avatar.cost))
        {
            avatar.used = true;
            avatar.Save();
            _buttons[id].GetComponent<Image>().color = new Color(0.2971698f,0.6548494f,1,1);
            _buttons[id].onClick.RemoveAllListeners();
            _buttons[id].onClick.AddListener(delegate { OnUseClicked(id, avatar); });
            OnUseClicked(id,avatar);
            OnClose();
        }
    }
    
    public void OnUseClicked(int id,Avatar avatar)
    {
        OnUseUnclicked(PlayerController.playerAvatarId);
        PlayerController.playerAvatar = avatar;
        PlayerController.playerAvatarId = id;
        PlayerController.Save();
        _buttons[id].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "USING";
        _buttons[id].interactable = false;
    }
    
    public void OnUseUnclicked(int id)
    {
        _buttons[id].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "USE";
        _buttons[id].interactable = true;
    }

    public void OnClose()
    {
        popUpButton.onClick.RemoveAllListeners();
        popUp.active = false;
    }
}
