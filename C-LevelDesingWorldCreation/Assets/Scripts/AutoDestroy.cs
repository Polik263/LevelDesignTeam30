using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] private GameObject nextWall;
    public float delay = 5f;


    private void Start()
    {
        StartCoroutine(HideObject());
    }


    IEnumerator HideObject()
    {
        yield return new WaitForSeconds(delay);

        nextWall.SetActive(true);
    }
}
