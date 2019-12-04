namespace Elwark.Extensions.AspNet.Hosting
{
    public class ElwarkHostBuilderOptions
    {
        public ElwarkHostBuilderOptions(string appName, string[] args, string environment)
        {
            AppName = appName;
            Environment = environment;
            Args = args;
        }

        public string AppName { get; }
        
        public string[] Args { get; }
        
        public string Environment { get; }
    }
}