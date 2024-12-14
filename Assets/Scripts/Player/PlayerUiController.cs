using UnityEngine;

public class PlayerUiController : MonoBehaviour
{
    public Transform player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        transform.rotation = player.transform.rotation;



    }
}
