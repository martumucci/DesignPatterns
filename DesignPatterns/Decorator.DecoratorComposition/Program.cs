using System;
using static System.Console;

namespace Decorator.DecoratorComposition
{
    //  Dynamic composition

    public interface IShape
    {
        public string AsString();
    }

    public class Circle : IShape
    {
        private float radius;
        public Circle(float radius)
        {
            this.radius = radius;
        }

        public string AsString()
        {
            return $"A circle with radius {radius}";
        }

        public void Resize(float factor)
        {
            radius *= factor;
        }
    }

    public class Square : IShape
    {
        private float side;
        public Square(float side)
        {
            this.side = side;
        }
        public string AsString() => $"A sqare with side {side}";
    }

    public class ColoredShape : IShape
    {
        private IShape shape;
        private string color;

        public ColoredShape(IShape shape, string color)
        {
            this.shape = shape ?? throw new ArgumentNullException(paramName: nameof(shape));
            this.color = color ?? throw new ArgumentNullException(paramName: nameof(color));
        }
        public string AsString()
        {
            return $"{shape.AsString()} has the color {color} ";
        }
    }

    public class TransparentShape : IShape
    {
        private IShape shape;
        private float transparency;
        public TransparentShape(IShape shape, float transparency)
        {
            this.shape = shape ?? throw new ArgumentNullException(paramName: nameof(shape));
            this.transparency = transparency;
        }
        public string AsString() => $"{shape.AsString()} has {transparency * 100.0}% transparency";
    }

    class Program
    {
        static void Main(string[] args)
        {
            var square = new Square(1.23f);
            WriteLine(square.AsString());

            var redSquare = new ColoredShape(square, "red");
            WriteLine(redSquare.AsString());

            var transparentSquare = new TransparentShape(redSquare, 0.5f);
            WriteLine(transparentSquare.AsString());
        }
    }
}
