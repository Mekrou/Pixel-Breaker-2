using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameState : MonoBehaviour
{
    // config params
    [Range(0.1f, 10f)] [SerializeField] private float timeScale = 1f;
    [SerializeField] private int scorePointsPerBlockBroken = 44; // how many points you get per block broken

    // state variables
    [SerializeField] private int currentScore = 0; // Serialied for debug purposes.

    [SerializeField] private int blocksBroken = 0; // Serialied for debug purposes.

    [SerializeField] private int amountOfBlocksInScene;

    public int BlocksBroken
    {
        get { return blocksBroken; }

        set { blocksBroken = value; }
    }

    public int AmountOfBlocksInScene
    {
        get { return amountOfBlocksInScene; }
        private set { amountOfBlocksInScene = value; }
    }

    public int CurrentScore
    {
        get { return currentScore; }
        set { currentScore = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        // Initializes amountOfBlocksInScene by looping through all of "Blocks", our container gameObject that holds blocks, and adding to its counter.
        foreach (Transform child in GameObject.Find("Blocks").transform)
        {
            if (!child.CompareTag("Unbreakable"))
            {
                AmountOfBlocksInScene += 1;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = timeScale;

        EvaluateIfGameWasWon();
    }

    /// <summary>
    /// Checks if the game was won by comparing the amount of blocks broken to the initial amount of blocks in the scene.
    /// Loads the main menu scene (buildIndex 0) if the game was won.
    /// </summary>
    void EvaluateIfGameWasWon()
    {
        if (blocksBroken == amountOfBlocksInScene)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void AddPoints()
    {
        CurrentScore += scorePointsPerBlockBroken;
    }
}
