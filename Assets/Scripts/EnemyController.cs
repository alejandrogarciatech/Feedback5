using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float vidas;
    [SerializeField] private AudioClip impactoSonido;
    [SerializeField] private AudioClip muerteSonido;
    [SerializeField] private float velocidad;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private float distancia;
    [SerializeField] private bool moviendoDerecha;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        RaycastHit2D informacionSuelo = Physics2D.Raycast(controladorSuelo.position, Vector2.down, distancia);

        rb.velocity = new Vector2(velocidad, rb.velocity.y);

        if (informacionSuelo == false)
        {
            Girar();
        }
    }

    private void Girar()
    {
        moviendoDerecha = !moviendoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        velocidad *= -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorSuelo.transform.position, controladorSuelo.transform.position + Vector3.down * distancia);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "bala")
        {
            vidas--;
            Debug.Log("Vida quitada");
            if (vidas == 0)
            {
                Muerte();
            }
        }
    }

    public void TomarDaño(float daño)
    {
        vidas -= daño;
        ControladorSonido.Instance.EjecutarSonido(impactoSonido);
        if (vidas <= 0)
        {
            Muerte();
        }
    }

    public void Muerte()
    {
    // Verificar si el componente Animator está presente en el GameObject o en un hijo del GameObject
    Animator animator = GetComponent<Animator>();
    if (animator == null)
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Si se encuentra el componente Animator, activar la animación de muerte
    if (animator != null)
    {
        animator.SetTrigger("muerto"); // "muerto" es el nombre del parámetro de la animación de muerte en el Animator
        Debug.Log("animación muerte ejecutada");
    }
    else
    {
        Debug.LogWarning("Animator no encontrado");
    }

    // Destruir el GameObject después de la animación
    Destroy(gameObject);
    ControladorSonido.Instance.EjecutarSonido(muerteSonido);
    }
}
