
using System;
using System.Collections.Generic;

namespace NoteLi;
public struct Vec2i
{
  public int x;
  public int y;

  public Vec2i(int _x = 0, int _y = 0)
  {
    this.x = _x;
    this.y = _y;
  }

  public Vec2i(Vec2i other)
  {
    this.x = other.x;
    this.y = other.y;
  }

  public static Vec2i operator +(Vec2i a, Vec2i b)
  {
    return new Vec2i(a.x + b.x, a.y + b.y);
  }

  public static Vec2i operator -(Vec2i a, Vec2i b)
  {
    return new Vec2i(a.x - b.x, a.y - b.y);
  }

  public static Vec2i operator -(Vec2i a, int b)
  {
    return new Vec2i(a.x - b, a.y - b);
  }

  public static Vec2i operator -(Vec2i a)
  {
    return new Vec2i(-a.x, -a.y);
  }


  public static Vec2i operator *(Vec2i a, int b)
  {
    return new Vec2i(a.x * b, a.y * b);
  }

  public static Vec2i operator /(Vec2i a, int b)
  {
    return new Vec2i(a.x / b, a.y / b);
  }

  public static Vec2i operator *(Vec2i a, Vec2i b)
  {
    return new Vec2i(a.x * b.x, a.y * b.y);
  }
  public static bool operator ==(Vec2i a, Vec2i b)
  {
    return a.x == b.x && a.y == b.y;
  }

  public static bool operator !=(Vec2i a, Vec2i b)
  {
    return !(a == b);
  }

  public static bool operator ==(Vec2i a, int b)
  {
    return a.x == b && a.y == b;
  }

  public static bool operator !=(Vec2i a, int b)
  {
    return !(a == b);
  }

  public static bool operator <(Vec2i a, Vec2i b)
  {
    return Math.Abs(a.x) < Math.Abs(b.x) && Math.Abs(a.y) < Math.Abs(b.y);
  }

  public static bool operator >(Vec2i a, Vec2i b)
  {
    return Math.Abs(a.x) > Math.Abs(b.x) && Math.Abs(a.y) > Math.Abs(b.y);
  }

  public static bool operator <(Vec2i a, int b)
  {
    return a.x < b && a.y < b;
  }

  public static bool operator >(Vec2i a, int b)
  {
    return Math.Abs(a.x) > b && Math.Abs(a.y) > b;
  }

  public static bool operator <=(Vec2i a, Vec2i b)
  {
    return a.x <= b.x && a.y <= b.y;
  }

  public static bool operator >=(Vec2i a, Vec2i b)
  {
    return a.x >= b.x && a.y >= b.x;
  }

  public static Vec2i operator <<(Vec2i a, int b)
  {
    return new Vec2i(a.x << b, a.y << b);
  }

  public static Vec2i operator >>(Vec2i a, int b)
  {
    return new Vec2i(a.x >> b, a.y >> b);
  }

  public static Vec2i operator &(Vec2i a, int b)
  {
    return new Vec2i(a.x & b, a.y & b);
  }


  public override string ToString()
  {
    return $"[{x},{y}]";
  }

  public float dot(Vec2i other)
  {
    var dot_result = x * other.x + y * other.y;
    return dot_result;
  }

  public double Magnitude()
  {
    return Math.Sqrt(x * x + y * y);
  }

  public bool IsInBoundsOf(Vec2i _size)
  {
    return this.x <= _size.x && this.y <= _size.y && this.x >= 0 && this.y >= 0;
  }

  public static readonly Vec2i[] CardinalDirections = [Vec2i.DOWN, Vec2i.UP, Vec2i.LEFT, Vec2i.RIGHT];
  public static Vec2i RandomCardinalDirection
  {
    get { return CardinalDirections[new Random().Next(0, CardinalDirections.Length)]; }
  }

  public Vec2i Normalized
  {
    get
    {
      double magnitude = this.Magnitude();
      if (magnitude == 0) // Avoid division by zero
        return Vec2i.ZERO;

      // Normalize using floating-point arithmetic
      double normalizedX = x / magnitude;
      double normalizedY = y / magnitude;

      // Convert back to integer-based coordinates by rounding
      int roundedX = (int)Math.Round(normalizedX);
      int roundedY = (int)Math.Round(normalizedY);

      return new Vec2i(roundedX, roundedY);
    }
  }
  public Vec2i StepToZero(int _stepSize = 1)
  {
    var result = new Vec2i(this);
    if (this.x != 0)
    {
      result.x += (this.x > 0) ? -_stepSize : _stepSize;
    }

    if (this.y != 0)
    {
      result.y += (this.y > 0) ? -_stepSize : _stepSize;
    }
    return result;
  }

