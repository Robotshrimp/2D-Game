
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

namespace GameJom
{
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
            Rectangle roomSelection = RectangleSelection(Mouse.GetState().Position, Mouse.GetState().LeftButton == ButtonState.Pressed);
            //
            Color selectionColor = Color.Green;

            foreach (Room n in Rooms)
            {
                if(n.RoomSize.Intersects(roomSelection) ||
                    GridManager.GridToScreen(roomSelection).Width < Game1.calculationScreenSize.X ||
                    GridManager.GridToScreen(roomSelection).Height < Game1.calculationScreenSize.Y)
                {
                    selectionColor = Color.Red;
                }
            };
            if (!(Mouse.GetState().LeftButton == ButtonState.Pressed) && previouseMousePressedState == true && selectionColor == Color.Green)
            {
                Rooms.Add(new Room(roomSelection, this));
            }
            if (Mouse.GetState().LeftButton == ButtonState.Pressed) 
            {
                DrawParam.Draw(GridManager.GridToScreen(RectangleSelection(Mouse.GetState().Position, Mouse.GetState().LeftButton == ButtonState.Pressed)), Game1.BlankTexture, selectionColor);
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
        int Layer = 0;
        public void Edit(Rectangle Grid, AutomatedDraw drawParam)
        {
            this.DrawParam = drawParam;
            GridManager = new GridTexture(drawParam, Grid);
            GridManager.Griddify(Game1.Griddy);
            AutomatedDraw Base = new AutomatedDraw(); 
            Menu menu = new Menu(Base, new FontSettings(10, Color.White, new Point(30, 60)), Game1.testFonts);
            //tiles
            Menu LayerSelecter = new Menu(drawParam, new FontSettings(10, Color.White, new Point(30, 60)), Game1.testFonts);
            if (brushState == (int)AvaliableBrushes.tile)
            {
                foreach (Room room in Rooms)
                {
                    int lineSize = 0; 
                    for (int l = 0; l <= room.TotalLayers; l++)
                    {

                        if (l == room.TotalLayers)
                        {
                            if (LayerSelecter.ButtonPressedLeftAt(new Rectangle(GridManager.GridToScreen(room.RoomSize).Location + new Point(lineSize, -LayerSelecter.printParam.fontSize.Y), new Point(0, 0)), null, "add"))
                            {
                                room.SelectedLayer = l;
                                room.AddTilemap();
                            }
                        }
                        else if (LayerSelecter.ButtonPressedLeftAt(new Rectangle(GridManager.GridToScreen(room.RoomSize).Location + new Point(lineSize, -LayerSelecter.printParam.fontSize.Y), new Point(0, 0)), null, (l + 1) + " "))
                        {
                            if (room.SelectedLayer == l)

                            room.SelectedLayer = l;
                        }

                        lineSize += LayerSelecter.ButtonSize(new Rectangle(), " " + (l + 1) + " ").Width;
                    }
                }
                int n = 0;
                if(Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    if (SelectRoom(Mouse.GetState().Position, ref n))
                    {
                        Rooms[n].Edit(GridManager.GridPoint(drawParam.CalcPoint(Mouse.GetState().Position)) - Rooms[n].RoomSize.Location);
                        Rooms[n].Edit(GridManager.GridPoint(drawParam.CalcPoint(Mouse.GetState().Position)) - Rooms[n].RoomSize.Location, 0, 1);
                    }
                }
                if (Mouse.GetState().RightButton == ButtonState.Pressed)
                {
                    if (SelectRoom(Mouse.GetState().Position, ref n))
                    {
                        Rooms[n].Edit(GridManager.GridPoint(drawParam.CalcPoint(Mouse.GetState().Position)) - Rooms[n].RoomSize.Location, Room.empty);
                    }
                }
                foreach (Room room in Rooms)
                {
                    GridManager.ModularTexture(Game1.Griddy, room.RoomSize);
                }
                foreach (Room room in Rooms)
                {
                    room.Draw(drawParam, GridManager.Grid);
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
                    GridManager.ModularTexture(Game1.Griddy, room.RoomSize);
                }
            }
            Base.Draw(new Rectangle(0, 0, 300, 10000), Game1.BlankTexture, Color.Gray);
            //brush sellection
            if (menu.ButtonPressedLeftAt(new Rectangle(), null, "room"))
            {
                brushState = (int)AvaliableBrushes.room;
            }
            if (menu.ButtonPressedLeftAt(new Rectangle(0, 100, 0, 0), null, "tiles"))
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
