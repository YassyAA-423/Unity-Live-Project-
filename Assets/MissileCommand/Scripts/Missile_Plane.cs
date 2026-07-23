using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile_Plane : MonoBehaviour
{
    public float speed = .25f;
    public bool moveRight;
    public float timer, threshold;

    // Start is called before the first frame update
    void Start()
    {
        Rotate();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > threshold)
        {
            Destroy(gameObject);
        }
        if (moveRight)
        {
            transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(-1, 0, 0) * speed * Time.deltaTime;
        }
    }

    public void Rotate()
    {
        if (moveRight)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Boundary" || collision.gameObject.tag == "NPC")
        {
            moveRight = !moveRight;
            Rotate();
        }

    }
}
