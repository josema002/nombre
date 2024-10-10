using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Vector2 direccion = Vector2.right;
    public Transform segmetoPrefab;
    public List<Transform> tamañoSerpiente = new List<Transform>();

    private void Start()
    {
        tamañoSerpiente.Add(transform);
        Time.timeScale = 1.0f;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            direccion = Vector2.up;
        } 
        else if (Input.GetKeyDown(KeyCode.S))
        { 
            direccion = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A)) 
        { 
            direccion = Vector2.left; 
        }
        else if (Input.GetKeyDown(KeyCode.D))
        { 
            direccion = Vector2.right;
        }
    }

    private void FixedUpdate()
    {

        for (int i = tamañoSerpiente.Count - 1; i > 0; i--)
        {
            tamañoSerpiente[i].position = tamañoSerpiente[i - 1].position;
        }


        transform.position = new Vector3(Mathf.Round(transform.position.x) + direccion.x,
                                          Mathf.Round(transform.position.y) + direccion.y,
                                          0.0f);
    }

    void Reset()
    {
        for (int i = 1; i <tamañoSerpiente.Count; i++)
        {
            Destroy(tamañoSerpiente[i].gameObject);
        }
        tamañoSerpiente.Clear();
        tamañoSerpiente.Add(transform);

        transform.position = Vector3.zero;

    }

    void Crecer()
    {
        Transform segmentoNuevo = Instantiate(segmetoPrefab);
        segmentoNuevo.position = tamañoSerpiente[tamañoSerpiente.Count-1].position;
        tamañoSerpiente.Add(segmentoNuevo);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("obstaculo"))
        {
            Reset();

        }
        if (collision.CompareTag("food"))
        {
            Crecer();
        }
    }

}
