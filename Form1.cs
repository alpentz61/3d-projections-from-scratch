using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Phase_1_Basic_Projections
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //-----------------------------------------------------------------------------
            //Test Matrix operations
            //-----------------------------------------------------------------------------

            //Test Multiplication 1:
            float[,] A1 = {  {3,4,2}   };
            float[,] B1 = {  {13,9,7,15},
                            {8,7,4,6},
                            {6,4,0,3}  };
            float[,] C1 = MatrixMath.multiply(A1, B1);
            Console.WriteLine("\nTestMultiplication 1:-------------------------------------");
            Console.WriteLine("Expected Matrix:");
            MatrixMath.printMatrix(new float[,]{{83,63,37,75}});
            Console.WriteLine("Actual Matrix:");
            MatrixMath.printMatrix(C1);

            //Test Addition 1:
            float[,] A2 = {  {3,7,11,22},
                            {4,3,5,-4}  };
            float[,] B2 = {  {3,2,1,2},
                            {7,-3,43,21} };
            float[,] C2 = MatrixMath.add(A2, B2);
            Console.WriteLine("\nTest Addition 1:-------------------------------------");
            Console.WriteLine("Expected Matrix:");
            MatrixMath.printMatrix(new float[,] { { 6, 9, 12, 24},{11,0,48,17}});
            Console.WriteLine("Actual Matrix:");
            MatrixMath.printMatrix(C2);

            //Test Rotation Matrix------------------------------------------------------------
            //Test 1: Identity matrix 
            float[,] rotMat1 = MatrixMath.caculateRotationMatrix(0, 0, 0);
            Console.WriteLine("\nTest Roation Matrix 1:-------------------------------------");
            Console.WriteLine("Expected Matrix:");
            MatrixMath.printMatrix(new float[,] { { 1,0,0 }, { 0,1,0 }, {0,0,1}});
            Console.WriteLine("Actual Matrix:");
            MatrixMath.printMatrix(rotMat1);

            //Test 2: Rotate about the Y axis
            float[,] rotMat2 = MatrixMath.caculateRotationMatrix(0, (float)Math.PI/4, 0);
            float[,] startingMatrix2 = {{ 1},{ 0},{ 0}};
            float[,] rotatedMatrix2 = MatrixMath.multiply(rotMat2, startingMatrix2);
            Console.WriteLine("Expected Rotated Vector:");
            MatrixMath.printMatrix(new float[,] { {(float)Math.Pow(2,0.5)/2},{0},{(float)-Math.Pow(2,0.5)/2} });
            Console.WriteLine("Actual Rotated Vector:");
            MatrixMath.printMatrix(rotatedMatrix2);

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {   /*
            //-----------------------------------------------------------------------------
            //Test Drawing to the screen
            //-----------------------------------------------------------------------------
            
            Rectangle screenDimensions = new Rectangle(0,0,300,300);
            //Test drawing is a house (vertices 0 - 9):
            //Also, 4 additional vertices were added to test the clipping (10-13)
            //And 4 additional vertices were added to test the 4 corners
            double[,] drawingTest_NormScreenPositions = new double[,]
            {
                //house vertices
                {0,0.75},
                {-0.5, 0.25},
                {-0.5,-0.5},
                {-0.125,-0.5},
                {0.125, -0.5,},
                {0.5,-0.5},
                {0.5,0.25},
                {0.125,-0.125},
                {-0.125,-0.125},
                //out of bounds vertices
                {-2,2},
                {2,2},
                {2,-2},
                {-2,-2},
               //corner vertices:
                {-0.99, 0.99},
                {0.99, 0.99},
                {0.99, -0.99},
                {-0.99, -0.99};

            };
            Vertex[] drawingTestVertexBuffer = new Vertex[drawingTest_NormScreenPositions.GetLength(0)];
            for (int i=0; i<drawingTestVertexBuffer.Length; i++)
            {
                drawingTestVertexBuffer[i].normalizedScreenPosition.x = (float)drawingTest_NormScreenPositions[i,0];
                 drawingTestVertexBuffer[i].normalizedScreenPosition.y = (float)drawingTest_NormScreenPositions[i,1];

            }
            RenderCore renderer = new RenderCore(drawingTestVertexBuffer, screenDimensions);
            e.Graphics.DrawImage(renderer.RenderFrame(), 0, 0);
             
            */

            /*
            //-----------------------------------------------------------------------------
            //Perpsective Projection Test 1 -  without the camera transform
            //-----------------------------------------------------------------------------
            float viewDistance = (float)(1 / Math.Tan(Math.PI / 6)); //calculate view distance based off a 60 degree FOV
            float[,] projTest1_3DVertices = new float[,]
            {



            }
             

            ProjectionCore projCore = new ProjectionCore(ver,viewDistance);
            
             */
        }
    }
}