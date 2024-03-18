using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvOchPolis
{
    internal class Helper
    {
        public static void UpdatePosition(ref int x, ref int y, int direction)
        {
            switch (direction)
            {
                case 0:
                    y--;
                    break;
                case 1:
                    y++;
                    break;
                case 2:
                    x++;
                    break;
                case 3:
                    x--;
                    break;
                case 4:
                    y--; x++;
                    break;
                case 5:
                    y--; x--;
                    break;
                case 6:
                    y++; x++;
                    break;
                case 7:
                    y++; x--;
                    break;
            }
        }
    }
}
