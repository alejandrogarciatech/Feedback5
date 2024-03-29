using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidad;
    public float daño;

    void Update()
    {
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemigo"))
        {
            other.GetComponent<EnemyController>().TomarDaño(daño);
            Destroy(gameObject);
        }
    }
}
