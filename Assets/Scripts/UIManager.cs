using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Refs
    [SerializeField]
    private Slider _bubbleGaugeOne;
    [SerializeField]
    private Slider _bubbleGaugeTwo;
    [SerializeField]
    private GameObject _playerOne;
    [SerializeField] 
    private GameObject _playerTwo;
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        _playerOne.GetComponent<BubbleGauge>().OnGaugeChange.AddListener(UpdateSliderOne);
    }

    // Update is called once per frame
    void UpdateSliderOne(float value, bool isPlayerOne)
    {
        if(isPlayerOne)
            _bubbleGaugeOne.value = value;
        else
            _bubbleGaugeTwo.value = value;
    }
}
