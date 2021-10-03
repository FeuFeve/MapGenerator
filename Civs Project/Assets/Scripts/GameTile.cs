public class GameTile
{
    public string type;
    public string feature;

    public bool isWater;
    public bool isImpassable;


    public GameTile(string type)
    {
        this.type = type;

        if (type == "ocean" || type == "coast")
        {
            isWater = true;
        }
        else if (type == "mountain")
        {
            isImpassable = true;
        }
    }

    public void setFeature(string feature)
    {
        this.feature = feature;
    }
}
