using System.Reflection;
using ImageConverter.Sdk.Interfaces;

namespace MainProject.Utils
{
    public class PluginsReader
    {
        public static List<IImageWriter> GetAvailableWriters()
        {
            var pluginsLists = new List<IImageWriter>();
            // 1- Read the dll files from the extensions folder
            var files = Directory.GetFiles("Plugins", "*.dll");
            files = files.Concat(Directory.GetFiles("Writers")).ToArray();

            // 2- Read the assembly from files 
            foreach (var file in files)
            {
                var assembly = Assembly.LoadFile(Path.Combine(Directory.GetCurrentDirectory(), file));

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

        public static List<IImageReader> GetAvailableReaders()
        {
            var pluginsLists = new List<IImageReader>();
            // 1- Read the dll files from the extensions folder
            var files = Directory.GetFiles("Plugins", "*.dll");
            files = files.Concat(Directory.GetFiles("Readers")).ToArray();

            // 2- Read the assembly from files 
            foreach (var file in files)
            {
                var assembly = Assembly.LoadFile(Path.Combine(Directory.GetCurrentDirectory(), file));

                // 3- Exteract all the types that implements IPlugin 
                var pluginTypes = assembly.GetTypes().Where(t => typeof(IImageReader).IsAssignableFrom(t) && !t.IsInterface).ToArray();

                foreach (var pluginType in pluginTypes)
                {
                    // 4- Create an instance from the extracted type 
                    var pluginInstance = Activator.CreateInstance(pluginType) as IImageReader;
                    pluginsLists.Add(pluginInstance);
                }
            }

            return pluginsLists;
        }
    }
}
