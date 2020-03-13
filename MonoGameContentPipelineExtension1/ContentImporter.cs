using System;
using System.Xml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;

using TInput = MonoGameContentPipelineExtension1.CoordinatesContent;
using TOutput = System.String;

namespace MonoGameContentPipelineExtension1
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to import a file from disk into the specified type, TImport.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    ///
    /// TODO: change the ContentImporter attribute to specify the correct file
    /// extension, display name, and default processor for this importer.
    /// </summary>

    [ContentImporter(".txt", DisplayName = "Coordinate Importer", DefaultProcessor = "ContentProcessor")]
    public class ContentImporter : ContentImporter<TInput>
    {

        public override TInput Import(string filename, ContentImporterContext context)
        {
            List<Vector2> vectorcoords = new List<Vector2>();
            string line;
            StreamReader file = new StreamReader(filename);
            while ((line = file.ReadLine()) != null)
            {
                char[] delimit = { ',' };
                string[] newLine = line.Split(delimit);
                vectorcoords.Add(new Vector2(float.Parse(newLine[0]), float.Parse(newLine[1])));
            }
            file.Close();

            return new CoordinatesContent
            {
                vectors = vectorcoords
            };
        }


        public void test(string filename)
        {
            
        }
    }



}
