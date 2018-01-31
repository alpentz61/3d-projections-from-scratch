using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phase_1_Basic_Projections
{
    public class MatrixMath
    {
        /// <summary>
        /// multiplies 2D matrix A by 2D matrix B
        /// Let the first dimension index rows and the second dimension index columns
        /// </summary>
        /// 
        /// <param name="A"></param> 2D input matrix A - Assumes that the matrix has uniform row and column lengths
        /// <param name="B"></param> 2D input matrix B - Assumes that the matrix has uniform row and column lengths
        /// <returns></returns> A * B
        public static float[,] multiply(float[,] A, float[,] B)
        {
            float[,] C;

            int numARows = A.GetLength(0);
            int numAColumns = A.GetLength(1);
            int numBRows = B.GetLength(0);
            int numBColumns = B.GetLength(1);

            //Check for empty matrices
            if (0 == numARows)
            {
                throw new Exception("Matrix A has 0 rows");
            }
            if (0 == numBRows)
            {
                throw new Exception("Matrix B has 0 rows");
            }

            //Check for valid dimensions required by multiplication
            if (numAColumns != numBRows)
            {
                throw new Exception("The number of Columns in A must match the number of rows in B. However, " + numAColumns + " columns are in A and " + numBRows + " rows are in B.");
            }

            //Create the result matrix with the proper dimentions
            C = new float[numARows, numBColumns];
            int numCRows = C.GetLength(0);
            int numCColumns = C.GetLength(1);

            //perform the matrix multiplication
            for (int i = 0; i < numCRows; i++)
            {
                for (int j = 0; j < numCColumns; j++)
                {
                    for (int n = 0; n < numAColumns; n++)
                    {
                        C[i, j] += A[i, n] * B[n, j];
                    }
                }
            }

            //return the result matrix
            return C;
        }

        public static float[,] add(float[,] A, float[,] B)
        {
            float[,] C;

            int numARows = A.GetLength(0);
            int numAColumns = A.GetLength(1);
            int numBRows = B.GetLength(0);
            int numBColumns = B.GetLength(1);

            //Check to ensure that the matrix dimensions match
            if (numARows != numBRows)
            {
                throw new Exception("Number of rows in A is not equal to the number of rows in B. A has " + numARows + " rows and B has " + numBRows + " rows.");
            }
            if (numAColumns != numBColumns)
            {
                throw new Exception("Number of columns in A is not equal to the number of columns in B.  A has " + numAColumns + " columns and B has " + numBColumns + " columns.");
            }

            //Perform the addition operation
            C = new float[numARows, numAColumns];

            for (int i = 0; i < numARows; i++)
            {
                for (int j = 0; j < numAColumns; j++)
                {

                    C[i, j] = A[i, j] + B[i, j];
                }
            }

            return C;
        }

        public static float[,] caculateRotationMatrix(float thetaX, float thetaY, float thetaZ)
        {
            float[,] XRotationMatrix = 
            {   
                {1,0,0},
                {0,Cos(thetaX),-Sin(thetaX)},
                {0,Sin(thetaX),Cos(thetaX)}  
            };
            float[,] YRotationMatrix = 
            {   
                {Cos(thetaY), 0, Sin(thetaY)},
                {0,1,0},
                {-Sin(thetaY), 0, Cos(thetaY)}
            };
            float[,] ZRotationMatrix = 
            {
                {Cos(thetaZ), -Sin(thetaZ), 0},
                {Sin(thetaZ), Cos(thetaZ), 0},
                {0,0,1}
            };

            float[,] YxZMatrix = MatrixMath.multiply(YRotationMatrix, ZRotationMatrix);
            return MatrixMath.multiply(XRotationMatrix, YxZMatrix);
        }

        public static float Sin(float x)
        {
            return (float)Math.Sin(x);
        }
        public static float Cos(float x)
        {
            return (float)Math.Cos(x);
        }
        public static void printMatrix(float[,] A)
        {
            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    Console.Write(A[i, j] + "\t\t");
                }
                Console.Write("\n");
            }
        }
    }
    public struct Vector3D
    {
        public float x;
        public float y;
        public float z;
    }
    public struct Vector2D
    {
        public float x;
        public float y;
    }
    public struct Vertex
    {
        public Vector3D worldPosition;
        public Vector2D normalizedScreenPosition;
    }
}
