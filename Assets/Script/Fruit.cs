using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private Color _colorFruit;
    public Color colorFruit { get { return _colorFruit; } }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "mix")
        {
            Destroy(gameObject);
        }
    }
    
}
