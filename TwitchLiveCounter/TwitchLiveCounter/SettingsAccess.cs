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
        public string returnString() {
            return "hello world";
        } 

    }
}
