using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public Rigidbody2D hook;
    public Rigidbody2D rb;
    public GameObject nextPlayer;

    public float time = .15f;
    public float MaxDrugDistance = 2f;
    private bool isPressed = false;



    private void Update()
    {
        if (isPressed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector3.Distance(mousePos, hook.position) > MaxDrugDistance)
                rb.position = hook.position + (mousePos -  hook.position).normalized * MaxDrugDistance;
            else
                rb.position = mousePos;
        }
    }
    void OnMouseDown()
    {
        isPressed = true;
        rb.isKinematic = true;
    }

    private void OnMouseUp()
    {
        isPressed = false;
        rb.isKinematic = false;
        StartCoroutine(Release());
    }


    IEnumerator Release()
    {
        yield return new WaitForSeconds(time);
        GetComponent<SpringJoint2D>().enabled = false;
        this.enabled = false;

        yield return new WaitForSeconds(1);

        if (nextPlayer != null)
            nextPlayer.SetActive(true);
        else
        {
            Enemy.EnemiesAlive = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}
