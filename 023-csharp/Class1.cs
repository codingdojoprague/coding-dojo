using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NUnit.Core;
using NUnit.Framework;

namespace PacmanDojo
{
    /*
    * pacman is on a grid filled with dots
    * pacman has a direction
    * pacman moves on each tick
    * user can rotate pacman
    * pacman eats dots
    * pacman wraps around 
    * pacman stops on wall
    * pacman will not rotate into a wall
    * game score (levels completed, number of dots eaten in this level)
    * monsters...
    * levels
    * animate pacman eating (mouth opens and closes)
    Challenge:
       No of lines per function: 4
       NoLoops, NoIf, NoSwitch
       Immutables
    */
    //var game = new Game(plan);
    //game.Place(pacman);
    //game.Place(monster1);
    //game.Place(monster2);
    //game.Place(monster3);
    //game.Start();
    //game.Tick();
    //game.Pacman.SetDirection(direction);
    //game.Tick();


    [TestFixture]
    public class Class1
    {
        [Test]
        public void CanSetDirection()
        {
            var pacman = new Pacman(new Coordinates(0, 0), Direction.North);
            var pacman2 = pacman.SetDirection(Direction.South);
            Assert.That(pacman2.Direction, Is.EqualTo(Direction.South));
        }

        [TestCase(0, 0, Direction.North, 0, 1)]
        [TestCase(0, 0, Direction.East, 1, 0)]
        [TestCase(10, 10, Direction.South, 10, 9)]
        [TestCase(10, 10, Direction.West, 9, 10)]
        public void MovesOnTick(int actualX, int actualY, Direction direction, int expectedX, int expectedY)
        {
            var actualCoordinates = new Coordinates(actualX, actualY);

            var pacman = new Pacman(actualCoordinates, direction);
            var plan = new Plan();
            var game = new Game(plan).Place(pacman).Tick();
            Assert.That(game.Pacman.Coordinates.X, Is.EqualTo(expectedX));
            Assert.That(game.Pacman.Coordinates.Y, Is.EqualTo(expectedY));
        }

        [TestCase(0, 0, Direction.West, 0, 0)]
        [TestCase(0, 0, Direction.South, 0, 0)]
        [TestCase(Plan.Width, Plan.Height, Direction.North, Plan.Width, Plan.Height)]
        public void StopByBorderWall(int actualX, int actualY, Direction direction, int expectedX, int expectedY)
        {
            var pacman = new Pacman(new Coordinates(actualX, actualY), direction);
            var plan = new Plan();
            var game = new Game(plan).Place(pacman).Tick();
            Assert.That(game.Pacman.Coordinates, Is.EqualTo(new Coordinates(expectedX, expectedY)));
        }

        public void CanPlaceMonster()
        {
            var monster = new Monster(new Coordinates(3, 3));
            var game = new Game(new Plan()).Place(monster);
            Assert.That(game.Monsters[0], Is.EqualTo(monster));
        }
    }

    public class Game
    {
        private readonly Plan plan;
        private readonly List<Monster> monsters;

        public Game(Plan plan)
        {
            this.plan = plan;
            this.monsters = new List<Monster>();
        }

        public List<Monster> Monsters
        {
            get { return monsters; }
        }

        public Pacman Pacman { get; private set; }

        public Game Place(Pacman pacman)
        {
            return new Game(plan) { Pacman = pacman };
        }

        public Game Place(Monster monster)
        {
            var game = new Game(plan);
            game.monsters.Add(monster);
            return game;
        }

        public Game Tick()
        {
            return Place(CanMove(Pacman) ? Pacman.Move() : Pacman);
        }

        private bool CanMove(Pacman pacman)
        {
            var fakePacman = pacman.Move();
            return (
                (fakePacman.Coordinates.X >= 0 && fakePacman.Coordinates.Y >= 0)
                &&
                (fakePacman.Coordinates.X <= Plan.Width && fakePacman.Coordinates.Y <= Plan.Height)
             );
        }
    }

    public class Plan
    {
        public const int Width = 30;
        public const int Height = 30;
    }

    public enum Direction
    {
        North,
        East,
        South,
        West
    }

    public abstract class GameActor
    {
        private readonly Coordinates coordinates;
        private readonly Direction direction;
        private readonly Dictionary<Direction, Coordinates> deltas;

        protected GameActor(Coordinates coordinates, Direction direction)
        {
            this.coordinates = coordinates;
            this.direction = direction;

            deltas = new Dictionary<Direction, Coordinates>
            {
                {Direction.North, new Coordinates(0, 1)},
                {Direction.West, new Coordinates(-1, 0)},
                {Direction.East, new Coordinates(1, 0)},
                {Direction.South, new Coordinates(0, -1)}
            };
        }

        public Coordinates Coordinates
        {
            get { return coordinates; }
        }

        public Direction Direction
        {
            get { return direction; }
        }


        public Pacman SetDirection(Direction direction)
        {
            return new Pacman(coordinates, direction);
        }


        public Pacman Move()
        {
            var newCoordinate = this.coordinates.Add(deltas[direction]);
            return new Pacman(newCoordinate, Direction);
        }
    }

    public class Pacman : GameActor
    {
        public Pacman(Coordinates coordinates, Direction direction)
            : base(coordinates, direction)
        { }
    }

    public class Monster : GameActor
    {
        public Monster(Coordinates coordinates)
            : base(coordinates, Direction.South)
        { }
    }

    public class Coordinates
    {
        private readonly int x;
        private readonly int y;

        public Coordinates(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public Coordinates Add(Coordinates coordinates)
        {
            return new Coordinates(x + coordinates.x, y + coordinates.y);
        }

        protected bool Equals(Coordinates other)
        {
            return x == other.x && y == other.y;
        }

        public override bool Equals(object obj)
        {
            return Equals((Coordinates)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (x * 397) ^ y;
            }
        }
    }
}
