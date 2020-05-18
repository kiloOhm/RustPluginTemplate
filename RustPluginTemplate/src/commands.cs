namespace Oxide.Plugins
{
    using UnityEngine;

    partial class RustPluginTemplate : RustPlugin
    {
        partial void initCommands()
        {
            cmd.AddChatCommand("pos", this, nameof(posCommand));
            cmd.AddChatCommand("list", this, nameof(listCommand));
        }

        private void posCommand(BasePlayer player, string command, string[] args)
        {
            if (!config.allowPosCom) return;
            if (!hasPermission(player, permissions.use))
            {
                PrintToChat(player, lang.GetMessage("noPermission", this, player.UserIDString));
                return;
            }
            //get player Position
            Vector3 pos = player.transform.position;
            //store Position in Data
            StoredData.addPosition(pos);
            //output localized message
            PrintToChat(player, lang.GetMessage("posOutput", this, player.UserIDString), pos.x, pos.y, pos.z);
        }

        private void listCommand(BasePlayer player, string command, string[] args)
        {
            UIPositionList(player);
        }
    }
}