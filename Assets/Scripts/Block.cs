using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockState
{
    DEFAULT,
    DAMAGED,
    BROKEN
};

public class Block : MonoBehaviour
{
    [Header("Damaged Sprite")]
    [SerializeField] Sprite damaged;
    [Header("Sound Effects")]
    [SerializeField] AudioClip brokeSFX;
    [SerializeField] GameObject burstVFX;
    
    
    GameObject VFX_Holder;

    private BlockState currentState = BlockState.DEFAULT; // All blocks will be intitialized to their standard, undamaged form.

    private GameState gameState;

    private bool alreadyBroken;
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!gameObject.CompareTag("Unbreakable"))
        {
            DestoryBlock(collision);          
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameState = FindObjectOfType<GameState>();
        VFX_Holder = GameObject.Find("VFX_Holder");
        
        StartCoroutine(CheckBlockState());
    }

    void DestoryBlock(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<DiamondController>() && currentState != BlockState.DAMAGED) // Did the diamond hit the block? Is the block already damaged?
            {
                currentState = BlockState.DAMAGED; // If block was hit by diamond and has not been damaged, damage it!
                PlayBurstVFX();
                return;
            }
            else if (currentState == BlockState.DAMAGED) // If block already damaged, break it!
            {
                currentState = BlockState.BROKEN;
            }
            else
            {
                return;
            }
    }

    private IEnumerator CheckBlockState()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(0.1f); // performance optimization

            switch (currentState)
            {
                case BlockState.DEFAULT:
                    break;
                case BlockState.DAMAGED:
                    gameObject.GetComponent<SpriteRenderer>().sprite = damaged;
                    break;
                case BlockState.BROKEN:
                    if (!alreadyBroken)
                    {                                         // Plays right next to our "ear" (Audio Listener)
                        AudioSource.PlayClipAtPoint(brokeSFX, Camera.main.transform.position, 0.5f);
                        PlayBurstVFX();
                        gameObject.SetActive(false);
                        alreadyBroken = true;
                        gameState.BlocksBroken++;
                        gameState.AddPoints();
                    }
                    break;
            }
        }
    }

    private void PlayBurstVFX()
    {
        GameObject tempBurstVFX  = Instantiate<GameObject>(burstVFX, gameObject.transform.position, gameObject.transform.rotation, VFX_Holder.transform);
        tempBurstVFX.transform.localScale = new Vector3(5f, 5f, 5f);
        Destroy(tempBurstVFX, 0.3f);
    }
}
