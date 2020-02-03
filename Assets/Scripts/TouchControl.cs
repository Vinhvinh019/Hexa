using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TouchControl : MonoBehaviour
{
    public Transform creator;
    public Transform Slot;
    public Transform text;
    public Transform Status;
    public float swap_time = 0.5f;
    public int min_success = 4;
    Createballs creator_script;
    Slot_pr Slot_script;
    status Status_script;
    private Transform selectedball;
    private Transform selectedball2;
    bool onTurn = false;

    void Start()
    {
        creator_script = creator.GetComponent<Createballs>();
        Slot_script = Slot.GetComponent<Slot_pr>();
        Status_script = Status.GetComponent<status>();
        Physics.IgnoreLayerCollision(8, 8);
        Physics.gravity = new Vector3(0, -40F, 0);
    }

    void Update()
    {
        if (!onTurn && (Input.touchCount > 0 || Input.GetMouseButtonDown(0)))
        {
            Ray touchRay = new Ray();
            if (Input.touchCount > 0)
            {
                Touch touch = Input.touches[0];
                touchRay = Camera.main.ScreenPointToRay(touch.position);

            }
            else
            {

                touchRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            }
            Physics.Raycast(touchRay, out RaycastHit hit);
            if (hit.collider != null && hit.collider.gameObject.tag == "Ball" && hit.collider.gameObject.transform != selectedball)
            {

                if (selectedball == null || (Vector3.Distance(selectedball.transform.position, hit.collider.gameObject.transform.position) > 2.5f))
                {
                    if (selectedball != null)
                        selectedball.GetComponent<Outline>().enabled = false;
                    if (selectedball == hit.collider.gameObject.transform)
                    {
                        selectedball = null;
                    }
                    else
                    {
                        selectedball = hit.collider.gameObject.transform;
                        selectedball.GetComponent<Outline>().enabled = true;
                    }
                }
                else
                {
                    onTurn = true;
                    selectedball2 = hit.collider.gameObject.transform;
                    StartCoroutine("SwapBall");
                    selectedball.GetComponent<Outline>().enabled = false;
                }
            }
        }
    }
    IEnumerator SwapBall()
    {
        int i = 0;
        Vector3 p1 = selectedball.transform.position;
        Vector3 p2 = selectedball2.transform.position;
        var c1 = Instantiate(selectedball);
        var c2 = Instantiate(selectedball2);
        c1.GetComponent<SphereCollider>().enabled = false;
        c2.GetComponent<SphereCollider>().enabled = false;
        Destroy(c1.GetComponent<Rigidbody>());
        Destroy(c2.GetComponent<Rigidbody>());
        selectedball.GetComponent<Renderer>().enabled = false;
        selectedball2.GetComponent<Renderer>().enabled = false;
        if (selectedball.childCount > 0) { selectedball.GetChild(0).GetComponent<Renderer>().enabled = false; }
        if (selectedball2.childCount > 0) { selectedball2.GetChild(0).GetComponent<Renderer>().enabled = false; }
        //
        
        for (float t = 0; t <= swap_time; t += Time.deltaTime)
        {
            i++;
            Vector3 m = (p1 - p2).normalized * Time.deltaTime / swap_time;
            c1.position -= m * 2;
            c2.position += m * 2;

            yield return new WaitForSeconds(Time.deltaTime);
        }
        selectedball.position = c1.position;
        selectedball2.position = c2.position;
        selectedball.GetComponent<Renderer>().enabled = true;
        selectedball2.GetComponent<Renderer>().enabled = true;
        if (selectedball.childCount > 0) { selectedball.GetChild(0).GetComponent<Renderer>().enabled = true; }
        if (selectedball2.childCount > 0) { selectedball2.GetChild(0).GetComponent<Renderer>().enabled = true; }
        Destroy(c1.gameObject); Destroy(c2.gameObject);
        //text.GetComponent<TextMeshProUGUI>().text = CheckSameNear(selectedball).Count.ToString() + " " + CheckSameNear(selectedball2).Count.ToString();
        breakMatch(new List<Transform> { selectedball, selectedball2 });
        selectedball = selectedball2 = null;
        onTurn = false;
        Status_script.AddTurns(-1);
        yield return new WaitForSeconds(Time.deltaTime);
    }
    void breakMatch(List<Transform> l_trs)
    {
        List<Transform> alreadycheck = new List<Transform>();
        List<List<Transform>> list_success = new List<List<Transform>>();
        foreach (var trs in l_trs)
        {
            if (!alreadycheck.Contains(trs))
            {
                var match = CheckSameNear(trs);
                alreadycheck.AddRange(match);
                if (match.Count >= min_success)
                {
                    list_success.Add(match);
                }
            }
        }

        foreach (var gr_ball in list_success)
        {
            foreach (var ball in gr_ball)
            {
                //ball.GetComponent<Renderer>().material.color = Color.black;
                creator_script.Breakball(ball.gameObject);
                if(ball.childCount>0&&ball.GetChild(0).name== "AddTurn(Clone)")
                {
                    Status_script.AddTurns(1);
                }
            }
            Status_script.AddScores(gr_ball.Count);
        }
        if (list_success.Count > 0)
        {
            Slot_script.StartCheck();
        }
    }
    List<Transform> CheckSameNear(Transform trs, List<Transform> math = null)
    {
        if (math == null)
        {
            math = new List<Transform>();
        }
        math.Add(trs);
        int layerMask = ~(1 << 2);
        Collider[] hitColliders = Physics.OverlapSphere(trs.position, 1.5f, layerMask);
        foreach (Collider collider in hitColliders)
        {
            if (collider.name == trs.name && !math.Contains(collider.transform))
            {
                math = CheckSameNear(collider.transform, math);
            }
        }
        return math;

    }
}
