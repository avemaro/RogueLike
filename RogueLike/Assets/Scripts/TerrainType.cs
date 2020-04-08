
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
}