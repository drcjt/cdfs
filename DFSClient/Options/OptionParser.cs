namespace DFSClient.Options
{
    public class OptionParser : IOptionParser
    {
        public CommonSubOptions ParseOptions(string[] args)
        {
            var options = new Options();

            string invokedVerb = "";
            object invokedVerbInstance = null;
            var parsedSuccessfully = CommandLine.Parser.Default.ParseArguments(args, options, (verb, subOptions) =>
            {
                invokedVerb = verb;
                invokedVerbInstance = subOptions;
            });


            return parsedSuccessfully ? invokedVerbInstance as CommonSubOptions : null;
        }
    }
}
