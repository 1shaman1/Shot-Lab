using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ShotLab
{
    public class PlayGround
    {
        public readonly MapCell[,] Laboratory;
        public readonly Point InitialPosition;
		public readonly Point Exit;
		public readonly Point[] Walls;
        //public readonly Props[] Boxes;

		public static PlayGround FromText(string text)
		{
			var lines = text.Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
			return FromLines(lines);
		}
        private PlayGround(Point initialPosition, MapCell[,] laboratory, Point exit, Point[] walls)
        {
			InitialPosition = initialPosition;
			Laboratory = laboratory;
			Exit = exit;
			Walls = walls;
        }

		public static PlayGround FromLines(string[] lines)
		{
			var dungeon = new MapCell[lines[0].Length, lines.Length];
			var initialPosition = Point.Empty;
			var exit = Point.Empty;
			var walls = new List<Point>();
			//var chests = new List<Point>();
			for (var y = 0; y < lines.Length; y++)
			{
				for (var x = 0; x < lines[0].Length; x++)
				{
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
						//case 'C':
						//	dungeon[x, y] = MapCell.Empty;
						//	chests.Add(new Point(x, y));
						//	break;
						case 'E':
							dungeon[x, y] = MapCell.Empty;
							exit = new Point(x, y);
							break;
						default:
							dungeon[x, y] = MapCell.Empty;
							break;
					}
				}
			}
			return new PlayGround(initialPosition, dungeon, exit, walls.ToArray());
		}

		public bool InBounds(Point point)
		{
			var bounds = new Rectangle(0, 0, Laboratory.GetLength(0), Laboratory.GetLength(1));
			return bounds.Contains(point);
		}		
	}
}
