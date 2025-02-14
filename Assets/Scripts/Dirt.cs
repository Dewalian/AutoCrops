using UnityEngine;

public enum DirtState
{
    Empty,
    Occupied,
    Ready
}

public class Dirt
{
    public DirtState dirtState;
    public GameObject crop;
    public Dirt(DirtState dirtState, GameObject crop)
    {
        this.dirtState = dirtState;
        this.crop = crop;
    }
}
