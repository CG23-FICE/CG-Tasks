using System.Reflection;
using ImageConverter.Sdk.Interfaces;

namespace MainProject.Utils
{
    public class PluginsReader
    {
        static string _workingDirectory = Environment.CurrentDirectory;
        static string _projectDirectory = Directory.GetParent(_workingDirectory)!.Parent!.Parent!.FullName;
        static string _pathToPlugins = Path.Combine(_projectDirectory, "Plugins");
        static string _pathToReaders = Path.Combine(_projectDirectory, "Readers");
        static string _pathToWriters = Path.Combine(_projectDirectory, "Writers");

        public static List<IImageWriter> GetAllAvailableWriters()
        {
            var writers = GetWriterPlugins();
            return writers.Concat(GetOurWriters()).ToList();
        }

        public static List<IImageWriter> GetOurWriters()
        {
            var pluginsLists = new List<IImageWriter>();

            var assembly = Assembly.LoadFile(Path.Combine(_workingDirectory, "MainProject.dll"));

            var pluginTypes = assembly.GetTypes().Where(t => typeof(IImageWriter).IsAssignableFrom(t) && !t.IsInterface).ToArray();

            foreach (var pluginType in pluginTypes)
            {
                var pluginInstance = Activator.CreateInstance(pluginType) as IImageWriter;
                pluginsLists.Add(pluginInstance);
            }

            return pluginsLists;
        }
        public static List<IImageWriter> GetWriterPlugins()
        {
            var pluginsLists = new List<IImageWriter>();

            var plugins = Directory.GetFiles(_pathToPlugins, "*.dll");

            foreach (var plugin in plugins)
            {
                var assembly = Assembly.LoadFile(Path.Combine(Directory.GetCurrentDirectory(), plugin));

                var pluginTypes = assembly.GetTypes().Where(t => typeof(IImageWriter).IsAssignableFrom(t) && !t.IsInterface).ToArray();

                foreach (var pluginType in pluginTypes)
                {
                    var pluginInstance = Activator.CreateInstance(pluginType) as IImageWriter;
                    pluginsLists.Add(pluginInstance);
                }
            }

            return pluginsLists;
        }

        public static List<IImageReader> GetAllAvailableReaders()
        {
            var readers = GetReaderPlugins();
            return readers.Concat(GetOurReaders()).ToList();
        }

        public static List<IImageReader> GetOurReaders()
        {
            var pluginsLists = new List<IImageReader>();

            var assembly = Assembly.LoadFile(Path.Combine(_workingDirectory, "MainProject.dll"));

            var pluginTypes = assembly.GetTypes().Where(t => typeof(IImageReader).IsAssignableFrom(t) && !t.IsInterface).ToArray();

            foreach (var pluginType in pluginTypes)
            {
                var pluginInstance = Activator.CreateInstance(pluginType) as IImageReader;
                pluginsLists.Add(pluginInstance);
            }

            return pluginsLists;
        }

        public static List<IImageReader> GetReaderPlugins()
        {
            var pluginsLists = new List<IImageReader>();

            var plugins = Directory.GetFiles(_pathToPlugins, "*.dll");

            foreach (var plugin in plugins)
            {
                var assembly = Assembly.LoadFile(Path.Combine(Directory.GetCurrentDirectory(), plugin));

                var pluginTypes = assembly.GetTypes().Where(t => typeof(IImageReader).IsAssignableFrom(t) && !t.IsInterface).ToArray();

                foreach (var pluginType in pluginTypes)
                {
                    var pluginInstance = Activator.CreateInstance(pluginType) as IImageReader;
                    pluginsLists.Add(pluginInstance);
                }
            }

            return pluginsLists;
        }
    }
}
