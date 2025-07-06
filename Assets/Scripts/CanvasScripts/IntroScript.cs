using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScript : MonoBehaviour
{
    public Canvas c1;
    public Canvas c2;
    public Canvas c3;

    // Start is called before the first frame update
    void Start()
    {
        c1.gameObject.SetActive(true);
        c2.gameObject.SetActive(false);
        c3.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (c1.gameObject.activeSelf)
            {
                c1.gameObject.SetActive(false);
                c2.gameObject.SetActive(true);
            }
            else if (c2.gameObject.activeSelf)
            {
                c2.gameObject.SetActive(false);
                c3.gameObject.SetActive(true);
            }
            else if (c3.gameObject.activeSelf)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
            }
        }
    }
}
