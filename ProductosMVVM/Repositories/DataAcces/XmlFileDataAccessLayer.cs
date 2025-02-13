using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ProductosMVVM.Models.Repositories.DataAcces
{
    public class XmlFileDataAccessLayer : IDataAccessLayer
    {
        private readonly string _filePath;

        public XmlFileDataAccessLayer(string filePath) => _filePath = filePath;

        public List<T> LoadData<T>()
        {
            if (File.Exists(_filePath))
            {
                using (FileStream fs = new FileStream(_filePath, FileMode.Open))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                    return (List<T>)serializer.Deserialize(fs);
                }
            }

            return new List<T>();
        }

        public void SaveData<T>(List<T> data)
        {
            using (FileStream fs = new FileStream(_filePath, FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                serializer.Serialize(fs, data);
            }
        }
    }
}
