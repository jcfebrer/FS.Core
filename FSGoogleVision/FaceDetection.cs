using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Vision.V1;

namespace FSGoogleVision
{
    public class Vision
    {
        public string FaceDetection(string fileName)
        {
            StringBuilder result = new StringBuilder();

            // Instantiates a client
            ImageAnnotatorClient client = ImageAnnotatorClient.Create();
            // Load the image file into memory
            Image imageSource = Image.FromFile(fileName);
            // Performs label detection on the image file
            var responseLabels = client.DetectLabels(imageSource);
            foreach (var annotation in responseLabels)
            {
                if (annotation.Description != null)
                    result.AppendLine(annotation.Description);
            }


            // [END vision_face_detection_tutorial_client]
            // [START vision_face_detection_tutorial_send_request]
            var responseFaces = client.DetectFaces(imageSource);
            // [END vision_face_detection_tutorial_send_request]

            int numberOfFacesFound = 0;
            // [START vision_face_detection_tutorial_process_response]
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(fileName))
            {
                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image))
                {
                    var cyanPen = new System.Drawing.Pen(System.Drawing.Color.Cyan, 3);
                    foreach (var annotation in responseFaces)
                    {
                        g.DrawPolygon(cyanPen, annotation.BoundingPoly.Vertices.Select(
                            (vertex) => new System.Drawing.Point(vertex.X, vertex.Y)).ToArray());
                        // [START_EXCLUDE]
                        numberOfFacesFound++;
                        // [END_EXCLUDE]
                    }
                    // [START_EXCLUDE]
                    int lastDot = fileName.LastIndexOf('.');
                    string outFilePath = lastDot < 0 ?
                        fileName + ".faces" :
                        fileName.Substring(0, lastDot) + ".faces" + fileName.Substring(lastDot);
                    image.Save(outFilePath);
                    // [END_EXCLUDE]
                }
            }
            // [END vision_face_detection_tutorial_process_response]
            // [START_EXCLUDE]
            result.AppendLine($"Found {numberOfFacesFound} "
                + $"face{(numberOfFacesFound == 1 ? string.Empty : "s")}.");
            // [END_EXCLUDE]

            return result.ToString();
        }
    }
}
