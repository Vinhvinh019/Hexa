using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot_key : MonoBehaviour
{
    private Slot_pr parent_script;
    // Start is called before the first frame update
    void Start()
    {
        parent_script = transform.parent.GetComponent<Slot_pr>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        parent_script.refresh_board();
    }
    private void OnTriggerExit(Collider other)
    {
        parent_script.refresh_board();
    }
    public bool HasBall()
    {
        Physics.Raycast(transform.position + new Vector3(0, 0, 2), new Vector3(0, 0, -4), out RaycastHit hit1);
        return hit1.collider != null && hit1.collider.tag == "Ball";
    }
}