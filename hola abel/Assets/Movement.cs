using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Vector2 direccion = Vector2.right;
    public Transform segmetoPrefab;
    public List<Transform> tama�oSerpiente = new List<Transform>();

    private void Start()
    {
        tama�oSerpiente.Add(transform);
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

        for (int i = tama�oSerpiente.Count - 1; i > 0; i--)
        {
            tama�oSerpiente[i].position = tama�oSerpiente[i - 1].position;
        }


        transform.position = new Vector3(Mathf.Round(transform.position.x) + direccion.x,
                                          Mathf.Round(transform.position.y) + direccion.y,
                                          0.0f);
    }

    void Reset()
    {
        for (int i = 1; i <tama�oSerpiente.Count; i++)
        {
            Destroy(tama�oSerpiente[i].gameObject);
        }
        tama�oSerpiente.Clear();
        tama�oSerpiente.Add(transform);

        transform.position = Vector3.zero;

    }

    void Crecer()
    {
        Transform segmentoNuevo = Instantiate(segmetoPrefab);
        segmentoNuevo.position = tama�oSerpiente[tama�oSerpiente.Count-1].position;
        tama�oSerpiente.Add(segmentoNuevo);

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
