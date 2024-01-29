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

        public virtual float Radius { get { return Radius; } protected set { Radius = value; Height = value * 2;Width = value * 2; } }

        public sealed override float Height { get => base.Height; protected set => base.Height = value; }
        public sealed override float Width { get => base.Width; protected set => base.Width = value; }


        public Circle() : this(0,0,1)
        { 
        
        }

        public Circle (float PosX, float PosY,float radius) : base(PosX, PosY, radius*2, radius*2) 
        {
            Radius = radius;
        }

        public override float Area()
        {
            return Radius*Radius*(float)Math.PI;
        }

        public override float Peremiter()
        {
            return Width*(float)Math.PI;
        }

        public override string ToString()
        {
            return $"A Circle with a radius of {Height} Centered on ({PositionX},{PositionY})";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Circle otherCircle)
            {
                return (PositionX ==  otherCircle.PositionX && PositionY == otherCircle.PositionY && Height == otherCircle.Height);
            }
            else return false;
        }

        public bool Intersect(Circle otherCircle)
        {
            return MathF.Sqrt(MathF.Pow(PositionX - otherCircle.PositionX, 2) + MathF.Pow(PositionY - otherCircle.PositionY, 2)) <= Radius + otherCircle.Radius;
        }

        public bool Intersect(Rectangle otherRectangle)
        {
            
            if (MathF.Abs(PositionX - otherRectangle.PositionX)<= otherRectangle.Width)
            {
                return MathF.Abs(PositionY - otherRectangle.PositionY) <= otherRectangle.Height+Radius;
            }
            else if( MathF.Abs(PositionY - otherRectangle.PositionY) <= otherRectangle.Height)
            {
                return MathF.Abs(PositionX - otherRectangle.PositionX)<= otherRectangle.Width + Radius;
            }
            else 
            {
                float cornerX = MathF.Sign(PositionX - otherRectangle.PositionX) * (otherRectangle.Width / 2) + otherRectangle.PositionX;
                float cornerY = MathF.Sign(PositionY -otherRectangle.PositionY) * (otherRectangle.Height / 2) + otherRectangle.PositionY;
                return MathF.Sqrt(MathF.Pow(cornerX - PositionX, 2) + MathF.Pow(cornerY - PositionY, 2)) <= Radius;
            }
        }

        public override int GetHashCode()
        {
            return (PositionX.GetHashCode()<<4)+(PositionY.GetHashCode() << 8)+(Radius.GetHashCode() << 12);
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

        public override string ToString()
        {
            return $"A Rectangle with a Height of {Height} and a Width of {Width} Centered on ({PositionX},{PositionY})";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Rectangle otherRectangle)
            {
                return PositionX==otherRectangle.PositionX && PositionY ==otherRectangle.PositionY && Width == otherRectangle.Width && Height == otherRectangle.Height;
            }
            else return false;
        }

        public bool Intersect (Rectangle otherRectangle)
        {
            return MathF.Abs(PositionX - otherRectangle.PositionX) <= Width+otherRectangle.Width && MathF.Abs(PositionY - otherRectangle.PositionY) <= Height+otherRectangle.Height;
        }

        public bool Intersect (Circle otherCircle)
        {
            return otherCircle.Intersect(this);
        }
    }

    public class Triangle : Shape
    {
        public override string Name { get; protected set; } = "Triangle";

        public virtual float CornerOffset { get; protected set; }

        public Triangle() : this(0, 0, 1, 1, 0) { }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="PosX">The X of the pivot, which is located in the middle of the triangles base</param>
        /// <param name="PosY">The Y of the pivot, which is located in the middle of the triangles base</param>
        /// <param name="width"> The Length of the Base of the triangle</param>
        /// <param name="height"> The Height of the triangle</param>
        /// <param name="cornerOffset">offset of the X of the top corner inrelationship to the pivot</param>
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

            Offset = Math.Abs(Offset);

            return (float)Math.Sqrt(Offset*Offset+Height*Height);
        }

        public override string ToString()
        {
            return $"A Triangle with a Base centered on ({PositionX},{PositionY}), " +
                $"with corners located at ({PositionX - (Width/2)},{PositionY}), " +
                $"({PositionX+ (Width / 2)},{PositionY}) and ({PositionX+CornerOffset},{PositionY+Height})";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Triangle otherTriangle)
            {
                return PositionX == otherTriangle.PositionX && PositionY == otherTriangle.PositionY && Width == otherTriangle.Width && Height == otherTriangle.Height && CornerOffset == otherTriangle.CornerOffset;
            }
            else return false;
        }


    }


    // This form of inheritence has multiple problems. in fact it has at least 2 of them. 
    // the first one, is the fact that it is rather meaningless. it doesn't add any additional meaningful functionality. 
    // the second one, and the more glaring one, is the fact that a Square technically speaking also inherits the properties of a rhombus,
    // but due to the limit of C#'s ingeritense, we can inherit only one shape type. therefore, this square won't have any dynamic implementations
    // that would be implemented for a rhombus. a more appropriate form of implementation would be interfaces.
    public class Square : Rectangle 
    {
        public sealed override float Width { get => base.Width; protected set { base.Width = value; base.Height = value; } }
        public sealed override float Height { get => base.Height; protected set { base.Height = value; base.Width = value; } }
        public Square() :this(0,0,1) { }

        public Square(float PosX, float PosY, float Width) :base(PosX,PosY,Width,Width) { }
    }

}
