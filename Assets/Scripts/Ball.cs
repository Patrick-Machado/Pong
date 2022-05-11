using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //public float force = 1;

    //Rigidbody rb;
    /*private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Invoke("ShotBall", 2f);
    }

    void ShotBall()
    {
        float x = Random.Range(1, 5);
        
        rb.AddForce(new Vector3(20 * force, -15, 0), ForceMode.Force);
        Debug.Log("Shot");
    }*/

    Vector3 aceleracao;
    public Vector3 deslocamento;
    public Vector3 velocidade;
    float tempo;
    public float massa = 1;
    public Vector3 forca;
    Vector3 gravidade = new Vector3(0, -9.8f, 0);
    public float elasticidade;
    // Start is called before the first frame update
    void Start()
    {
        deslocamento = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float tempo = Time.fixedDeltaTime;
        aceleracao = forca / massa; // + gravidade;
        velocidade += aceleracao * tempo;
        deslocamento += tempo * velocidade;
        transform.position = deslocamento;
        forca = Vector3.zero;

        if (deslocamento.x < -4.33f)
        {
            velocidade = velocidade.magnitude * elasticidade *
                    Refletir(velocidade.normalized, Vector3.right);
        }

        if (deslocamento.x > 4.33f)
        {
            velocidade = velocidade.magnitude * elasticidade *
                    Refletir(velocidade.normalized, Vector3.left);
        }
        
        if (deslocamento.y > 15.58f)
        {
            velocidade = velocidade.magnitude * elasticidade *
                    Refletir(velocidade.normalized, Vector3.down);
        }

        if(deslocamento.y < -0.16f && coliding)
        {
            velocidade = velocidade.magnitude * elasticidade *
                    Refletir(velocidade.normalized, Vector3.up);
        }

        if (velocidade.magnitude > 0.1f)
            transform.position = deslocamento;
    }
    public void AddForce(Vector3 f)
    {
        forca = f;
    }
    public static Vector3 Refletir(Vector3 vector, Vector3 normal)
    {
        return vector - 2 * Vector3.Dot(vector, normal) * normal;
    }

    bool coliding = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Paddle"))
        {
            coliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Paddle"))
        {
            coliding = false;
        }
    }
}
