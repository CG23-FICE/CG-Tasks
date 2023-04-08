using System.Reflection;
using ImageConverter.Sdk.Interfaces;

namespace MainProject.Utils
{
    public class PluginsReader
    {
        static string workingDirectory = Environment.CurrentDirectory;
        static string projectDirectory = Directory.GetParent(workingDirectory)!.Parent!.Parent!.FullName;
        static string pathToPlugins = Path.Combine(projectDirectory, "Plugins");

        public static List<IImageWriter> GetAllAvailableWriters()
        {
            var writers = GetWriterPlugins();
            return writers;
        }

        public static List<IImageWriter> GetOurWriters()
        {
            throw new NotImplementedException();
        }
        public static List<IImageWriter> GetWriterPlugins()
        {
            var pluginsLists = new List<IImageWriter>();
            // 1- Read the dll files from the extensions folder

            var plugins = Directory.GetFiles(pathToPlugins, "*.dll");

            // 2- Read the assembly from files 
            foreach (var plugin in plugins)
            {
                var assembly = Assembly.LoadFile(Path.Combine(Directory.GetCurrentDirectory(), plugin));

                // 3- Exteract all the types that implements IPlugin 
                var pluginTypes = assembly.GetTypes().Where(t => typeof(IImageWriter).IsAssignableFrom(t) && !t.IsInterface).ToArray();

                foreach (var pluginType in pluginTypes)
                {
                    // 4- Create an instance from the extracted type 
                    var pluginInstance = Activator.CreateInstance(pluginType) as IImageWriter;
                    pluginsLists.Add(pluginInstance);
                }
            }

            return pluginsLists;
        }

        public static List<IImageWriter> GetAllAvailableReaders()
        {
            var readers = GetReaderPlugins();
            return readers;
        }

        public static List<IImageWriter> GetOurReaders()
        {
            throw new NotImplementedException();
        }

        public static List<IImageWriter> GetReaderPlugins()
        {
            var pluginsLists = new List<IImageWriter>();
            // 1- Read the dll files from the extensions folder
            var plugins = Directory.GetFiles(pathToPlugins, "*.dll");

            // 2- Read the assembly from files 
            foreach (var plugin in plugins)
            {
                var assembly = Assembly.LoadFile(Path.Combine(Directory.GetCurrentDirectory(), plugin));

                // 3- Exteract all the types that implements IPlugin 
                var pluginTypes = assembly.GetTypes().Where(t => typeof(IImageWriter).IsAssignableFrom(t) && !t.IsInterface).ToArray();

                foreach (var pluginType in pluginTypes)
                {
                    // 4- Create an instance from the extracted type 
                    var pluginInstance = Activator.CreateInstance(pluginType) as IImageWriter;
                    pluginsLists.Add(pluginInstance);
                }
            }

            return pluginsLists;
        }
    }
}
