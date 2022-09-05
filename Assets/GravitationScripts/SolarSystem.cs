using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class SolarSystem : MonoBehaviour
{
    public float timer = 0.0f;

    // Condition to start and stop the timer
    private bool beginTime = false;

    // Condition used to determine if default solar system is used
    private bool useDefault = false;

    public float G = 100f;
    public float period = 0.1f;

    GameObject[] celestials;

    [Header("Planet Prefabs")]
    [SerializeField] private GameObject EarthPrefab = null;
    [SerializeField] private GameObject MarsPrefab = null;
    [SerializeField] private GameObject MoonPrefab = null;
    [SerializeField] private GameObject JupiterPrefab = null;
    [SerializeField] private GameObject MercuryPrefab = null;
    [SerializeField] private GameObject VenusPrefab = null;
    [SerializeField] private GameObject SaturnPrefab = null;
    [SerializeField] private GameObject UranusPrefab = null;
    [SerializeField] private GameObject NeptunePrefab = null;
    [SerializeField] private GameObject PlutoPrefab = null;

    [Header("UI")]
    [SerializeField] private TMP_InputField g_input = null;
    [SerializeField] private TMP_InputField m1_input = null;
    [SerializeField] private TMP_InputField m2_input = null;
    [SerializeField] private TMP_InputField r_input = null;
    [SerializeField] private TMP_Text time = null;
    [SerializeField] private TMP_Text EarthOrbitTime = null;
    [SerializeField] private TMP_Text MarsOrbitTime = null;
    [SerializeField] private TMP_Text JupiterOrbitTime = null;


    // Update is called once per frame
    private void FixedUpdate()
    {
        Gravity();

        if (beginTime)
        {
            timer += Time.deltaTime;
            time.text = "Time: " + timer.ToString("F2");

        }


    }

    void Gravity()
    {
        if (celestials != null && celestials.Length > 1)
        {
            foreach (GameObject a in celestials)
            {
                foreach (GameObject b in celestials)
                {
                    if (!a.Equals(b))
                    {
                        float m1 = a.GetComponent<Rigidbody>().mass;
                        float m2 = b.GetComponent<Rigidbody>().mass;
                        float r = Vector3.Distance(a.transform.position, b.transform.position);

                        a.GetComponent<Rigidbody>().AddForce((b.transform.position - a.transform.position).normalized *
                            (G * (m1 * m2) / (r * r)));
                    }
                }
            }
        }

        else { return;  }

    }

    void InitialVelocity()
    {
        foreach(GameObject a in celestials)
        {
            if (a.name == "Sun")
            {
                if (!useDefault && m1_input.text != "")
                {
                    a.GetComponent<Rigidbody>().mass = Int32.Parse(m1_input.text);
                }
                else
                {
                    a.GetComponent<Rigidbody>().mass = 333000f;
                }
            }
            foreach(GameObject b in celestials)
            {

                if (!a.Equals(b) && a.name != "Sun")
                {
                    float m2 = b.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);
                    a.transform.LookAt(b.transform);

                    a.GetComponent<Rigidbody>().velocity += a.transform.right * Mathf.Sqrt((G * m2) / r);

                    if (b.name == "Sun")
                    {
                        period = Mathf.Sqrt((4 * Mathf.Pow(3.14f, 2) * Mathf.Pow(r, 3)) / (G * m2));
                        if (a.name.StartsWith("Earth"))
                        {
                            EarthOrbitTime.text = "Earth Period: " + period.ToString();
                        }
                        if (a.name.StartsWith("Mars"))
                        {
                            MarsOrbitTime.text = "Mars Period: " + period.ToString();
                        }
                        if (a.name.StartsWith("Jupiter"))
                        {
                            JupiterOrbitTime.text = "Jupiter Period: " + period.ToString();
                        }
                    }

                }
            }
        }
    }

    public void Default()
    {
        useDefault = true;
        G = 100f;
        Instantiate(EarthPrefab);
        Instantiate(MarsPrefab);
        //Instantiate(MoonPrefab);
        Instantiate(JupiterPrefab);
        Instantiate(MercuryPrefab);
        Instantiate(VenusPrefab);
        Instantiate(SaturnPrefab);
        Instantiate(UranusPrefab);
        Instantiate(NeptunePrefab);
        Instantiate(PlutoPrefab);

        beginTime = true;

        celestials = GameObject.FindGameObjectsWithTag("Celestial");
        InitialVelocity();


    }

    public void SetValues()
    {
        if(G != null)
        {
            G = int.Parse(g_input.text);
        }
        else
        {
            G = 100f;
        }
        float r = Int32.Parse(r_input.text);
        GameObject earth = Instantiate(EarthPrefab, new Vector3(r, 0, 0), Quaternion.identity);
        earth.GetComponent<Rigidbody>().mass = Int32.Parse(m2_input.text);

        beginTime = true;

        celestials = GameObject.FindGameObjectsWithTag("Celestial");
        
        InitialVelocity();

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
