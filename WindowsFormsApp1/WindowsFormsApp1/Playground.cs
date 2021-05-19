using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ShotLab
{
    public class PlayGround
    {
        public readonly MapCell[,] Laboratory;
        public readonly Point InitialPosition;
		public readonly Point Exit;
		public readonly Point[] Walls;
		public readonly Player Gamer;
		public readonly List<Box> Boxes;
		public readonly List<Killer> Killers;
		public readonly Point[] CheckPoints;
		

		public static PlayGround FromText(string text)
		{
			var lines = text.Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
			return FromLines(lines);
		}
        private PlayGround(Point initialPosition, MapCell[,] laboratory, 
			Point exit, Point[] walls, 
			Point[] boxesPos, Point[] killers,
			Point[] checkPoints)
        {
			InitialPosition = initialPosition;
			Laboratory = laboratory;
			Exit = exit;
			Walls = walls;
			Gamer = new Player(100, initialPosition);
			Boxes = CreateBoxes(boxesPos);
			Killers = CreateKillers(killers);
			CheckPoints = checkPoints;
        }

		public static PlayGround FromLines(string[] lines)
		{
			var dungeon = new MapCell[lines[0].Length, lines.Length];
			var initialPosition = Point.Empty;
			var exit = Point.Empty;
			var walls = new List<Point>();
			var checkPoints = new List<Point>();
			var box = new List<Point>();
			var killers = new List<Point>();
			for (var y = 0; y < lines.Length; y++)
				for (var x = 0; x < lines[0].Length; x++)
					switch (lines[y][x])
					{
						case '#':
							dungeon[x, y] = MapCell.Wall;
							walls.Add(new Point(x, y));
							break;
						case 'P':
							dungeon[x, y] = MapCell.Empty;
							initialPosition = new Point(x, y);
							break;
                        case 'C':
                            dungeon[x, y] = MapCell.Empty;
                            checkPoints.Add(new Point(x, y));
                            break;
						case 'B':
							dungeon[x, y] = MapCell.Empty;
							box.Add(new Point(x, y));
							break;
						case 'K':
							dungeon[x, y] = MapCell.Empty;
							killers.Add(new Point(x, y));
							break;
						case 'E':
							dungeon[x, y] = MapCell.Empty;
							exit = new Point(x, y);
							break;
						default:
							dungeon[x, y] = MapCell.Empty;
							break;
					}
			return new PlayGround(initialPosition, dungeon, exit, walls.ToArray(),
				box.ToArray(), killers.ToArray(), checkPoints.ToArray());
		}

		public bool InBounds(Point point)
		{
			var bounds = new Rectangle(0, 0, Laboratory.GetLength(0), Laboratory.GetLength(1));
			return bounds.Contains(point);
		}

		public bool PointIsEmpty(Point point) =>			
			Laboratory[point.X, point.Y] == MapCell.Empty;

		private List<Box> CreateBoxes(Point[] boxes) =>
			boxes.Select(point => new Box(100, point)).ToList();

		private List<Killer> CreateKillers(Point[] killers) =>
			killers.Select(point => new Killer(100, point)).ToList();

		public bool IsBox(Point point)
        {
			foreach(var box in Boxes)
            {
				if (box.Position == point)
					return true;
            }
			return false;
        }

		public bool IsKiller(Point point)
		{
			foreach (var killer in Killers)
			{
				if (killer.Position == point)
					return true;
			}
			return false;
		}
	}
}
