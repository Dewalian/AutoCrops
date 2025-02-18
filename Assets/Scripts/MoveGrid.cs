using UnityEngine;

public class MoveGrid : MonoBehaviour
{
    [SerializeField] Area area;
    private void Start()
    {
        transform.position = new Vector3((float)area.width / 2, (float)area.height / 2);
    }
}
