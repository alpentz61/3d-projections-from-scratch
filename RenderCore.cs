using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Phase_1_Basic_Projections
{
    public class RenderCore
    {
        Vertex[] vertexBuffer;

        Rectangle screenDimensions;
        Color pixelColor;

        public RenderCore(Vertex[] vertexBuffer, Rectangle screenDimensions)
        {
            this.vertexBuffer = vertexBuffer;
            this.screenDimensions = screenDimensions;
            pixelColor = Color.DarkBlue;
        }

        public Bitmap RenderFrame()
        {
            Bitmap frame = new Bitmap(screenDimensions.Width, screenDimensions.Height);
         
            for (int i = 0; i < vertexBuffer.Length; i++)
            {
                float normalizedX = vertexBuffer[i].normalizedScreenPosition.x;
                float normalizedY = vertexBuffer[i].normalizedScreenPosition.y;

                //scale and offset the normalized (-1 to 1) coordinates to the screen dimensions (0 to "Screen Dimension")
                int screenX = (int)( (normalizedX + 1) * screenDimensions.Width/2); 
                int screenY = (int)( (normalizedY + 1) * screenDimensions.Height/2);

                //flip the y component of the coordinates so that they render properly in the the bitmap (whose origin is in the top left corner)
                screenY = screenDimensions.Height - screenY;

                //draw the vertices to the frame, with clipping applied so only valid pixel coordinates are drawn
                if (screenX >= 0 && screenX <= screenDimensions.Width-1 && 
                    screenY >= 0 && screenY <= screenDimensions.Height-1)
                {   
                    frame.SetPixel(screenX, screenY, pixelColor);
                }
            }

            return frame;
        }
     }
}
