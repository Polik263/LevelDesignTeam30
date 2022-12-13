using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayPlay : MonoBehaviour
{
    [SerializeField] private GameObject nextWall;

    private void Awake()
    {
      nextWall.SetActive(false);
    }

    private void Update()
    {
  
    }

    private void OnDestroy()
    {
      nextWall.SetActive(true);
    }
}
