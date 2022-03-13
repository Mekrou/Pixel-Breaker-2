using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateScore : MonoBehaviour
{
    [SerializeField] GameState gameState;

    private TextMeshProUGUI textMeshPro;


    private void Start()
    {
        textMeshPro = gameObject.GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        int currentScore = gameState.CurrentScore;
        textMeshPro.text = currentScore.ToString();
    }
}
