using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour
{
	void Start () 
    {
        StartCoroutine(Waiter(5));
	}

    IEnumerator Waiter(float wSec)
    {
        yield return new WaitForSeconds(Random.Range(2, wSec));
        Application.LoadLevel(0);
    }
}
