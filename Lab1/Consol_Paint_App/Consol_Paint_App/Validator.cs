

namespace Consol_Paint_App
{
    public static class Validator
    {
        public static bool ValidateHorizPosition(int param)
        {
            if (param >= 0 && param <= 200)
            {
                return true;
            }
            return false;
        }
        public static bool ValidateVerticalPosition(int param)
        {
            if (param >= 0 && param <= 40)
            {
                return true;
            }
            return false;
        }

        public static bool ValidateEllipseParams(int x, int y, int a, int b)
        {
            if (!ValidateHorizPosition(x)) return false;
            if (!ValidateVerticalPosition(y)) return false;

            if (a <= 0) return false;
            if (b <= 0) return false;

            return true;
        }
        public static bool ValidateRectParams(int x, int y, int a, int b)
        {
            if (!ValidateHorizPosition(x)) return false;
            if (!ValidateVerticalPosition(y)) return false;

            if (a <= 0) return false;
            if (b <= 0) return false;

            return true;
        }
        public static bool ValidateTriangleParams(int x, int y, int a, int b,int c)
        {
            if (!ValidateHorizPosition(x)) return false;
            if (!ValidateVerticalPosition(y)) return false;

            if (a <= 0) return false;
            if (b <= 0) return false;
            if (c <= 0) return false;
            if (a + b < c || a + c < b || b + c < a) return false;

            return true;
        }
    }
}
