using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;


public class CharacterSelection : MonoBehaviour
{
    [SerializeField]
    private List<BubbleType> _bubblesWeapons;
    [SerializeField]
    private List<Sprite> _playerSkins;

    [SerializeField]
    private Image _playerOneSkin;
    [SerializeField]
    private Image _playerTwoSkin;
    [SerializeField]
    private Image _weaponOneSprite;
    [SerializeField] 
    private Image _weaponTwoSprite;
    [SerializeField]
    private TextMeshProUGUI _weaponOneInfo;
    [SerializeField]
    private TextMeshProUGUI _weaponTwoInfo;

    private int _playerOneSkinID;
    private int _playerTwoSkinID;
    private int _weaponOneID;
    private int _weaponTwoID;

    private int PlayerOneSkinID
    {
        get => _playerOneSkinID;
        set
        {
            if (value >= _playerSkins.Count)
                _playerOneSkinID = 0;
            else if(value < 0)
                _playerOneSkinID = _playerSkins.Count - 1;
            else
                _playerOneSkinID = value;
        }
    }
    private int PlayerTwoSkinID
    {
        get => _playerTwoSkinID;
        set
        {
            if(value >= _playerSkins.Count)
                _playerTwoSkinID = 0;
            else if(value < 0)
                _playerTwoSkinID = _playerSkins.Count - 1;
            else
                _playerTwoSkinID = value;
        }
    }
    private int WeaponOneID
    {
        get => _weaponOneID;
        set
        {
            if(value >= _bubblesWeapons.Count)
                _weaponOneID = 0;
            else if(value < 0)
                _weaponOneID = _bubblesWeapons.Count - 1;
            else
                _weaponOneID = value;
        }
    }
    private int WeaponTwoID
    {
        get => _weaponTwoID;
        set
        {
            if(value >= _bubblesWeapons.Count)
                _weaponTwoID = 0;
            else if(value < 0)
                _weaponTwoID = _bubblesWeapons.Count - 1;
            else
                _weaponTwoID = value;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadData();
        UpdateUI();
    }

    private void LoadData()
    {
        string[] saveChoices = PlayerPrefs.GetString("Choices", "0-0-1-1").Split('-');
        PlayerOneSkinID = int.Parse(saveChoices[0]);
        WeaponOneID = int.Parse(saveChoices[1]);
        PlayerTwoSkinID = int.Parse(saveChoices[2]);
        WeaponTwoID = int.Parse(saveChoices[3]);
    }

    public void PlayerOneChoice(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Vector2 input = context.ReadValue<Vector2>().normalized;
        int x = Mathf.RoundToInt(input.x);
        int y = Mathf.RoundToInt(input.y);
        PlayerOneSkinID += x;
        WeaponOneID += y;
        UpdateUI();
    }

    public void PlayerTwoChoice(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        Vector2 input = context.ReadValue<Vector2>().normalized;
        int x = Mathf.RoundToInt(input.x);
        int y = Mathf.RoundToInt(input.y);
        PlayerTwoSkinID += x;
        WeaponTwoID += y;
        UpdateUI();
    }

    private void UpdateUI()
    {
        _playerOneSkin.sprite = _playerSkins[PlayerOneSkinID];
        _playerTwoSkin.sprite = _playerSkins[PlayerTwoSkinID];
        _weaponOneSprite.sprite = _bubblesWeapons[WeaponOneID].prefab.GetComponent<SpriteRenderer>().sprite;
        _weaponOneSprite.color = _bubblesWeapons[WeaponOneID].prefab.GetComponent<SpriteRenderer>().color;
        _weaponOneInfo.text = _bubblesWeapons[WeaponOneID].description;
        _weaponTwoSprite.sprite = _bubblesWeapons[WeaponTwoID].prefab.GetComponent<SpriteRenderer>().sprite;
        _weaponTwoSprite.color = _bubblesWeapons[WeaponTwoID].prefab.GetComponent<SpriteRenderer>().color;
        _weaponTwoInfo.text = _bubblesWeapons[WeaponTwoID].description;
    }

    public void Play()
    {
        PlayerPrefs.SetString("Choices", $"{_playerOneSkinID}-{_weaponOneID}-{_playerTwoSkinID}-{_weaponTwoID}");
        SceneManager.LoadScene("MainGameScene");
        Debug.Log("PLAY");
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
