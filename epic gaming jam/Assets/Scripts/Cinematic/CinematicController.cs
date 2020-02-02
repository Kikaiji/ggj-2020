using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicController : MonoBehaviour
{

    public GameObject title;
    private float titleOpacity = 1;

    public GameObject withoutTent;
    private float panY = 0;


    public GameObject withTent;

    public GameObject nomad;
    
    void Start()
    {
        StartCoroutine(HideTitle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator HideTitle()
    {
        yield return new WaitForSeconds(1);
        titleOpacity = 0;
        title.SetActive(false);

        withoutTent.SetActive(true);
        StartCoroutine(PanUp());
    }

    IEnumerator PanUp()
    {
        while (panY < 11) {
            yield return new WaitForEndOfFrame();
            panY += 0.05f;
            transform.position = new Vector3(transform.position.x, panY, transform.position.z);
        }
        yield return new WaitForSeconds(3);
        withoutTent.SetActive(false);
        withTent.SetActive(true);
        nomad.SetActive(true);
        StartCoroutine(PanDown());
    }

    IEnumerator PanDown()
    {
        while (panY > 0)
        {
            yield return new WaitForEndOfFrame();
            panY -= 0.05f;
            transform.position = new Vector3(transform.position.x, panY, transform.position.z);
        }
        yield return new WaitForSeconds(3);
        SceneManager.LoadSceneAsync("TownScene");
    }
}
