using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasksController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void AttackRight()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 2);
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().AddTorque(100f);
        Invoke(nameof(DeleteCask), 2.0f);
    }

    void AttackLeft()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(10, 2);
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().AddTorque(-100f);
        Invoke(nameof(DeleteCask), 2.0f);
    }

    void DeleteCask()
    {
        Destroy(gameObject);
    }
}