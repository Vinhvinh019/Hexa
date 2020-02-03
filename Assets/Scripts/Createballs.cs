using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Createballs : MonoBehaviour
{
    public List<Transform> balls;
    public Transform liveup;
    public Transform main;
    //public int maxball = 61;
    //public int BallPerSecont = 10;
    //private int Numball = 0;
    //private float Timecount = 0;
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, -50F, 0);
        //InvokeRepeating("Fillball", 1f, 0.5f);
        enabled = false;
    }
    //void Update()
    //{
    //    Timecount += Time.deltaTime;
    //    if (Numball < maxball && Timecount > 0.25f)
    //    {
    //        Instantiate(balls[Random.Range(0, balls.Count)], transform.position + new Vector3(0, Random.Range(-2f, -3f), 0), Quaternion.identity);
    //        Numball++;
    //        Timecount = 0;
    //    }
    //}
    // Update is called once per frame
    //private void Fillball()
    //{
    //    for (int i = -3; i <= 3; i++)
    //    {
    //        if (Numball < maxball)
    //        {

    //            Instantiate(balls[Random.Range(0, balls.Count)], transform.position + new Vector3(i * 2.0F, Random.Range(-2f, -3f), 0), Quaternion.identity);
    //            Numball++;
    //        }
    //        else
    //        {

    //            enabled = true;
    //            CancelInvoke(); break;
    //        }
    //    }
    //}
    public void CreateBall(Vector3 position)
    {
        var newball = Instantiate(balls[Random.Range(0, balls.Count)].transform, position, Quaternion.identity);
        if (Random.Range(0, 5) == 0)
        {
            var bf = Instantiate(liveup.transform, newball.position, Quaternion.identity);
            bf.SetParent(newball);
        }
        StartCoroutine("FadeIn", newball);
        //Numball++;
    }
    public void Breakball(GameObject gameObject)
    {
        Destroy(gameObject);
        //Numball--;
    }

    IEnumerator FadeIn(Transform newball)
    {
        for (float f = 0f; f <= 1; f += 0.1f)
        {
            Color c = newball.GetComponent<Renderer>().material.color;
            c.a = f;
            newball.GetComponent<Renderer>().material.color = c;
            yield return new WaitForSeconds(0.025f);
        }
    }
}
