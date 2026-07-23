using UnityEngine;

public class Missile_Moon : MonoBehaviour
{
    private int hits = 5;
    private bool fall;

    private void FixedUpdate()
    {
        if(hits <= 0)
        {
            fall = true;
        }
        if(fall)
        {
            transform.position += -transform.up * 2f * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("Ow");
        hits--;
    }
}
