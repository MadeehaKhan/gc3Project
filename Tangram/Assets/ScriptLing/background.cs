using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour
{
    // Start is called before the first frame update
	public float scrollspeed = -2f;
	public float offset;
	Vector2 sPos;
	float newPos;
    void Start()
    {
		sPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		newPos = Mathf.Repeat(Time.time*scrollspeed, offset);
		transform.position = sPos + Vector2.right*newPos;
    }
}
