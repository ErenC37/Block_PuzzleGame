using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CountDown : MonoBehaviour
{
   [SerializeField] private Image _timerImg;

   [SerializeField] private TMP_Text _timerText;

   [SerializeField] public float _currentTime;

   [SerializeField] private float _duration;


   
   void Start()
   {
    _currentTime = _duration;
    _timerText.text = _currentTime.ToString();
    StartCoroutine(UpdateTime());
   }

    void Update()
    {
        if(_currentTime == 0)
        {
           GameEvents.GameOver(false);
        }
    }

   
   private IEnumerator UpdateTime()
   {
    while(_currentTime >= 0)
    {
        _timerImg.fillAmount = Mathf.InverseLerp(0, _duration, _currentTime);
        _timerText.text = _currentTime.ToString();
        yield return new WaitForSeconds(1f);
        _currentTime--;
    }
    yield return null;
   }
}
