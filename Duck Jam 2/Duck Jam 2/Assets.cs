using System;
using System.Collections;

using Microsoft.Xna.Framework.Graphics;

namespace Duck_Jam_2
{
    public class Assets
    {
        public static Hashtable content = null;

        public static void Init()
        {
            Assets.content = new Hashtable();
        }

        public static void Add<T>(string key, T value)
        {
            if (content == null) { return;  }
            content[key] = (T) value;
        }
 
        public static T Get<T>(string name)
        {
           return (T) Assets.content[name];
        }
    }
}
