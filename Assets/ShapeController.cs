using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeController : MonoBehaviour
{
    
    private float moveSpeed = 0.5f;
    private float scrollSpeed = 10f;

    private float y = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            y += 45;
            transform.localRotation = Quaternion.Euler(0, y , 0);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            y -= 45;
            transform.localRotation = Quaternion.Euler(0, y, 0);
        }

        
    }
}
