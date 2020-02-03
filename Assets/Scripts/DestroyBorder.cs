using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBorder : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform createball;
    Createballs createballs_s;
    void Start()
    {
        createballs_s = createball.GetComponent<Createballs>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != null) { 
        createballs_s.Breakball(collision.gameObject);
        }
    }

}
