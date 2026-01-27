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
                GameObject newTile = Instantiate(checkerTile, new Vector3(-6 + (2.0476f * i), -4 + (2.0476f * j), 0), Quaternion.identity);
                newTile.transform.parent = transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Transform tile in transform)
        {
            tile.transform.position += new Vector3(speed * 10 * Time.deltaTime, speed * 2f * Time.deltaTime, 0);
            if(tile.transform.position.x > 6.3f)
            {
                tile.transform.position -= Vector3.right * 2.0476f*7;
            }
            if(tile.transform.position.y > 4.2f)
            {
                tile.transform.position -= Vector3.up * 2.0476f*5;
            }
        }
    }
}
