namespace Oxide.Plugins
{
    using System.Collections.Generic;

    partial class RustPluginTemplate : RustPlugin
    {
        partial void initLang()
        {
            lang.RegisterMessages(messages, this);
        }

        private Dictionary<string, string> messages = new Dictionary<string, string>()
        {
            {"posOutput", "Player Coordinates: X:{0}, Y:{1}, Z:{2}"},
            {"noPermission", "You don't have permission to use this command!"}
        };
    }
}