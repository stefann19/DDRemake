using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DDRemakeProject.GamePlay.Old;

namespace DDRemakeProject.World
{
    public class Camera
    {
        public Camera(Engine engine)
        {
            Canvas = engine.MapWindow.Canvas;
            Engine = engine;
            InitiateCamera();
            //CheatsActivated = true;
        }

        public Canvas Canvas { get; set; }
        public double Scale { get; set; }
        public Engine Engine { get; set; }

        private void InitiateCamera()
        {
            _lastMousePos = new Point(Engine.MapWindow.Width / 2f, Engine.MapWindow.Height / 2f);
            Scale = 1;
            _cheatsActivated = true;
            CheatsActivated = false;
        }
        #region Cheats
        

        public bool CheatsActivated
        {
            get => _cheatsActivated;
            set
            {
                if (!_cheatsActivated && value)
                {
                    Canvas.MouseWheel += ZoomOnCanvas;
                    Canvas.MouseMove += DragCanvas;
                }
                else if (_cheatsActivated && !value)
                {
                    Canvas.MouseWheel -= ZoomOnCanvas;
                    Canvas.MouseMove -= DragCanvas;
                    //Scale = Scale;
                    
                    Follow(Engine.Player.CurrentTile.Position - (Vector)_lastMousePos/Constants.TilePx);
                    Zoom();
                }
                _cheatsActivated = value;
            }
        }

        private double _scale;
        private void ZoomOnCanvas(object sender, MouseWheelEventArgs e)
        {
            UIElement element = Canvas;

            Point position = e.GetPosition(element);
            MatrixTransform transform = element.RenderTransform as MatrixTransform;
            Matrix matrix = transform.Matrix;
            _scale = e.Delta >= 0 ? 1.1 : 1.0 / 1.1; // choose appropriate scaling factor
            Scale *= _scale;
            matrix.ScaleAtPrepend(_scale, _scale, position.X, position.Y);

            element.RenderTransform = new MatrixTransform(matrix);
        }

        private bool _c1;
        private Point _lastMousePos;
        private bool _cheatsActivated;

        private void DragCanvas(object sender, MouseEventArgs e)
        {
            UIElement element = Canvas;

            Point position = e.GetPosition(element);
            MatrixTransform transform = element.RenderTransform as MatrixTransform;
            Matrix matrix = transform.Matrix;

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!_c1)
                {
                    if (position != _lastMousePos)
                    {
                        matrix.TranslatePrepend(+position.X - _lastMousePos.X, position.Y - _lastMousePos.Y);
                        element.RenderTransform = new MatrixTransform(matrix);
                    }
                }
                else
                {
                    _c1 = false;
                }
            }
            else if (e.LeftButton == MouseButtonState.Released)
            {
                _lastMousePos = position;
                _c1 = true;
            }
        }

        #endregion

        #region CameraFollow

        public void CameraFollow(Vector movementVector)
        {
            //Zoom();
            Follow(movementVector);
        }

        public void Zoom()
        {
            //Scale = 1f / Scale;

            UIElement element = Canvas;

            //var position = e.GetPosition(element);
            Vector position = Engine.Player.Tile.Position * Constants.TilePx;

            MatrixTransform transform = element.RenderTransform as MatrixTransform;
            Matrix matrix = transform.Matrix;
            // Scale = e.Delta >= 0 ? 1.1 : (1.0 / 1.1); // choose appropriate scaling factor
            matrix.ScaleAtPrepend(1f/Scale,1f/Scale,position.X,position.Y);
            double minScale = Math.Max(6, Engine.Player.CurrentMultiTileShape.Rect.MinWH());
            Scale =  ((1.75f * Constants.TilePx) / (minScale * 10 )) ; // choose appropriate scaling factor
            
            //double scaleX =

            matrix.ScaleAtPrepend(Scale, Scale, position.X, position.Y);
           // matrix.sc
            element.RenderTransform = new MatrixTransform(matrix);
        }

        public void Follow(Vector movementVector)
        {
            UIElement element = Canvas;

            Point position = (Point)(movementVector * Constants.TilePx);
            MatrixTransform transform = element.RenderTransform as MatrixTransform;
            Matrix matrix = transform.Matrix;


            matrix.TranslatePrepend(-position.X, -position.Y);
            element.RenderTransform = new MatrixTransform(matrix);
        }

        #endregion
    }
}