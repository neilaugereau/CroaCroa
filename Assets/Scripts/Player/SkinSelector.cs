using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class SkinSelector : MonoBehaviour
{
    public int DebugId;
    [SerializeField]
    private List<RuntimeAnimatorController> animators;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        GetComponent<Animator>().runtimeAnimatorController = animators[DebugId];
    }

    // Update is called once per frame
    void Update()
    {
        //string spriteName = "";
        //if (DebugId != -1)
        //    spriteName = _currentSprite.name.Replace("Red", _skinIDtoString[GetComponent<PlayerController>().IsPlayerOne ? GameManager.instance._playerOneSkinID : GameManager.instance._playerTwoSkinID]);
        //else
        //    spriteName = _currentSprite.name.Replace("Red", _skinIDtoString[DebugId]);

        //GetComponent<SpriteRenderer>().sprite = Resources.Load(spriteName) as Sprite;
    }
}
