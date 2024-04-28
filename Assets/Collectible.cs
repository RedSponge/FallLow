using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public AudioClip CollectionSound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Collect()
    {
        AudioSource.PlayClipAtPoint(CollectionSound, gameObject.transform.position);
        Destroy(gameObject);
    }
}
