using System;

namespace DFSClient.Core
{
    public class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();

            string invokedVerb = "";
            object invokedVerbInstance = null;
            if (!CommandLine.Parser.Default.ParseArguments(args, options,
                (verb, subOptions) =>
                {
                    invokedVerb = verb;
                    invokedVerbInstance = subOptions;
                }))
            {
                Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
            }

            var program = new Program();
            program.Run(invokedVerb, invokedVerbInstance as CommonSubOptions);
        }

        public void Run(string verb, CommonSubOptions options)
        {
            var nameNode = new ClientProtocol(options);

            if (options is ListingSubOptions)
            {
                var listingOptions = options as ListingSubOptions;
                var srcPath = listingOptions.FilePath.Count > 0 ? listingOptions.FilePath[0] : "";
                var listing = nameNode.GetListing(srcPath);
                foreach (var file in listing)
                {
                    Console.WriteLine(file);
                }
            }
            else if (options is PutSubOptions)
            {
                var putSubOptions = options as PutSubOptions;
                nameNode.Create(putSubOptions.PutValues[0], putSubOptions.PutValues[1]);
            }
            else if (options is DeleteSubOptions)
            {
                var deleteSubOptions = options as DeleteSubOptions;
                nameNode.Delete(deleteSubOptions.FilePath[0]);
            }
            else if (options is MkdirSubOptions)
            {
                var mkdirSubOptions = options as MkdirSubOptions;
                nameNode.Mkdir(mkdirSubOptions.DirectoryPath[0]);
            }
        }
    }
}
