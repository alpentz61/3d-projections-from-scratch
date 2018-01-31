using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phase_1_Basic_Projections
{
    public class ProjectionCore
    {
        public Vertex[] vertexBuffer;
        float viewDistance;

        public ProjectionCore(Vertex[] vertexBuffer, float viewDistance)
        {
            //viewDistance = 1 / ((float)Math.Tan(horizontalFOV / 2));
            this.vertexBuffer = vertexBuffer;
            this.viewDistance = viewDistance;
        }

        public void performPerspectiveProjection(Vector3D camWorldPos, Vector3D camRot,bool useCameraTransform)
        {
            //calculate the camera transform
            float[,] cameraPosMatrix = new float[,] 
            {
                {-camWorldPos.x},
                {-camWorldPos.y}, 
                {-camWorldPos.z}
            };
            float[,] cameraRotMatrix = MatrixMath.caculateRotationMatrix(-camRot.x, -camRot.y, -camRot.z);

            for (int i = 0; i < vertexBuffer.Length; i++)
            {
                //Retrieve the vertex from the vertex buffer and put in a 3 x 1 column matrix (vector)
                float[,] vertex = 
                {
                    { vertexBuffer[i].worldPosition.x },
                    { vertexBuffer[i].worldPosition.y }, 
                    { vertexBuffer[i].worldPosition.z }
                };

                //Perform the camera transform
                float[,] vertexTransformed;
                if (useCameraTransform)
                {
                    //subtract the camera position from the vertex position 
                    float[,] vertexMinusCamPos = MatrixMath.add(vertex, cameraPosMatrix);

                    //"subtract" the camera orientation relative to the world origin from the orientation of the vertex relative to the world origin
                    //The result is the orginal vertex, transformed into the camera coordinate system
                    vertexTransformed = MatrixMath.multiply(cameraRotMatrix, vertexMinusCamPos);
                }
                else
                {
                    //if we are not using the camera transform, consider the original vertex to already be in the camera coordinate system
                    vertexTransformed = vertex;
                }

                //Perform the Perspective projection operation on the transformed vertex
                //The result is the normalized 2D screen coordinate
                float normScreenX = (viewDistance / vertexTransformed[3, 1]) * vertexTransformed[1, 1];
                float normScreenY = (viewDistance / vertexTransformed[3, 1]) * vertexTransformed[2, 1];

                //store the normalized 2D screen coordinates back into the vertex buffer
                vertexBuffer[i].normalizedScreenPosition.x = normScreenX;
                vertexBuffer[i].normalizedScreenPosition.y = normScreenY;
            }
        }
    }
}