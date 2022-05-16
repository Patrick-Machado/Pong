using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle_Skin : MonoBehaviour
{
    float defaultScale = 3;
    float megaScale = 4.5f;

    public GameObject Ornaments;
    public GameObject Spheres;
    public GameObject Spikes;

    private void Start()
    {
        Game_Manager.Instance.currentScenePaddle = gameObject;
    }
    public void TransformToMegaVisually()
    {
        transform.localScale = new Vector3(megaScale, 1, 1);
        Ornaments.SetActive(true);
        Spikes.SetActive(true);
        Spheres.SetActive(false);
    }
    public void TransformToNormalVisually()
    {
        transform.localScale = new Vector3(defaultScale, 1, 1);
        Ornaments.SetActive(false);
        Spikes.SetActive(false);
        Spheres.SetActive(true);

    }


}
