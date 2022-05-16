using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    private float xRotate; // These are the values used in our Vector3 Later
    private float yRotate;
    private float zRotate;

    [SerializeField]
    int counter = 0; // Initialization of counter used to change random rotation.

    [SerializeField]
    float threshold = 250; // The higher the number, the less often the object will change its rotate values.

    [SerializeField]
    float spinFactor = 0.5f; // Initialization of how quickly the object rotates.

    private void Start()
    {
        Rotate();
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.parent.GetComponent<Ball>().isPaused == true) return;
        Vector3 boxRotater = new Vector3(xRotate, yRotate, zRotate); // Creating our initial Vector with rotation values.
        transform.Rotate(boxRotater * Time.deltaTime * spinFactor); // The main part of the rotation script.
        counter++; // Increases the counter so that there can be change.

        if (counter > threshold)
        {
            Rotate(); // Call the rotate method once our counter hits its threshold.
            counter = 0; // Reset the counter so the script can continue.
        }
    }

    public void Rotate()
    {
        xRotate = Random.Range(1, 360); // This method is used to change the rotation.
        yRotate = Random.Range(1, 360);
        zRotate = Random.Range(1, 360);
    }
}
