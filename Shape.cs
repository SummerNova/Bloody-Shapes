using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloody_Shapes
{
    public abstract class Shape
    {
        public virtual float PositionX { get; protected set; }
        public virtual float PositionY { get; protected set;}
        public virtual float Width { get; protected set;}
        public virtual float Height { get; protected set;}
        public abstract string Name { get; protected set;}

        protected Shape() : this(0,0,1,1)
        {

        }

        protected Shape(float PosX, float PosY, float width, float height)
        {
            PositionX = PosX;
            PositionY = PosY;
            Width = width;
            Height = height;
        }

        public abstract float Area();
        public abstract float Peremiter();
        public virtual void Draw()
        {
            Console.WriteLine(Name);
        }

        
    }
    public class Circle : Shape
    {
        public override string Name { get; protected set; } = "Circle";


        public Circle() : this(0,0,1)
        { 
        
        }

        public Circle (float PosX, float PosY,float Radius) : base(PosX, PosY, Radius*2, Radius) // using Height as Radius and Width for Diameter
        {

        }

        public override float Area()
        {
            return Height*Height*(float)Math.PI;
        }

        public override float Peremiter()
        {
            return Width*(float)Math.PI;
        }
    }

    public class Rectangle : Shape
    {
        public override string Name { get; protected set; } = "Rectangle";

        public Rectangle() : this(0,0,1,1) { }

        public Rectangle(float PosX, float PosY, float width, float height) : base(PosX, PosY, width, height) { }

        public override float Area()
        {
            return Width * Height;
        }

        public override float Peremiter()
        {
            return (Width + Height) * 2;
        }
    }

    public class Triangle : Shape
    {
        public override string Name { get; protected set; } = "Triangle";

        public virtual float CornerOffset { get; protected set; }

        public Triangle() : this(0, 0, 1, 1, 0) { }

        public Triangle(float PosX, float PosY, float width, float height, float cornerOffset) : base(PosX, PosY, width, height) 
        { 
            CornerOffset = cornerOffset;
        }

        public override float Area()
        {
            return (Height * Width) / 2;
        }

        public override float Peremiter()
        {
            return Width + CalculateVerticies(true) + CalculateVerticies(false);
        }

        public float CalculateVerticies(bool DirectionRight)
        {
            float Offset = CornerOffset;
            if (DirectionRight) Offset += Width / 2;
            else Offset -= Width / 2;

            return (float)Math.Sqrt(Offset*Offset+Height*Height);
        }
    }
}
