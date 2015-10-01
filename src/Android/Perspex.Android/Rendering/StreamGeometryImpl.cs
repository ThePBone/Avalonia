using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Perspex.Media;
using Perspex.Platform;
using APath = Android.Graphics.Path;
using ARect = Android.Graphics.RectF;

namespace Perspex.Android.Rendering
{
    public class StreamGeometryImpl : IStreamGeometryImpl
    {
        public StreamGeometryImpl()
        {
            _impl = new StreamGeometryContextImpl(null);
        }

        public StreamGeometryImpl(StreamGeometryContextImpl impl)
        {
            _impl = impl;
        }

        private readonly StreamGeometryContextImpl _impl;

        public APath Path
        {
            get { return _impl.Path; }
        }

        public Rect Bounds
        {
            get { return _impl.Bounds; }
        }
        private Matrix _transform = Matrix.Identity;
        public Matrix Transform
        {
            get { return _transform; }
            set
            {
                if (value != Transform)
                {
                    if (!value.IsIdentity)
                    {
                        _transform = value;
                    }
                }
            }
        }
        public Rect GetRenderBounds(double strokeThickness)
        {
            throw new NotImplementedException();
        }

        public IStreamGeometryImpl Clone()
        {
            return new StreamGeometryImpl(_impl);
        }

        public IStreamGeometryContextImpl Open()
        {
            return _impl;
        }
    }

    public class StreamGeometryContextImpl : IStreamGeometryContextImpl
    {
        public StreamGeometryContextImpl(APath path = null)
        {
            this.Path = path != null ? new APath(path) : new APath();
        }

        public APath Path { get; private set; }
        private ARect _bounds;

        public Rect Bounds
        {
            get
            {
                Path.ComputeBounds(_bounds, true);
                return _bounds.ToPerspex();
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void ArcTo(Point point, Size size, double rotationAngle, bool isLargeArc, SweepDirection sweepDirection)
        {
            throw new NotImplementedException();
            //TODO: Needs a rect?
            //this.Path.ArcTo();
        }

        public void BeginFigure(Point startPoint, bool isFilled)
        {
            this.Path.MoveTo((float)startPoint.X, (float)startPoint.Y);
        }

        public void BezierTo(Point point1, Point point2, Point point3)
        {
            this.Path.CubicTo((float)point1.X, (float)point1.Y,
                (float)point2.X, (float)point2.Y,
                (float)point3.X, (float)point3.Y);
        }

        public void LineTo(Point point)
        {
            this.Path.LineTo((float)point.X, (float)point.Y);
        }

        public void EndFigure(bool isClosed)
        {
            if (this.Path != null)
                if (isClosed)
                    this.Path.Close();
        }
    }
}