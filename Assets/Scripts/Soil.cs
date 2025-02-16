
public enum SoilState
{
    Empty,
    Occupied,
    Ready
}

public class Soil
{
    public SoilState soilState;
    public Crop crop;
    public Soil(SoilState dirtState, Crop crop)
    {
        this.soilState = dirtState;
        this.crop = crop;
    }
}
