using UnityEngine;
using System.Collections;

public class Stairs : MonoBehaviour
{
	public bool active = true;
    public Type type;
    public float char_positionx;
    public float char_positiony;
    public GameObject camera;
    public float x;
    public float y;
    public float z;

    private bool colliding = false;
    private Collider2D c;

    public enum Type
    {
        NORMAL, CAMERA
    };

    void Start()
    {
        if (active == false)
        {
            if (GameManager.instance.dayData.DataDictionary.ContainsKey(gameObject.name))
            {
                if (GameManager.instance.dayData.DataDictionary[gameObject.name])
                {
                    active = true;
                }
            }
        }
    }

    void Update()
    {
        if (colliding == true && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(StairsCoroutine());
            colliding = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
		if (!active)
		{
			return;
		}

        if (col.gameObject.tag == "Player")
        {
            colliding = true;
            c = col;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
            colliding = false;
    }

    IEnumerator StairsCoroutine()
    {
        yield return StartCoroutine(SceneManager.instance.fade_black());
        c.transform.position = new Vector2(char_positionx, char_positiony);

        if (type == Type.CAMERA)
        {
            camera.transform.position = new Vector3(x, y, z);
        }
        yield return null;
        yield return StartCoroutine(SceneManager.instance.fade_out());
    }
}
