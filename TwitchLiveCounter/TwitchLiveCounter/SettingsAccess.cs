using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLiveCounter {
    class SettingsAccess {

        private static SettingsAccess instance;

        private SettingsAccess() { }

        public static SettingsAccess Instance {
            get {
                if (instance == null) {
                    instance = new SettingsAccess();
                }
                return instance;
            }
        }
        
        public string getUserList() {
            return Properties.Settings.Default.userList;
        }
        public void setUserList(string strList) {
            strList = Properties.Settings.Default.userList;
        }

        public string getTwitchUsername() {
            return Properties.Settings.Default.twitchUsernameSettings;
        }
        public void setTwitchUsername(string strUsername) {
            strUsername = Properties.Settings.Default.twitchUsernameSettings;
        }

        public int getTimerInterval() {
            return Properties.Settings.Default.timerInterval;
        }
        public void setTimerInterval(int intTimerInterval) {
            intTimerInterval = Properties.Settings.Default.timerInterval;
        }
        
    }
}
