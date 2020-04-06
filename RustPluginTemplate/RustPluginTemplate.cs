﻿using Newtonsoft.Json;
using Oxide.Core;
using Oxide.Core.Configuration;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Oxide.Plugins
{
    [Info("RustPluginTemplate", "OHM", "1.0.0")]
    [Description("Template")]
    class RustPluginTemplate : RustPlugin
    {
        #region fields
        DynamicConfigFile File;
        StoredData storedData;
        #endregion

        #region oxide hooks
        void Init()
        {
            permission.RegisterPermission("RPT.use", this);
            File = Interface.Oxide.DataFileSystem.GetFile("RustPluginTemplate/posData");
            loadData();
        }

        void Loaded()
        {
            lang.RegisterMessages(messages, this);
            cmd.AddChatCommand("pos", this, nameof(posCommand));
        }
        #endregion

        #region commands
        //see Loaded() hook
        private void posCommand(BasePlayer player, string command, string[] args)
        {
            if (!config.allowPosCom) return;
            if (permission.UserHasPermission(player.UserIDString, "RustPluginTemplate.use"))
            {
                //get player Position
                Vector3 pos = player.transform.position;
                //store Position in Data
                storedData.positionList.Add(pos);
                saveData();
                //output localized message
                PrintToChat(player, lang.GetMessage("posOutput", this, player.UserIDString), pos.x, pos.y, pos.z);
            }
            else
            {
                PrintToChat(player, lang.GetMessage("noPermission", this, player.UserIDString));
            }
        }
        #endregion

        #region data management
        private class StoredData
        {
            public List<Vector3> positionList = new List<Vector3>();

            public StoredData()
            {
            }
        }

        void saveData()
        {
            try
            {
                File.WriteObject(storedData);
            }
            catch (Exception E)
            {
                Puts(E.ToString());
            }
        }

        void loadData()
        {
            try
            {
                storedData = File.ReadObject<StoredData>();
            }
            catch (Exception E)
            {
                Puts(E.ToString());
            }
        }
        #endregion

        #region Config
        private static ConfigData config;

        private class ConfigData
        {
            [JsonProperty(PropertyName = "Allow Pos Command")]
            public bool allowPosCom = true;

        }

        protected override void LoadConfig()
        {
            base.LoadConfig();

            try
            {
                config = Config.ReadObject<ConfigData>();
            }
            catch
            {
                Puts("Config data is corrupted, replacing with default");
                config = new ConfigData();
            }

            SaveConfig();
        }

        protected override void SaveConfig() => Config.WriteObject(config);

        protected override void LoadDefaultConfig() => config = new ConfigData();
        #endregion

        #region Localization
        Dictionary<string, string> messages = new Dictionary<string, string>()
        {
            {"posOutput", "Player Coordinates: X:{0}, Y:{1}, Z:{2}"},
            {"noPermission", "You don't have permission to use this command!"}
        };
        #endregion
    }
}