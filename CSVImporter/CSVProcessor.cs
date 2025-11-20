using Microsoft.Xna.Framework.Content.Pipeline;

using TInput = System.String;
using TOutput = System.String[];

namespace CSVPipelineExtension
{
    [ContentProcessor(DisplayName = "CSVProcessor")]
    internal class CSVProcessor : ContentProcessor<TInput, TOutput>
    {
        public override TOutput Process(TInput input, ContentProcessorContext context)
        {
            var lines = input.Split('\n');
            return lines;
        }
    }
}
