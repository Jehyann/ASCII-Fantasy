using ASCIIFantasy;
using System;
using System.Diagnostics;
using System.Net.Http.Headers;

public class Map
{
    public const char TALL_GRASS = '#';
    public const int RATE_COMBAT = 10;

    public char[][] mapTile { get; set; }
    public int width { get; set; }// largeur
    public int height { get; set; } // hauteur
    public int positionX { get; set; }
    public int positionY { get; set; }
    public int widthGap { get; set; } = 4;
    public int heightGap { get; set; } = 2;

    public bool inDialogue { get; set; }
    public List<string> dialogues { get; set; }
    public int currentDialogueIndex { get; set; }
    public char nextCell { get; set; }
    public bool InDialogue
    {
        get { return inDialogue; }
        set { inDialogue = value; }
    }
    public Map()
    {
    }
    public Map(int width, int height)
    {
        this.width = width;
        this.height = height;
        mapTile = new char[width][];
        for (int i = 0; i < width; i++)
        {
            mapTile[i] = new char[height];
        }

        InitializeMap();

        dialogues = new List<string>
        {
            "NPC: Hello, adventurer!",
            "NPC: How can I assist you?",
            "NPC: Feel free to ask me any questions!"
        };

        inDialogue = false;
        currentDialogueIndex = 0;
    }

