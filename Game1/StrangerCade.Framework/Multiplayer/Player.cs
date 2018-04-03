using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrangerCade.Framework.Multiplayer
{
    class Player
    {
        public float MouseX;
        public float MouseY;
        public Vector2 Mouse;

        public float MouseDX;
        public float MouseDY;
        public Vector2 MouseMovement;
        
        public string Name;

        public int NumInRoom;

        public Player(string name, float mousex, float mousey, float mousedx, float mousedy, int num)
        {
            Name = name;

            MouseX = mousex;
            MouseY = mousey;
            Mouse = new Vector2(mousex, mousey);

            MouseDX = mousedx;
            MouseDY = mousedy;
            MouseMovement = new Vector2(mousedx, mousedy);

            NumInRoom = num;
        }
    }
}