  public Vec2i StepToPosition(Vec2i _desiredPos, int _stepSize = 1)
  {
    var result = new Vec2i(this);

    int stepX = Math.Min(_stepSize, Math.Abs(this.x - _desiredPos.x));
    result.x += (this.x > _desiredPos.x) ? -stepX : stepX;

    int stepY = Math.Min(_stepSize, Math.Abs(this.y - _desiredPos.y));
    result.y += (this.y > _desiredPos.y) ? -stepY : stepY;

    return result;
  }

  public Vec2i DirectionTo(Vec2i other)
  {
    return new Vec2i(other.x - this.x, other.y - this.y);
  }


  public static readonly Vec2i ZERO = new Vec2i(0, 0);
  public static readonly Vec2i ONE = new Vec2i(1, 1);

  public static readonly Vec2i UP = new Vec2i(0, -1);
  public static readonly Vec2i DOWN = new Vec2i(0, 1);
  public static readonly Vec2i LEFT = new Vec2i(-1, 0);
  public static readonly Vec2i RIGHT = new Vec2i(1, 0);

  public static Vec2i Random(int _maxExclusive)
  {
    var rng = new Random();
    return new Vec2i(rng.Next(_maxExclusive), rng.Next(_maxExclusive));
  }

  public static Vec2i Random(Vec2i _range, bool _randomSign = false)
  {
    var rng = new Random();
    var x = rng.Next(_range.x);
    var y = rng.Next(_range.y);
    if (_randomSign)
    {
      var flipSigns = new Random().Next(100) < 50 ? true : false;
      if (flipSigns)
        x = -x;
      flipSigns = new Random().Next(100) < 50 ? true : false;
      if (flipSigns)
        y = -y;
    }
    return new Vec2i(x, y);
  }

  public static List<Vec2i> GetLine(Vec2i start, Vec2i end)
  {
    var line = new List<Vec2i>();

    int x1 = start.x;
    int y1 = start.y;
    int x2 = end.x;
    int y2 = end.y;

    // Calculate the differences and signs
    int dx = Math.Abs(x2 - x1);
    int dy = Math.Abs(y2 - y1);
    int sx = x1 < x2 ? 1 : -1;
    int sy = y1 < y2 ? 1 : -1;

    // Bresenham's algorithm error terms
    int err = dx - dy;

    // While we have not reached the end point
    while (true)
    {
      // Add the current point to the line
      line.Add(new Vec2i(x1, y1));

      // If we've reached the end point, break
      if (x1 == x2 && y1 == y2)
        break;

      // Calculate the error term for the next point
      int e2 = 2 * err;

      if (e2 > -dy)
      {
        err -= dy;
        x1 += sx; // Move horizontally
      }
      if (e2 < dx)
      {
        err += dx;
        y1 += sy; // Move vertically
      }
    }

    return line;
  }

  public override bool Equals(object? obj)
  {
    if (obj is Vec2i other)
      return this.x == other.x && this.y == other.y;

    return false;
  }

  public override int GetHashCode()
  {
    return HashCode.Combine(x, y);
  }

  public static Vec2i FromCKI(ConsoleKeyInfo dir)
  {
    switch (dir.Key)
    {
      case ConsoleKey.UpArrow:
        return Vec2i.UP;
      case ConsoleKey.DownArrow:
        return Vec2i.DOWN;
      case ConsoleKey.LeftArrow:
        return Vec2i.LEFT;
      case ConsoleKey.RightArrow:
        return Vec2i.RIGHT;
      case ConsoleKey.W:
        return Vec2i.UP;
      case ConsoleKey.S:
        return Vec2i.DOWN;
      case ConsoleKey.A:
        return Vec2i.LEFT;
      case ConsoleKey.D:
        return Vec2i.RIGHT;
      default:
        return Vec2i.ZERO;
    }
  }

  public static Vec2i New(int x, int y)
  {
    return new Vec2i(x, y);
  }



}