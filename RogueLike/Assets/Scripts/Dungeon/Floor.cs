using System;
using System.Collections.Generic;
using UnityEngine;

public class Floor {
    public (int x, int y) floorSize;
    public List<TerrainCell> Terrains { get; private set; } = new List<TerrainCell>();
    public Cell StairPosition;
    public int NumberOfStairs { get; private set; } = 1;
    public Player Player { get; private set; }

    public List<Room> Rooms { get; private set; } = new List<Room>();

    public List<Piece> Pieces { get; private set; } = new List<Piece>();
    public List<Enemy> Enemies { get; private set; } = new List<Enemy>();
    public List<Item> Items { get; private set; } = new List<Item>();
    public List<Trap> Traps { get; private set; } = new List<Trap>();

    public FloorPrinter printer;

    public Floor(int width, int height) {
        Player = new Player(this);
        printer = new FloorPrinter(this);

        floorSize = (width, height);

        for (var x = 0; x < width; x++) {
            for (var y = 0; y < height; y++) {
                var cell = new Cell(x, y);
                Terrains.Add(new TerrainCell(this, cell, TerrainType.wall));
            }
        }
    }

    public void Work() {
        Enemies.RemoveAll(enemy => enemy.IsState(State.Dead));
        Pieces.RemoveAll(piece => piece.IsState(State.Dead));

        foreach (var enemy in Enemies)
            enemy.Work();
        foreach (var trap in Traps)
            trap.Work();
        if (StairPosition == Player.Position) GoNextFloor();

        Enemies.RemoveAll(enemy => enemy.IsState(State.Dead));
        Pieces.RemoveAll(piece => piece.IsState(State.Dead));

        printer.GetSrroundings();
    }

    void GoNextFloor() {
        NumberOfStairs++;

        var newFloor = FloorMaker.NextFloor(this);
        Terrains = newFloor.Terrains;
        StairPosition = newFloor.StairPosition;
        Player.Position = newFloor.Player.Position;
        Player.StorePieces();
        Pieces = newFloor.Pieces;
        Rooms = newFloor.Rooms;
        Traps = newFloor.Traps;
    }

    #region terrain
    public TerrainCell GetTerrainCell(Cell to) {
        foreach (var terrain in Terrains)
            if (terrain.Equals(to.Tuple)) return terrain;
        return null;
    }

    public void SetTerrain(int x, int y, TerrainType terrain) {
        var terrainCell = GetTerrainCell(new Cell(x, y));
        if (terrainCell is null) return;
        terrainCell.type = terrain;
    }


    public TerrainType GetTerrain(int x, int y) {
        return GetTerrain(new Cell(x, y));
    }

    public TerrainType GetTerrain(Cell cell) {
        var terrainCell = GetTerrainCell(cell);
        if (terrainCell is null) return TerrainType.wall;
        return terrainCell.type;
    }

    public List<TerrainType> GetTerrain(List<Cell> cells) {
        var terrains = new List<TerrainType>();
        foreach (var cell in cells)
            terrains.Add(GetTerrain(cell));
        return terrains;

    }
    #endregion

    #region getter
    public Stuff GetStuff(int x, int y) {
        var enemy = GetEnemy(x, y);
        if (enemy != null) return enemy;
        var piece = GetPiece(new Cell(x, y));
        if (piece != null) return piece;
        var item = GetItem(x, y);
        if (item != null) return item;
        var trap = GetTrap(x, y);
        if (trap != null) return trap;
        return null;
    }

    public Item GetItem(int x, int y) {
        foreach (var item in Items) {
            if (item.Position == (x, y)) return item;
        }
        return null;
    }

    public Creature GetCreature(Cell cell) {
        if (Player.Position == cell) return Player;
        var enemy = GetEnemy(cell);
        if (enemy != null) return enemy;
        var piece = GetPiece(cell);
        if (piece != null) return piece;
        return null;
    }

    public Piece GetPiece(Cell cell) {
        foreach (var piece in Pieces)
            if (piece.Position == cell) return piece;
        return null;
    }

    public Enemy GetEnemy(Cell cell) {
        return GetEnemy(cell.x, cell.y);
    }

    public Enemy GetEnemy(int x, int y) {
        foreach (var enemy in Enemies)
            if (enemy.Position == (x, y)) return enemy;
        return null;
    }

    public Enemy[] GetEnemies(Room room) {
        var enemyList = new List<Enemy>();
        foreach (var enemy in Enemies)
            if (enemy.Room == room) enemyList.Add(enemy);
        return enemyList.ToArray();
    }

    public Enemy GetEnemy(Cell from, Direction direction, List<TerrainType> blockTerrans) {
        var nextCell = GetTerrainCell(from);

        while (true) {
            nextCell = nextCell.Next(direction);
            if (blockTerrans.Contains(nextCell.type)) break;
            var enemy = GetEnemy(nextCell);
            if (enemy == null) continue;
            return enemy;
        }
        return null;
    }

    public Trap GetTrap(int x, int y) {
        foreach (var trap in Traps)
            if (trap.Position == (x, y)) return trap;
        return null;
    }

    public Cell GetVacantPosition(TerrainType type) {
        while (true) {
            var cell = GetPosition(type);
            if (GetStuff(cell.x, cell.y) == null) return cell;
        }
    }

    public Cell GetVacantPosition(TerrainType type, bool isRoom) {
        if (!isRoom) return GetVacantPosition(type);

        while (true) {
            var cell = GetPosition(type);
            if (GetRoom(cell) == null) continue;
            if (GetStuff(cell.x, cell.y) == null) return cell;
        }
    }

    public Cell GetPosition(TerrainType type) {
        while (true) {
            var x = UnityEngine.Random.Range(0, floorSize.x - 1);
            var y = UnityEngine.Random.Range(0, floorSize.y - 1);
            if (GetTerrain(x, y) != type) continue;
            return new Cell(x, y);
        }
    }
    #endregion

    public void Remove(Item item) {
        Items.Remove(item);
    }

    public void Remove(Creature creature) {
        if (creature is Enemy) Enemies.Remove((Enemy)creature);
        if (creature is Piece) Pieces.Remove((Piece)creature);
    }

    public Room GetRoom(Cell position) {
        foreach (var room in Rooms) {
            if (room.Contains(position)) return room;
        }

        return null;
    }

    public List<string> Show() {
        if (printer is FloorPrinter)
            return printer.GetMap();
        return new List<string>();
    }
}