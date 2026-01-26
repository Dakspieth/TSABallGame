using UnityEngine;

public class checkerMovement : MonoBehaviour
{
    GameObject checkerTile;
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        checkerTile = Resources.Load<GameObject>("checkerPrefab");
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject newTile = Instantiate(checkerTile, new Vector3(-6 + (2.05f * i), -4 + (2.05f * j), 0), Quaternion.identity);
                newTile.transform.parent = transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Transform tile in transform)
        {
            tile.transform.position += new Vector3(speed * 10 * Time.deltaTime, speed * 2 * Time.deltaTime, 0);
            if(tile.transform.position.x > 6.3f)
            {
                tile.transform.position -= Vector3.right * 14.36f;
            }
            if(tile.transform.position.y > 4.2f)
            {
                tile.transform.position -= Vector3.up * 10.2f;
            }
        }
    }
}
