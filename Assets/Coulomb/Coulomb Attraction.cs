using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class CoulombAttraction : MonoBehaviour
{
    public float timer = 0.0f;

    public float K = (float)(8.9 * Math.Pow(10, -9));
    public float period = 0.1f;

    [Header("UI")]
    [SerializeField] private TMP_InputField q1_input = null;
    [SerializeField] private TMP_InputField q2_input = null;
    [SerializeField] private TMP_InputField r_input = null;

    GameObject[] charges;


        
    private void Start()
    {
        charges = GameObject.FindGameObjectsWithTag("Charge");
        

    }
    
    // Update is called once per frame
    private void FixedUpdate()
    {
        Coulomb();


    }

    void Coulomb()
    {
        if (charges != null && charges.Length > 1)
        {
            foreach (GameObject a in charges)
            {
                foreach (GameObject b in charges)
                {
                    if (!a.Equals(b))
                    {
                        int q1 = a.GetComponent<Charge>().charge;
                        int q2 = b.GetComponent<Charge>().charge;
                        float r = Vector3.Distance(a.transform.position, b.transform.position);

                        if ((q1 * q2) < 0)
                        {
                            a.GetComponent<Rigidbody>().AddForce((b.transform.position - a.transform.position).normalized *
                            (K * (Math.Abs(q1 * q2)) / (r * r)));
                        }   

                        else
                        {
                            a.GetComponent<Rigidbody>().AddForce(-(b.transform.position - a.transform.position).normalized *
                            (K * (Math.Abs(q1 * q2)) / (r * r)));
                        }

                    }
                }
            }
        }

        else { return; }

    }



    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
