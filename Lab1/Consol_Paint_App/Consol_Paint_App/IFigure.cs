using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consol_Paint_App
{
    public interface IFigure
    {
        void SetDimensions(int x, int y, params int[] dimensions);
        void Draw();
        void Move(int x, int y);
        void Erase();
        void AddBackground(ConsoleColor color);
        string GetInfo();

    }

}
