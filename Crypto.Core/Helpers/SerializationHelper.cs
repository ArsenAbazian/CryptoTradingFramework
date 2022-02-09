using Crypto.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Helpers {
    public static class SerializationHelper {
        public static bool Load(ISupportSerialization obj, Type t) {
            string fileName = obj.FileName;
            object res = FromFile(fileName, t);
            if(res == null)
                return false;

            PropertyInfo[] props = obj.GetType().GetProperties(System.Reflection.BindingFlags.Public | BindingFlags.Instance);
            foreach(var prop in props) {
                if(!prop.CanRead)
                    continue;
                if(prop.GetCustomAttribute(typeof(XmlIgnoreAttribute)) != null)
                    continue;
                if(prop.CanWrite) {
                    prop.SetValue(obj, prop.GetValue(res, null));
                }
                else {
                    if(prop.PropertyType.IsValueType)
                        continue;
                    object value = prop.GetValue(res, null);
                    if(value is IList) {
                        IList srcList = (IList)value;
                        IList dstList = (IList)prop.GetValue(obj, null);
                        dstList.Clear();
                        for(int i = 0; i < srcList.Count; i++) {
                            dstList.Add(srcList[i]);
                        }
                    }
                    else if(value is IDictionary) {
                        IDictionary srcDict = (IDictionary)value;
                        IDictionary dstDict = (IDictionary)prop.GetValue(obj, null);
                        dstDict.Clear();
                        foreach(object key in srcDict.Keys) {
                            dstDict.Add(key, srcDict[key]);
                        }
                    }
                }
            }
            return true;
        }
        public static ISupportSerialization FromFile(string fileName, Type t) {
            if(string.IsNullOrEmpty(fileName))
                return null;
            if(!File.Exists(fileName))
                return null;
            try {
                ISupportSerialization obj = null;
                XmlSerializer formatter = new XmlSerializer(t);
                using(FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate)) {
                    obj = (ISupportSerialization)formatter.Deserialize(fs);
                }
                obj.FileName = fileName;
                obj.OnEndDeserialize();
                return obj;
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return null;
            }
        }

        public static bool Save(ISupportSerialization obj, Type t, string path) {
            if(File.Exists(obj.FileName))
                obj.FileName = Path.GetFileName(obj.FileName);
            string fullName = string.IsNullOrEmpty(path)? obj.FileName: path + "\\" + obj.FileName;
            string tmpFile = fullName + ".tmp";
            if(!string.IsNullOrEmpty(path) && !Directory.Exists(path))
                Directory.CreateDirectory(path);
            if(string.IsNullOrEmpty(obj.FileName))
                return false;
            try {
                obj.OnStartSerialize();
                XmlSerializer formatter = new XmlSerializer(t);
                using(FileStream fs = new FileStream(tmpFile, FileMode.Create)) {
                    formatter.Serialize(fs, obj);
                }
                if(File.Exists(fullName))
                    File.Delete(fullName);
                File.Move(tmpFile, fullName);
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
            
            return true;
        }
    }

    public interface ISupportSerialization {
        string FileName { get; set; }
        void OnEndDeserialize();
        void OnStartSerialize();
    }
}
