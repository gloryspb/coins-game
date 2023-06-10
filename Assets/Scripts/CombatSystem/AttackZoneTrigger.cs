using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZoneTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }
}
