using System;
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
    
    
    
    // Update is called once per frame
    private void FixedUpdate()
    {
        _bubbleGaugeOne.value = _playerOne.GetComponent<BubbleGauge>().Gauge;
        _bubbleGaugeTwo.value = _playerTwo.GetComponent<BubbleGauge>().Gauge;
    }
}
