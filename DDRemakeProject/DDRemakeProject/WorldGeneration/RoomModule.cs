﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using DDRemakeProject.World;
using Point = System.Windows.Point;

namespace DDRemakeProject.WorldGeneration
{
    public class RoomModule
    {

        public Rect RoomRect { get; set; }

        /// <summary>
        /// Matrix holding the Image(tiles) objects of the room
        /// </summary>
        public Dictionary<Vector, Tile> Tiles { get; set; }

        /// <summary>
        /// List of Tiles containing all the wall tiles in the Room
        /// </summary>
        public HashSet<Tile> WallTiles { get; set; }

        public Dictionary<Vector,RoomModule> Neighbors { get; set; }
        public List<Vector> AvailableAngles { get; set; }
        public HashSet<RoomModule> LinkedRoomsList { get; set; }


        public RoomModule()
        {

        }
        public RoomModule(Rect size)
        {
            this.RoomRect = size;
          
            Generate(false);
            AvailableAngles = new List<Vector>{new Vector(-180,180)};
            Neighbors = new Dictionary<Vector, RoomModule>();

           
        }

        public void Generate(bool loadingFromFile)
        {
           
            //if (!loadingFromFile)
            //{
            //    Tiles = new Dictionary<Vector, Tile>();
            //    WallTiles = new HashSet<Tile>();
            //}
            Tiles = new Dictionary<Vector, Tile>();
            WallTiles = new HashSet<Tile>();

            
            int area = (int) (RoomRect.Width * RoomRect.Height);
            for (int i = area  - 1; i >= 0; i--)
            {
                Vector tilePosition = new Vector(Math.Floor(i % RoomRect.Size.Width),Math.Floor((i / RoomRect.Size.Width))) +(Vector) RoomRect.Location;
                Tile.TypeEnum tileType;

                if (i < RoomRect.Size.Width || i > area - RoomRect.Size.Width || Math.Abs(i % RoomRect.Size.Width) < 0.1f ||
                    Math.Abs(i % RoomRect.Size.Width - (RoomRect.Size.Width- 1)) < 0.1f)
                {
                    tileType = Tile.TypeEnum.Wall;
                }
                else
                {
                    tileType = Tile.TypeEnum.Floor;
                }

                this.AddTile(tilePosition, tileType);   
            }
            

        }

        public void AddTile( Vector Position, Tile.TypeEnum type)
        {
            Tile t = new Tile(Position, this, type);
            //t.InitialiseRect();
            if (this.Tiles.ContainsKey(t.Position)) return;
            this.Tiles.Add(t.Position, t);

            if (t.Type == Tile.TypeEnum.Wall)
            {
                this.WallTiles.Add(t);
            }
        }

        public Vector CalculateAngle(RoomModule roomModule)
        {

            List<Vector> Positions = new List<Vector>
            {
                new Vector(roomModule.RoomRect.X,roomModule.RoomRect.Y),
                new Vector(roomModule.RoomRect.X+roomModule.RoomRect.Width,roomModule.RoomRect.Y),
                new Vector(roomModule.RoomRect.X,roomModule.RoomRect.Y+roomModule.RoomRect.Height),
                new Vector(roomModule.RoomRect.X+roomModule.RoomRect.Width,roomModule.RoomRect.Y+roomModule.RoomRect.Height)
            };

            List<double> Angles = new List<double>();
            Positions.ForEach(pos =>
            {
                double angle = Vector.AngleBetween((Vector) this.RoomRect.Location, pos);
                Angles.Add(angle);
            });


            return new Vector(Angles.Min(), Angles.Max());
        }

        public void CalculateRemainingAngles(Vector addedAngle)
        {
            List<Vector> AddAngles =new List<Vector>();


            AvailableAngles.ForEach(angle =>
            {
                if (addedAngle.X > angle.X && addedAngle.Y < angle.Y)
                {
                    AddAngles.Add(new Vector(addedAngle.Y, angle.Y));
                    AddAngles.Add(new Vector(angle.X,addedAngle.X));
                    //angle.Y = addedAngle.X;
                }
                else
                {
                    if (addedAngle.X > angle.X)
                    {
                        AddAngles.Add(new Vector(angle.X, addedAngle.X));
                        //angle.Y = addedAngle.X;
                    }
                    else
                    {
                        //angle.X = addedAngle.Y;
                        AddAngles.Add(new Vector(addedAngle.Y, angle.Y));
                    }
                }
            });

            AvailableAngles = AddAngles;
        }
    }

    public static class RoomExtensions
    {
        //public static void AddTile(this RoomModule r, Vector Position,Tile.TypeEnum type)
        //{
        //    Tile t = new Tile(Position,r,type);
        //    //t.InitialiseRect();
        //    if (r.Tiles.ContainsKey(t.Position)) return;
        //    r.Tiles.Add(t.Position, t);

        //    if (t.Type == Tile.TypeEnum.Wall)
        //    {
        //        r.WallTiles.Add(t);
        //    }
        //}
    }

}
 