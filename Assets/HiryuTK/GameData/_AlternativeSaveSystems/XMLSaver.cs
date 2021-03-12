using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Xml;
using System.Collections.Generic;
using System.IO;

namespace HiryuTK.GameDataManagement
{
    //Application.dataPath = point to asset/project directory. In the PC build, it will point to (PathToExecutable)/executable_Data. In the webplayer build, it won't work. You'll have to use the WWW class.
    //Application.persistentDataPat = point to a directory where you applicaation can store user specific data on the target computer. 

    [System.Serializable]
    public class Monster
    {
        [XmlAttribute("Name")]
        public string name;
        public int Health;
    }

    [XmlRoot("MonsterCollection")]
    public class OptionalContainer
    {
        [XmlArray("Monsters"), XmlArrayItem("Monster")]
        public List<Monster> Monster = new List<Monster>();
    }

    public class XMLSaver
    {
        string path = Application.dataPath + "/XML/monsters.xml";

        public void SaveToFile()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(XMLSaver));
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, this);
            }
        }

        public XMLSaver LoadFromFile()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(XMLSaver));
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                return serializer.Deserialize(stream) as XMLSaver;
            }
        }

        //Loads the xml directly from the given string. Useful in combination with www.text
        public static XMLSaver LoadFromText(string text)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(XMLSaver));
            return serializer.Deserialize(new StringReader(text)) as XMLSaver;
        }
    }
}
