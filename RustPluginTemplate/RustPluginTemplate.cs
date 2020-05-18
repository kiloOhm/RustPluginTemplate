// Requires: GUICreator

namespace Oxide.Plugins
{
    [Info("RustPluginTemplate", "OHM", "0.1.0")]
    [Description("Template")]
    partial class RustPluginTemplate : RustPlugin
    {
        private static RustPluginTemplate PluginInstance;

        public RustPluginTemplate()
        {
            PluginInstance = this;
        }
    }
}