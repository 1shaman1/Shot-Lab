using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;

namespace ShotLab
{
	/// <summary>
	/// ИИ противника
	/// </summary>
	public class II
    {
		System.Media.SoundPlayer snd = new System.Media.SoundPlayer(Properties.Resources.hit);
		private readonly static List<Size> PossibleDirections = new List<Size>()
		{
			new Size(1, 0),
			new Size(-1, 0),
			new Size(0, 1),
			new Size(0, -1)
		};
		public void MoveKiller(Killer killer, PlayGround playGround)
        {
			var player = playGround.Gamer;
			if (PlayerIsNear(killer, player, out Size rotate))
			{
				killer.CurrentWeapon.Rotate(rotate);
				snd.Play();
				killer.CurrentWeapon.Shoot(killer, playGround);
				return;
			}			
			var path = CreatePath(playGround, killer.Position, playGround.Gamer.Position);
			if (path.Count == 0)
				return;
			var newPosition = path[0];
			killer.CurrentWeapon.Rotate(new Size(newPosition.X - killer.Position.X, newPosition.Y - killer.Position.Y));
			killer.Position = newPosition;				
        }

		public static IEnumerable<SinglyLinkedList<Point>> FindPaths(PlayGround playGround, Point start, Point target)
		{
			var pointsQueue = new Queue<SinglyLinkedList<Point>>();
			var startPoint = new SinglyLinkedList<Point>(start);
			pointsQueue.Enqueue(startPoint);
			var visitedPoints = new HashSet<Point>();
			while (pointsQueue.Count != 0)
			{
				var point = pointsQueue.Dequeue();
				if (!CanMove(point, playGround, visitedPoints, start))
					continue;
				visitedPoints.Add(point.Value);
				if (target == point.Value)
					yield return point;
				foreach (var move in PossibleDirections)
					
					pointsQueue.Enqueue(CreatePoint(move.Width, move.Height, point));
			}
		}

		private static bool CanMove(SinglyLinkedList<Point> point, PlayGround playGround, HashSet<Point> visited, Point start) =>
			playGround.InBounds(point.Value) &&
			playGround.PointIsEmpty(point.Value) &&
			!visited.Contains(point.Value) &&
			!playGround.IsBox(point.Value);
		private static SinglyLinkedList<Point> CreatePoint(int dx, int dy, SinglyLinkedList<Point> previousePoint)
		{
			var x = previousePoint.Value.X;
			var y = previousePoint.Value.Y;
			return new SinglyLinkedList<Point>(new Point { X = x + dx, Y = y + dy }, previousePoint);
		}

		private static SinglyLinkedList<Point> TakeMinPath(PlayGround playGround, Point start, Point target)
		{
			var waysToTarget = FindPaths(playGround, start, target);
			return waysToTarget.Where(way => way.Length == waysToTarget.Min(minWay => minWay.Length))
				.FirstOrDefault();
		}

		private static List<Point> CreatePath(PlayGround playGround, Point start, Point target)
        {
			var result = new List<Point>();
			var path = TakeMinPath(playGround, start, target);
			while(path != null && path.Previous != null)
            {
				result.Add(path.Value);
				path = path.Previous;
            }
			result.Reverse();
			result.Remove(target);
			return result;
        }

		private bool PlayerIsNear(Killer killer, Player player, out Size rotate)
        {
			rotate = new Size(player.Position.X - killer.Position.X, player.Position.Y - killer.Position.Y);
			return PossibleDirections.Contains(rotate);
        }
	}
}
