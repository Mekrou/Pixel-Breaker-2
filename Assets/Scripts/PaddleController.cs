using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [SerializeField] GameObject paddle;
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] int worldUnits;
    [SerializeField] bool isCursorVisible;

    private float paddleYPos;
    private float mousePosX;
    private float clampedPaddlePosX;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = isCursorVisible;
    }

    // Update is called once per frame
    void Update()
    {
        paddleYPos = paddle.transform.position.y;
        mousePosX = Input.mousePosition.x;

        clampedPaddlePosX = Mathf.Clamp(mousePosX / Screen.width * worldUnits, minX, maxX);
        
        paddle.transform.position = new Vector3(clampedPaddlePosX, paddleYPos);
    }
}
