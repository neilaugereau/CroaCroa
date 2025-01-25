using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.Windows;


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
            Debug.Log($"SkinOne : {_playerOneSkinID}");
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
            Debug.Log($"SkinTwo : {_playerTwoSkinID}");
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
            Debug.Log($"WeaponOne : {_weaponOneID}");
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
            Debug.Log($"WeaponTwo : {_weaponTwoID}");
        }
    }
    [SerializeField]
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerOneChoice(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Vector2 input = context.ReadValue<Vector2>().normalized;
        Debug.Log(input);
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
        _weaponTwoSprite.sprite = _bubblesWeapons[WeaponTwoID].prefab.GetComponent<SpriteRenderer>().sprite;
        _weaponOneInfo.text = _bubblesWeapons[WeaponOneID].description;
        _weaponTwoInfo.text = _bubblesWeapons[WeaponTwoID].description;
    }

    public void Play()
    {

    }
}
