using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Helpers {
    public static class BindingHelper {
        public static object GetBindingValue(string dataSourcePath, object root) {
            string[] props = dataSourcePath.Split('.');
            object res = root;
            for(int i = 0; i < props.Length; i++) {
                PropertyInfo pInfo = res.GetType().GetProperty(props[i], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if(pInfo == null)
                    return null;
                res = pInfo.GetValue(res, null);
                if(res == null)
                    break;
            }
            return res;
        }

        public static object GetBindingOwner(string dataSourcePath, object root) {
            string[] props = dataSourcePath.Split('.');
            object res = root;
            for(int i = 0; i < props.Length - 1; i++) {
                PropertyInfo pInfo = res.GetType().GetProperty(props[i], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if(pInfo == null)
                    return null;
                res = pInfo.GetValue(res, null);
                if(res == null)
                    break;
            }
            return res;
        }

        public static PropertyInfo GetPropertyInfo(string dataSourcePath, object root) {
            string[] props = dataSourcePath.Split('.');
            object res = root;
            PropertyInfo pInfo = null;
            for(int i = 0; i < props.Length; i++) {
                pInfo = res.GetType().GetProperty(props[i], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if(pInfo == null)
                    return null;
                res = pInfo.GetValue(res, null);
                if(res == null)
                    break;
            }
            return pInfo;
        }
    }
}
