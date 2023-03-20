
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

namespace GameJom
{
    class testing
    { 
}
    class LevelEditor : LevelClass
    {
        public AutomatedDraw DrawParam;
        GridTexture GridManager;
        Point rechtangleSelectionStart;
        bool previouseMousePressedState;
        int brushState = 1;
        enum AvaliableBrushes
        {
            room = 0,
            tile = 1,

        }
        public Rectangle RectangleSelection(Point mousePos, bool pressedState)
        {
            if(pressedState == true )
            {
                if (previouseMousePressedState == false)
                {
                    rechtangleSelectionStart = DrawParam.CalcPoint(mousePos);
                }
            }
            return GridManager.GridRectangle(new Rectangle(rechtangleSelectionStart, DrawParam.CalcPoint(mousePos) - rechtangleSelectionStart));
        }
        #region (back)rooms
        public void AddRoomCheck()
        {
                Rectangle roomSelection = new Rectangle();
                //
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    DrawParam.Draw(GridManager.GridToScreen(RectangleSelection(Mouse.GetState().Position, Mouse.GetState().LeftButton == ButtonState.Pressed)), Game1.BasicTexture);
                if (!(Mouse.GetState().LeftButton == ButtonState.Pressed) && previouseMousePressedState == true)
                {
                    roomSelection = RectangleSelection(Mouse.GetState().Position, Mouse.GetState().LeftButton == ButtonState.Pressed);
                    foreach (Room n in Rooms)
                    {
                        if(n.RoomSize.Intersects(roomSelection))
                        {
                            return;
                        }
                    }
                    Rooms.Add(new Room(roomSelection, this));
                
            }
            // TODO: takes a set of cordnet and place a new room at that location(might be good to allow rooms to overlap to have mid room transitions
        }
        void RemoveRoom()
        {
            int n = 0;
            if (SelectRoom(Mouse.GetState().Position, ref n) & Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                Rooms.RemoveAt(n);
            }
        }
        #endregion
        #region tiles
        void drawtile(int paint)
        {

        }
        #endregion
        bool SelectRoom(Point selectionLocation, ref int room)
        {
            for (int n = 0; n < Rooms.Count; n++)
            {
                if (DrawParam.DisplayRectangle(GridManager.GridToScreen(Rooms[n].RoomSize)).Intersects(new Rectangle(selectionLocation, new Point(0, 0))))
                {
                    room = n;
                    return true;
                }
            }
            return false;
        }
        bool MouseInBounds(Rectangle selectionSpace)
        {
            if (selectionSpace.Intersects(new Rectangle(Mouse.GetState().Position, new Point(0,0))))
            {
                return true;
            }
            return false;
        }
        public void Edit(Rectangle Grid, AutomatedDraw drawParam)
        {
            this.DrawParam = drawParam;
            GridManager = new GridTexture(drawParam, Grid);
            GridManager.Griddify(Game1.Griddy);
            AutomatedDraw Base = new AutomatedDraw(); Menu menu = new Menu(Base, new PrintManager(10, Color.White, new Point(40, 80)), new string[] { "room", "tiles" }, new Point(20, 20), 15, true);
                //tiles
            if (brushState == (int)AvaliableBrushes.tile)
            {
                int n = 0;
                if(Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    if (SelectRoom(Mouse.GetState().Position, ref n))
                    {
                        // edit not triggering, find out why
                        Rooms[n].Edit(GridManager.GridPoint(drawParam.CalcPoint(Mouse.GetState().Position)) - Rooms[n].RoomSize.Location);
                    }
                }
                if (Mouse.GetState().RightButton == ButtonState.Pressed)
                {
                    if (SelectRoom(Mouse.GetState().Position, ref n))
                    {
                        // edit not triggering, find out why
                        Rooms[n].Edit(GridManager.GridPoint(drawParam.CalcPoint(Mouse.GetState().Position)) - Rooms[n].RoomSize.Location, 0, 0);
                    }
                }

                foreach (Room room in Rooms)
                {
                    room.Draw(drawParam, GridManager, Game1.BasicTexture);
                }
            }

            //room brush
            if (brushState == (int)AvaliableBrushes.room)
            {
                if (MouseInBounds(new Rectangle(300, 0, 4000, 4000)))
                {
                    AddRoomCheck();
                    RemoveRoom();
                }
                foreach (Room room in Rooms)
                {
                    drawParam.Draw(GridManager.GridToScreen(room.RoomSize), Game1.BasicTexture);
                }
            }
            Base.Draw(new Rectangle(0, 0, 300, 10000), Game1.BasicTexture, Color.Gray);
            //brush sellection
            menu.MenuUpdate();
            if (menu.check("room"))
            {
                brushState = (int)AvaliableBrushes.room;
            }
            if (menu.check("tiles"))
            {
                brushState = (int)AvaliableBrushes.tile;
            }
            #region Mouse Updates
            //this part of the code updates values of the mouse that needs to be carried over next tick
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                previouseMousePressedState = true;
            else
                previouseMousePressedState = false;

            #endregion
        }



        public void Save() // output any level information from runtime to folder. IMPORTAINT: must only be useable in level editor
        {
            // code to convert every room to text format
            if (Rooms.Count > 0)
            {
                string roomfile = Rooms[0].Save();
                for (int n = 1; n < Rooms.Count; n++)
                {
                    roomfile += '&' + Rooms[n].Save();
                }
                File.WriteAllText(location + @"/Rooms.txt", roomfile);

            }
            // there's actrually no reason to save anything other than the room files, the texture files are only needed to load in and has no change done to them in runtime 
        }
    }
}
