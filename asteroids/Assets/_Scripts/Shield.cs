using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float rotationPerSecond = 0.1f;
    public Color altColor;

    [Header("Set Dynamically")]
    public int levelShown = -1;

    // This non-public variable will not appear in the Inspector
    private Material mat;
    private Color defaultCol;

    private PlayerShip ship;

    // Use this for initialization
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        if (mat != null)
            defaultCol = mat.color;
        ship = GetComponentInParent<PlayerShip>();
        if (ship == null)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Read the current shield level from the Hero Singleton
        int currLevel = Mathf.FloorToInt(ship.shieldLevel);
        if (currLevel == -1) return;
        // If this is different from levelShown...
        if (levelShown != currLevel)
        {
            //Debug.Log("Changing Sheild Level");
            levelShown = currLevel;
            // Adjust the texture offest to show different shield level
            mat.mainTextureOffset = new Vector2(0.2f * levelShown, 0);
        }
        if (ship.startUpShield)
            mat.color = altColor;
        else
            mat.color = defaultCol;
        // Rotate the shield a bit every frame in a time-based way
        float rZ = -(rotationPerSecond * Time.time * 360) % 360f;
        transform.rotation = Quaternion.Euler(0, 0, rZ);
    }
}
