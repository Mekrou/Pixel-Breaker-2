using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartText : MonoBehaviour
{
    [SerializeField] Sprite transparentText;
    [SerializeField] Sprite startText;
    [SerializeField] float delayInSeconds;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetInput());
        StartCoroutine(FlashText(delayInSeconds));
    }

    // Coroutine to Validate input on a given time delay.
    private IEnumerator GetInput()
    {
        do
        {
            yield return null;
        } while (!Input.GetKeyDown(KeyCode.Space));

        // Gets current scene's build index.
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        // Loads the next scene using build indexes.
        SceneManager.LoadScene(currentScene + 1);
    }

    private IEnumerator FlashText(float delayInSeconds)
    {

        do
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = transparentText;
            yield return new WaitForSecondsRealtime(delayInSeconds);
            gameObject.GetComponent<SpriteRenderer>().sprite = startText;
            yield return new WaitForSecondsRealtime(delayInSeconds);
        } while (true);
    }
}