    private void InitializeNextMap()
    {
        nextCell = ' ';

        if (MapArray.instance.maps[99 + Player.instance.mapGlobalIndex[0]][99 + Player.instance.mapGlobalIndex[1]] != null)
        {
            MapArray.instance.activeMap = MapArray.instance.maps[99 + Player.instance.mapGlobalIndex[0]][99 + Player.instance.mapGlobalIndex[1]];
            MapArray.instance.activeMap.SetPlayer(Player.instance.positionX, Player.instance.positionY);
        }
        else
        {
            Map newMap = new Map(Program.width, Program.height);

        }
    }
    private void InitializeMap()
    {
        positionX = Player.instance.positionX;
        positionY = Player.instance.positionY;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                mapTile[i][j] = ' ';
            }
        }

        // Borders
        for (int i = 0; i < width; i++)
        {
            if (i < (width / 2) - widthGap || i > (width / 2) + widthGap)
            {

                mapTile[i][0] = '-';
                mapTile[i][height - 1] = '-';
            }
            else
            {
                mapTile[i][0] = ' ';
                mapTile[i][height - 1] = ' ';
            }
        }
        for (int j = 0; j < height; j++)
        {
            if (j < (height / 2) - heightGap || j > (height / 2) + heightGap)
            {
                mapTile[0][j] = '|';
                mapTile[width - 1][j] = '|';
            }
            else
            {
                mapTile[0][j] = ' ';
                mapTile[width - 1][j] = ' ';
            }
        }

        GenerateBuilding();
        GenerateTallGrass();
        GenerateChest();
        GenerateNPC();
        nextCell = mapTile[positionX][positionY];
        mapTile[positionX][positionY] = 'P'; // Player
        MapArray.instance.maps[99 + Player.instance.mapGlobalIndex[0]][99 + Player.instance.mapGlobalIndex[1]] = this;
        MapArray.instance.activeMap = this;
    }

    public void DisplayMap()
    {
        Console.Clear();
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                if (mapTile[i][j] == 'P')
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(mapTile[i][j]);
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(mapTile[i][j]);
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine($" Zone : {Player.instance.mapGlobalIndex[0]} , {Player.instance.mapGlobalIndex[1]}\n");
        Console.WriteLine(" Press E to interact with a npc '8' or a chest '='.");
    }
    public void DrawHouse1(int x, int y)
    {
        mapTile[x + 2][y] = '_';
        mapTile[x + 3][y] = 'm';
        mapTile[x + 4][y] = '_';

        mapTile[x][y + 1] = '/';
        mapTile[x + 1][y + 1] = '\\';
        mapTile[x + 2][y + 1] = '_';
        mapTile[x + 3][y + 1] = '_';
        mapTile[x + 4][y + 1] = '_';
        mapTile[x + 5][y + 1] = '\\';

        mapTile[x][y + 2] = '|';
        mapTile[x + 1][y + 2] = '_';
        mapTile[x + 2][y + 2] = '|';
        mapTile[x + 3][y + 2] = '"';
        mapTile[x + 4][y + 2] = '"';
        mapTile[x + 5][y + 2] = '|';
    }

    public void DrawHouse2(int x, int y)
    {
        mapTile[x + 2][y] = '[';
        mapTile[x + 3][y] = ']';
        mapTile[x + 4][y] = '_';
        mapTile[x + 5][y] = '_';
        mapTile[x + 6][y] = '_';

        mapTile[x + 1][y + 1] = '/';
        mapTile[x + 6][y + 1] = '/';
        mapTile[x + 7][y + 1] = '\\';

        mapTile[x][y + 2] = '/';
        mapTile[x + 1][y + 2] = '_';
        mapTile[x + 2][y + 2] = '_';
        mapTile[x + 3][y + 2] = '_';
        mapTile[x + 4][y + 2] = '_';
        mapTile[x + 5][y + 2] = '/';
        mapTile[x + 6][y + 2] = '_';
        mapTile[x + 7][y + 2] = '_';
        mapTile[x + 8][y + 2] = '\\';

        mapTile[x][y + 3] = '|';
        mapTile[x + 1][y + 3] = '[';
        mapTile[x + 2][y + 3] = ']';
        mapTile[x + 3][y + 3] = '[';
        mapTile[x + 4][y + 3] = ']';
        mapTile[x + 5][y + 3] = '|';
        mapTile[x + 6][y + 3] = '|';
        mapTile[x + 7][y + 3] = '|';
        mapTile[x + 8][y + 3] = '|';
    }

    public void Draw1TallGrass(int x, int y)
    {
        mapTile[x][y] = TALL_GRASS;
    }

    public void DrawRoundTallGrass(int x, int y)
    {
        mapTile[x][y] = TALL_GRASS;
        mapTile[x + 1][y] = TALL_GRASS;
        mapTile[x - 1][y] = TALL_GRASS;
        mapTile[x + 2][y] = TALL_GRASS;

        mapTile[x][y + 1] = TALL_GRASS;
        mapTile[x - 1][y + 1] = TALL_GRASS;
        mapTile[x - 2][y + 1] = TALL_GRASS;
        mapTile[x + 1][y + 1] = TALL_GRASS;
        mapTile[x + 2][y + 1] = TALL_GRASS;
        mapTile[x + 3][y + 1] = TALL_GRASS;

        mapTile[x][y + 2] = TALL_GRASS;
        mapTile[x - 1][y + 2] = TALL_GRASS;
        mapTile[x - 2][y + 2] = TALL_GRASS;
        mapTile[x - 3][y + 2] = TALL_GRASS;
        mapTile[x + 1][y + 2] = TALL_GRASS;
        mapTile[x + 2][y + 2] = TALL_GRASS;
        mapTile[x + 3][y + 2] = TALL_GRASS;
        mapTile[x + 4][y + 2] = TALL_GRASS;

        mapTile[x][y + 3] = TALL_GRASS;
        mapTile[x - 1][y + 3] = TALL_GRASS;
        mapTile[x - 2][y + 3] = TALL_GRASS;
        mapTile[x + 1][y + 3] = TALL_GRASS;
        mapTile[x + 2][y + 3] = TALL_GRASS;
        mapTile[x + 3][y + 3] = TALL_GRASS;

        mapTile[x][y + 4] = TALL_GRASS;
        mapTile[x + 1][y + 4] = TALL_GRASS;
        mapTile[x - 1][y + 4] = TALL_GRASS;
        mapTile[x + 2][y + 4] = TALL_GRASS;
    }

    public void DrawNPC(int x, int y)
    {
        mapTile[x][y] = '8';
    }

    public void DrawTree(int x, int y)
    {
        mapTile[x][y + 4] = '|';

        mapTile[x][y] = '^';

        mapTile[x][y + 1] = '^';
        mapTile[x - 1][y + 1] = '^';
        mapTile[x + 1][y + 1] = '^';

        mapTile[x][y + 2] = '^';
        mapTile[x - 1][y + 2] = '^';
        mapTile[x - 2][y + 2] = '^';
        mapTile[x + 1][y + 2] = '^';
        mapTile[x + 2][y + 2] = '^';

        mapTile[x][y + 3] = '^';
        mapTile[x - 1][y + 3] = '^';
        mapTile[x - 2][y + 3] = '^';
        mapTile[x - 3][y + 3] = '^';
        mapTile[x + 1][y + 3] = '^';
        mapTile[x + 2][y + 3] = '^';
        mapTile[x + 3][y + 3] = '^';

    }

    public void SetPlayer(int posX, int posY)
    {
        Debug.WriteLine(posX + "\n");
        Debug.WriteLine(posY + "\n");
        mapTile[positionX][positionY] = nextCell;
        positionX = posX;
        positionY = posY;
        nextCell = mapTile[positionX][positionY];
        mapTile[positionX][positionY] = 'P';
    }
    public void MovePlayer(int moveX, int moveY)
    {
        int nextPosX = positionX + moveX;
        int nextPosY = positionY + moveY;
        if (nextPosX != 0 && nextPosX != width && nextPosY != 0 && nextPosY != height)
        {
            if (mapTile[nextPosX][nextPosY] == ' ' || mapTile[nextPosX][nextPosY] == '#')
            {
                mapTile[positionX][positionY] = nextCell;
                nextCell = mapTile[nextPosX][nextPosY];
                positionX = nextPosX;
                positionY = nextPosY;
                Player.instance.positionX = positionX;
                Player.instance.positionY = positionY;
                mapTile[positionX][positionY] = 'P';
                if (nextCell == '#')
                {
                    Random rnd = new Random();
                    int rollCombat = rnd.Next(0, 100);
                    if (rollCombat <= RATE_COMBAT)
                    {
                        int levelCircle = 0;
                        MapArray.instance.maps[99 + Player.instance.mapGlobalIndex[0]][99 + Player.instance.mapGlobalIndex[1]] = this;
                        (int x, int y) normalized = NormalizePoint(99 + Player.instance.mapGlobalIndex[0], 99 + Player.instance.mapGlobalIndex[1]);
                        int distanceFromCenter = (int)Math.Sqrt(normalized.x * normalized.x + normalized.y * normalized.y);
                        if (distanceFromCenter > 10)
                        {
                            levelCircle = 1;
                        }
                        else if (distanceFromCenter > 20)
                        {
                            levelCircle = 2;
                        }
                        else if (distanceFromCenter > 30)
                        {
                            levelCircle = 3;
                        }
                        else if (distanceFromCenter > 40)
                        {
                            levelCircle = 4;
                        }
                        else if (distanceFromCenter > 50)
                        {
                            levelCircle = 5;
                        }
                        else if (distanceFromCenter > 60)
                        {
                            levelCircle = 6;
                        }
                        else if (distanceFromCenter > 70)
                        {
                            levelCircle = 7;
                        }
                        else if (distanceFromCenter > 80)
                        {
                            levelCircle = 8;
                        }
                        else if (distanceFromCenter > 90)
                        {
                            levelCircle = 9;
                        }
                        Combat newCombat = new Combat(Player.instance, levelCircle);
                    }
                }
            }
        }
        else if (nextPosX == 0 || nextPosX == width)
        {
            if (nextPosY <= height / 2 + heightGap && nextPosY >= height / 2 - heightGap)
            {
                SwapZone();
            }
        }
        else if (nextPosY == 0 || nextPosY == height)
        {
            if (nextPosX <= width / 2 + widthGap && nextPosX >= width / 2 - widthGap)
            {
                SwapZone();

            }
        }

    }

    public void DisplayDialog()
    {
        while (InDialogue && HasMoreDialogues())
        {
            string currentDialogue = GetCurrentDialogue();
            Console.WriteLine(currentDialogue);
            Console.WriteLine("\n");
            Console.WriteLine("Appuyez sur Entrée pour continuer...");

            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey(true);
            } while (keyInfo.Key != ConsoleKey.Enter);

            NextDialogue();
        }

        InDialogue = false;
        Console.Clear();
    }



    public string GetCurrentDialogue()
    {
        return dialogues[currentDialogueIndex];
    }

    public void NextDialogue()
    {
        Console.Clear();
        currentDialogueIndex++;

        if (currentDialogueIndex >= dialogues.Count)
        {
            inDialogue = false;
            currentDialogueIndex = 0;
        }
    }

    public bool HasMoreDialogues()
    {
        return currentDialogueIndex <= dialogues.Count;
    }

    public int CurrentDialogueIndex
    {
        get { return currentDialogueIndex; }
    }

    public int PlayerPositionX { get { return positionX; } }
    public int PlayerPositionY { get { return positionY; } }

    public bool IsNPCNearby(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height && mapTile[x][y] == '8';
    }
    public bool IsChestNearby(int x, int y)
    {
        bool check = x >= 0 && x < width && y >= 0 && y < height && mapTile[x][y] == '=';
        if(check)
        {
            mapTile[x][y] = ' ';
            return check; 
        }
        return check;
    }


    public bool InteractWithNPC()
    {
        int playerX = PlayerPositionX;
        int playerY = PlayerPositionY;

        if ((IsNPCNearby(playerX - 1, playerY) ||
             IsNPCNearby(playerX + 1, playerY) ||
             IsNPCNearby(playerX, playerY - 1) ||
             IsNPCNearby(playerX, playerY + 1)) && InDialogue == false)
        {
            InDialogue = true;
            currentDialogueIndex = 0;
            return true;
        }

        return false;
    }
    public bool InteractWithChest()
    {
        int playerX = PlayerPositionX;
        int playerY = PlayerPositionY;

        if ((IsChestNearby(playerX - 1, playerY) ||
             IsChestNearby(playerX + 1, playerY) ||
             IsChestNearby(playerX, playerY - 1) ||
             IsChestNearby(playerX, playerY + 1)))
        {

            return true;
        }

        return false;
    }

    public void GenerateBuilding()
    {
        Random random = new Random();

        int buildingRndNbr = random.Next(4, 10);
        int kindOfBuilding = 0;
        for (int o = 0; o < buildingRndNbr; o++)
        {
            kindOfBuilding = random.Next(1, 3);
            bool canBuild = false;
            int x = 0;
            int y = 0;
            while (!canBuild)
            {
                x = random.Next(1, width);
                y = random.Next(1, height);
                if (x + 9 >= width || y + 5 >= height || x + 6 >= width || y + 3 >= height)
                {
                    continue;
                }
                if (kindOfBuilding == 1)
                {
                    for (int j = x; j < x + 6; j++)
                    {
                        for (int k = y; k < y + 3; k++)
                        {
                            if (mapTile[j][k] == ' ' || mapTile[j][k] == '#')
                            {
                                canBuild = true;
                            }
                            else
                            {
                                canBuild = false;
                            }
                        }
                    }
                }
                else
                {
                    for (int j = x; j < x + 9; j++)
                    {
                        for (int k = y; k < y + 5; k++)
                        {
                            if (mapTile[j][k] == ' ' || mapTile[j][k] == '#')
                            {
                                canBuild = true;
                            }
                            else
                            {
                                canBuild = false;
                            }
                        }
                    }
                }
            }
            if (kindOfBuilding == 1)
            {
                DrawHouse1(x, y);
            }
            else
            {
                DrawHouse2(x, y);
            }
        }
    }

    public void GenerateTallGrass()
    {
        Random random = new Random();

        int grassNbr = random.Next(5, 10);
        for (int o = 0; o < grassNbr; o++)
        {
            bool canBuild = false;
            int x = 0;
            int y = 0;
            while (!canBuild)
            {
                x = random.Next(1, width);
                y = random.Next(1, height);
                if (x - 3 <= 1 || y + 4 >= height - 1 || x + 3 >= width - 1 || y <= 1)
                {
                    canBuild = false;
                }
                else
                {
                    canBuild = true;
                }
            }
            DrawRoundTallGrass(x, y);
        }
    }

    public void GenerateChest()
    {
        Random random = new Random();

        int chestNbr = random.Next(0, 3);
        for (int o = 0; o < chestNbr; o++)
        {
            bool canBuild = false;
            int x = 0;
            int y = 0;
            while (!canBuild)
            {
                x = random.Next(1, width);
                y = random.Next(1, height);
                if (x  <= 1 || y  >= height - 1 || x >= width - 1 || y <= 1 || mapTile[x][y] != ' ')
                {
                    canBuild = false;
                }
                else
                {
                    canBuild = true;
                }
            }
            mapTile[x][y] = '=';
        }
    }
    public void GenerateNPC() {         Random random = new Random();
    
           int npcNbr = random.Next(0, 3);
           for (int o = 0; o < npcNbr; o++)
        {
            bool canBuild = false;
            int x = 0;
            int y = 0;
            while (!canBuild)
            {
                x = random.Next(1, width);
                y = random.Next(1, height);
                if (x  <= 1 || y  >= height - 1 || x >= width - 1 || y <= 1 || mapTile[x][y] != ' ')
                {
                    canBuild = false;
                }
                else
                {
                    canBuild = true;
                }
            }
            DrawNPC(x, y);
        }
    }

    public void SwapZone()
    {

        if (positionX == 1)
        {
            Player.instance.positionX = width - 1;
            Player.instance.mapGlobalIndex[0] = Player.instance.mapIndexX - 1;
            Player.instance.mapIndexX--;

        }
        else if (positionX == width - 1)
        {
            Player.instance.positionX = 1;
            Player.instance.mapGlobalIndex[0] = Player.instance.mapIndexX + 1;
            Player.instance.mapIndexX++;

        }
        else if (positionY == 1)
        {
            Player.instance.positionY = height - 1;
            Player.instance.mapGlobalIndex[1] = Player.instance.mapIndexY - 1;
            Player.instance.mapIndexY--;

        }
        else if (positionY == height - 1)
        {
            Player.instance.positionY = 1;
            Player.instance.mapGlobalIndex[1] = Player.instance.mapIndexY + 1;
            Player.instance.mapIndexY++;

        }
        InitializeNextMap();
    }

    private static int GCD(int a, int b)
    {
        if (b == 0)
            return a;
        return GCD(b, a % b);
    }
    public static (int, int) NormalizePoint(int x, int y)
    {
        int gcd = GCD(Math.Abs(x), Math.Abs(y));
        int newX = 0, newY = 0;
        if (gcd != 0)
        {
            newX /= gcd;
            newY /= gcd;
        }
        return (newX, newY);
    }
}