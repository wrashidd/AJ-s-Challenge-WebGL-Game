using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BlinkingText : MonoBehaviour
{
    [SerializeField] private float minTime = 0.5f;
    [SerializeField] private float maxTime = 0.5f;

    private float _timer;
    private TextMeshProUGUI _textFlicker;

    private void Start()
    {
        _textFlicker = GetComponent<TextMeshProUGUI>();
        _timer = UnityEngine.Random.Range(minTime, maxTime);
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _textFlicker.enabled = !_textFlicker.enabled;
            _timer = UnityEngine.Random.Range(minTime, maxTime);
        }
    }
}
