using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phase_1_Basic_Projections
{
    public class GraphicsEngine
    {
        ProjectionCore projectCore;
        RenderCore renderCore;

        public void initialize()
        {
            //Intialize the vertex buffer with 3d vertices
          //  Vertex[] vertexBuffer = 

   
            
        }
        public void ProcessFrame()
        {


        }
        
        /// <summary>
        /// Procedurally generates the vertices for a basic 3d cube of unit side length, body centered on 0,0,0
        /// </summary>
        /// <param name="increment"></param> The distance between each vertex on an edge, specied in world units.  1/increment must be a positive integer
        /// <returns></returns> The geneated vertices as an array of 3D vectors in row form
        public float[,] generate3dBoxVertices(float increment)
        {
            //Verify that 1/increment is an integer
            if ((1/increment)%1 != 0)
            {
                throw new Exception("1/increment must be a integer, but the observed value was 1/increment=" + 1/increment);
            }
            //Verify that 1/increment is positive 
            if ((1/increment) < 0)
            {
                throw new Exception("1/increment must be positive, but the observed value was 1/increment=" + 1/increment);
            }
           
            //Each edge of the cube has 1/incrment + 1 vertices (including corner vertices).
            //The cube is built in two separate loops:
           
            //1.   The first loop creates the horizontal edges on the top and bottom.  These edges are 
            //built in the clockwise direction, in which the last vertex of each edge (the corner vertices) are ommitteed 
            //to prevent vertex duplication (since the last vertex of one edge is the first vertex of the next).
            //Because of this, the number of vertices in each "horizontal edge" is actually 
            //one less than the number of vertices per edge described above.
            int horizLen = (int) (1 / increment);
           
            //2.   The second loop builds the vertical edges.  Since both of the corner vertices of each of 
            //the vertical edes are already included in the horizontal edges created previously,
            //the number of vertices in each "vertical edge" is actually two less than the actual 
            //number of vertices per edge.  Note that if the incrment is 1 (only corner vertices)
            //the vertical length with be 0 and no vertical vertices will be created.
            int vertLen = (int)(1 / increment - 1);

            //Create the vertex buffer for the cube based on 
            //the number of vertices in each edge
            int totalLen = horizLen * 8 + vertLen * 4;
            float[,] vertices = new float[totalLen, 3];
    
            //Create the horzintal vertices
            for (int i = 0; i < horizLen; i++)
            {
                //Each horizontal edge has one component which is either increasing or decreasing (the others are either 0.5 or -0.5)
                float incrComponent = -0.5f + i*increment;
                float decrComponent = 0.5f - i*increment;

                //horizontal edge 0: (-0.5, 0.5, 0.5) to (0.5, 0.5, 0.5)
                vertices[i, 0] = incrComponent;
                vertices[i, 1] = 0.5f;
                vertices[i, 2] = 0.5f;
                //horizontal edge 1: (0.5, 0.5, 0.5) to (0.5, 0.5, -0.5)
                vertices[i+horizLen, 0] = 0.5f;
                vertices[i+horizLen, 1] = 0.5f;
                vertices[i+horizLen, 2] = decrComponent;
                //horizontal edge 2: (0.5, 0.5, -0.5) to (-0.5, 0.5, -0.5)
                vertices[i+2*horizLen, 0] = decrComponent;
                vertices[i+2*horizLen, 1] = 0.5f;
                vertices[i+2*horizLen, 2] = -0.5f;
                //horizontal edge 3: (-0.5, 0.5, -0.5) to (-0.5, 0.5, 0.5)
                vertices[i+3*horizLen, 0] = -0.5f;
                vertices[i+3*horizLen, 1] = 0.5f;
                vertices[i+3*horizLen, 2] = incrComponent;
                //horizontal edge 4: (-0.5, -0.5, 0.5) to (0.5, -0.5, 0.5)
                vertices[i+4*horizLen, 0] = incrComponent;
                vertices[i+4*horizLen, 1] = -0.5f;
                vertices[i+4*horizLen, 2] = 0.5f;
                //horizontal edge 5: (0.5, -0.5, 0.5) to (0.5, -0.5, -0.5)
                vertices[i+5*horizLen, 0] = 0.5f;
                vertices[i+5*horizLen, 1] = -0.5f;
                vertices[i+5*horizLen, 2] = decrComponent;
                //horizontal edge 6: (0.5, -0.5, -0.5) to (-0.5, -0.5, -0.5)
                vertices[i+6*horizLen, 0] = decrComponent;
                vertices[i+6*horizLen, 1] = -0.5f;
                vertices[i+6*horizLen, 2] = -0.5f;
                //horizontal edge 7: (-0.5, -0.5, -0.5) to (-0.5, -0.5, 0.5)
                vertices[i+7*horizLen, 0] = -0.5f;
                vertices[i+7*horizLen, 1] = -0.5f;
                vertices[i + 7 * horizLen, 2] = incrComponent;
            }

            //store the vertical vertices beginning after the last horizontal vertex
            int vertBase = 8 * horizLen;

            //Create the vertical vertices
            for (int i = 0; i < vertLen; i++)
            {
                //Each vertical edge has a y component which is increasing (the others are either 0.5 or -0.5)
                //Note that the index is offset by 1 in order to skip the first vertex in each vertical edge
                float yComponent = -0.5f + (i+1)* increment;

                //vertical edge 0: (-0.5,-0.5,0.5) to (-0.5,0.5,0.5)
                vertices[i + vertBase, 0] = -0.5f;
                vertices[i + vertBase, 1] = yComponent;
                vertices[i + vertBase, 2] = 0.5f;
                //vertical edge 1: (0.5,-0.5,0.5) to (0.5,0.5,0.5)
                vertices[i + vertLen + vertBase, 0] = 0.5f;
                vertices[i + vertLen + vertBase, 1] = yComponent;
                vertices[i + vertLen + vertBase, 2] = 0.5f;
                //vertical edge 2: (0.5,-0.5,-0.5) to (0.5,0.5,-0.5)
                vertices[i + 2*vertLen + vertBase, 0] =0.5f;
                vertices[i + 2*vertLen + vertBase, 1] = yComponent;
                vertices[i + 2*vertLen + vertBase, 2] = -0.5f;
                //vertical edge 3: (,,) to (,,)
                vertices[i + 3*vertLen + vertBase, 0] = -0.5f;
                vertices[i + 3 * vertLen + vertBase, 1] = yComponent;
                vertices[i + 3 * vertLen + vertBase, 2] = -0.5f;
            }

            return vertices;
        }
         
    }
}
