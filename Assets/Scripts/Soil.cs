
public enum SoilState
{
    Empty,
    Occupied,
    Ready
}

public class Soil
{
    public SoilState dirtState;
    public Crop crop;
    public Soil(SoilState dirtState, Crop crop)
    {
        this.dirtState = dirtState;
        this.crop = crop;
    }
}
