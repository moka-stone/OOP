using System.Diagnostics;
namespace Consol_Paint_App
{
    class UnitTesting()
    {
        

        public static void RunAssertions()
        {
            // Тесты для ValidateHorizPosition
            Console.WriteLine("Testing ValidateHorizPosition...");
            Debug.Assert(Validator.ValidateHorizPosition(-10) == false);
            Debug.Assert(Validator.ValidateHorizPosition(0) == true);
            Debug.Assert(Validator.ValidateHorizPosition(10) == true);
            Debug.Assert(Validator.ValidateHorizPosition(250) == false);
           

            // Тесты для ValidateVerticalPosition
            Console.WriteLine("Testing ValidateVerticalPosition...");
            Debug.Assert(Validator.ValidateVerticalPosition(-10) == false);
            Debug.Assert(Validator.ValidateVerticalPosition(0) == true);
            Debug.Assert(Validator.ValidateVerticalPosition(10) == true);
            Debug.Assert(Validator.ValidateVerticalPosition(50) == false);

            // Тесты для ValidateEllipseParams
            Console.WriteLine("Testing ValidateEllipseParams...");
            Debug.Assert(Validator.ValidateEllipseParams(0, 0, 30, 30) == true);
            Debug.Assert(Validator.ValidateEllipseParams(-1, 0, 30, 30) == false);
            Debug.Assert(Validator.ValidateEllipseParams(0, -1, 30, 30) == false);
            Debug.Assert(Validator.ValidateEllipseParams(0, 0, 0, 30) == false);
            Debug.Assert(Validator.ValidateEllipseParams(0, 0, 30, 0) == false);

            // Тесты для ValidateRectParams
            Console.WriteLine("Testing ValidateRectParams...");
            Debug.Assert(Validator.ValidateRectParams(0, 0, 30, 30) == true);
            Debug.Assert(Validator.ValidateRectParams(-1, 0, 30, 30) == false);
            Debug.Assert(Validator.ValidateRectParams(0, -1, 30, 30) == false);
            Debug.Assert(Validator.ValidateRectParams(0, 0, 0, 30) == false);
            Debug.Assert(Validator.ValidateRectParams(0, 0, 30, 0) == false);
            Debug.Assert(Validator.ValidateRectParams(0, 0, 0, 0) == false);


            // Тесты для ValidateTriangleParams
            Console.WriteLine("Testing ValidateTriangleParams...");
            Debug.Assert(Validator.ValidateTriangleParams(0, 0, 30, 30,30) == true);
            Debug.Assert(Validator.ValidateTriangleParams(-1, 0, 30, 30,30) == false);
            Debug.Assert(Validator.ValidateTriangleParams(0, -1, 30, 30,30) == false);
            Debug.Assert(Validator.ValidateTriangleParams(0, 0, 0, 30,0) == false);
            Debug.Assert(Validator.ValidateTriangleParams(0, 0, 30, 0,0) == false);
            Debug.Assert(Validator.ValidateTriangleParams(0, 0, 0, 0,30) == false);

            Console.WriteLine("All assertions passed.");
        }

        
    }
}