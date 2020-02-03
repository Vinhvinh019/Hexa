using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot_pr : MonoBehaviour
{
    float time = 0;
    public Transform createball;
    Createballs createballs_s;
    bool start_check = true;
    // Start is called before the first frame update
    void Start()
    {
        createballs_s = createball.GetComponent<Createballs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (start_check)
        {
            time += Time.deltaTime;
            if (time > 1.5)
            {
                List<Transform> empty = new List<Transform>();
                foreach (Transform ch in transform)
                {
                    if (ch.name == "slot_key" ? !ch.GetComponent<Slot_key>().HasBall() : !ch.GetComponent<Slot_ch>().HasBall())
                    {
                        empty.Add(ch);
                    }
                }
                if (empty.Count > 0)
                    foreach (Transform ch in empty)
                    {
                        createballs_s.CreateBall(ch.position);
                    }
                time = 0f;
                start_check = false;
            }
        }
    }
    public void refresh_board()
    {
        time = 0;
    }
    public void StartCheck()
    {
        time = 0;
        start_check = true;
    }
}
