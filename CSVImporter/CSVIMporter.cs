using Microsoft.Xna.Framework.Content.Pipeline;
using System.IO;

using TImport = System.String;

namespace CSVPipelineExtension
{
    [ContentImporter(".csv", DisplayName = "CSVImporter", DefaultProcessor = "CSVProcessor")]
    public class CSVIMporter : ContentImporter<TImport>
    {
        public override TImport Import(string filename, ContentImporterContext context)
        {
            context.Logger.LogMessage("Importing CSV: " + filename);
            return File.ReadAllText(filename);
        }
    }
}
