using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot_ch : MonoBehaviour
{
    private Slot_pr parent_script;
    // Start is called before the first frame update
    void Start()
    {
        parent_script = transform.parent.GetComponent<Slot_pr>();
        
    }
    private void Update()
    {
        
    }
    public bool HasBall()
    {
        Physics.Raycast(transform.position + new Vector3(0, 0, 2), new Vector3(0, 0, -4), out RaycastHit hit1);
        return hit1.collider != null && hit1.collider.tag == "Ball";
    }
}
