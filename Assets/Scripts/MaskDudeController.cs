using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MaskDudeController : MonoBehaviour
{

    int puntuacion = 0;
    bool enSuelo = true;
    private int contadorSaltos = 0;
    public float fuerzaSalto = 1;
    Rigidbody2D rigidComponent;
    Animator animatiorComponent;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Ejecutado al inicio");
        //para inicializar variables
        rigidComponent = gameObject.GetComponent<Rigidbody2D>();
        animatiorComponent = gameObject.GetComponent<Animator>();
        Debug.Log(rigidComponent);
    }

    // Update is called once per frame
    void Update()
    {
        //Lógica de la aplicación
        //Debug.Log("Ejecución en Update");
    }

    private void FixedUpdate()
    {
        //Ejecuta una vez por cada frame, teniendo en cuenta las características del PC
        //Lógica de movimientos y física -> Renderizado
        //Debug.Log("Ejecución en FixedUpdate");
        if (Input.GetKey("a"))
        {
            //Debug.Log("Pulsada tecla izquierda");
            //Debug.Log(transform.localPosition);
            transform.localPosition = new Vector3((float)(transform.localPosition.x - 0.1), transform.localPosition.y, transform.localPosition.z);
            transform.rotation = Quaternion.Euler(0, -180, 0);
            animatiorComponent.SetBool("corriendo", true);
        }
        else if (Input.GetKey("d"))
        {
            //Debug.Log("Pulsada tecla derecha");
            //Debug.Log(transform.localPosition);
            transform.localPosition = new Vector3((float)(transform.localPosition.x + 0.1), transform.localPosition.y, transform.localPosition.z);
            transform.rotation = Quaternion.identity;
            animatiorComponent.SetBool("corriendo", true);
        }
        else
        {
            animatiorComponent.SetBool("corriendo", false);
        }

        if (Input.GetKey("w") | Input.GetKey("space"))
        {
            if (enSuelo)
            {
                Debug.Log("Pulsada tecla salto");
                rigidComponent.velocity = Vector2.up * 5f;
                animatiorComponent.SetBool("saltando", true);
                enSuelo = false;
            }
            else
            {
                animatiorComponent.SetBool("saltando", false);
            }
        }
        
        else if (Input.GetKey("s"))
        {
            Debug.Log("Pulsada tecla abajo");
            rigidComponent.velocity = Vector2.down * 10f;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "suelo")
        {
            enSuelo = true;
            Debug.Log("Aterrizando");
        }
        else if (other.gameObject.tag == "enemigo")
        {
            puntuacion++;
            Debug.Log("Ganas un punto");
            if (puntuacion == 5)
            {
                Debug.Log("Puntuación máxima alcanzada");
            }
        }
    }
}