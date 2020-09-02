using Autofac;
using System;
using static System.Console;

namespace Bridge.Bridge
{
    // It is a mechanisme that decouples an interface from an implementation so that the two can vary independently.

    public interface IRenderer
    {
        void RenderCircle(float radius);
    }

    public class VectorRender : IRenderer
    {
        public void RenderCircle(float radius)
        {
            WriteLine($"Drawing a circle of radius {radius}");
        }
    }

    public class RasterRender : IRenderer
    {
        public void RenderCircle(float radius)
        {
            WriteLine($"Drawing pixels for circle with radius {radius}");
        }
    }

    public abstract class Shape
    {
        protected IRenderer renderer;
        public Shape(IRenderer renderer)
        {
            this.renderer = renderer ?? throw new ArgumentNullException(paramName: nameof(renderer));
        }

        public abstract void Draw();
        public abstract void Resize(float factor);
    }

    public class Circle : Shape
    {
        private float radius;
        public Circle(IRenderer renderer, float radius) : base(renderer)
        {
            this.radius = radius;
        }
        public override void Draw()
        {
            renderer.RenderCircle(radius);
        }

        public override void Resize(float factor)
        {
            radius *= factor;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //IRenderer renderer = new RasterRender();
            var renderer = new RasterRender();
            var circle = new Circle(renderer, 5);
            circle.Draw();
            circle.Resize(2);
            circle.Draw();

            //var cb = new ContainerBuilder();
            //cb.RegisterType<VectorRender>().As<IRenderer>().SingleInstance(); //Singleton
            //cb.Register((c, p) => new Circle(c.Resolve<IRenderer>(), p.Positional<float>(0)));
            //using (var c = cb.Build())
            //{
            //    var circle = c.Resolve<Circle>(new PositionalParameter(0, 5.0f));
            //    circle.Draw();
            //    circle.Resize(2.0f);
            //    circle.Draw();
            //}

        }
    }
}
