
public enum TerrainType {
    wall = '◆', breakableWall = '☆', land = '　', water = '◇'
}

public static class TerrainTypeExtend {
    public static TerrainType GetTrrainType(char data) {
        switch (data) {
            case '◆': return TerrainType.wall;
            case '☆': return TerrainType.breakableWall;
            case '◇': return TerrainType.water;
        }
        return TerrainType.land;
    }

    public static string GetString(this TerrainType type) {
        switch (type) {
            case TerrainType.wall: return "◆";
            case TerrainType.breakableWall: return "◆";
            case TerrainType.land: return "　";
            default: return "◇";
        }
    }
}