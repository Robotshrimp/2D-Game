using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameJom
{
    public static class DataManager
    {
        // data modification
        public static List<string> IntListToString(List<int> list)// do not use. placed here as reminder to use convertall
        {
            List<string> result = list.ConvertAll(x => x.ToString());// use this instead of the function
            return result;
        }// convers the int array to string array
        // data labeling
        public static string FindLabeledData(List<string> labelList, string findLabel)
        {
            foreach (string labeledData in labelList)
            {
                string data = labeledData;
                if (CheckDataLable(ref data, findLabel))
                    return data;
            }
            return "";
        }
        public static bool CheckDataLable(ref string data, string nameofData)// returns of bool corresponding to wether or not the data is correct and remove the label if yes
        {
            string[] removeLabel = data.Split(':');
            if (removeLabel[0] == nameofData)
                return false;
            data = removeLabel[1];
            return true;
        }
        public static string DataLabeler(string data, string nameofData)// adds a label to a string and format it for serialization
        {
            return nameofData + ":" + data + System.Environment.NewLine;
        }
        // Storage to runtime
        public static List<string> ParseToStringList(string data, char splitMarker)// splits data string into string list using the split marker
        {
            List<string> result = new List<string>();
            string[] listvalues = data.Split(splitMarker);
            foreach (string value in listvalues)
            {
                result.Add(value);
            }
            return result;
        }
        public static Dictionary<string, string> ParseLabelListToDictionary(string data)
        {
            string[] labeledData = data.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            Dictionary<string, string> labelDictionary = new Dictionary<string, string>();
            foreach(string item in labeledData)
            {
                string[] splitLabel = item.Split(new string[] { " : " }, StringSplitOptions.None);
                labelDictionary.Add(splitLabel[0], splitLabel[1]);
            }
            return labelDictionary;
        }
        // runtime to storage
        public static string SerializeStringList(List<string> list, char dataMarker)
        {
            string result = "";
            foreach (string value in list)
            {
                result = result + value + dataMarker;
            }
            result.TrimEnd(dataMarker);
            return result;
        }
        public static string SerializeLabeledItem(string name, string data)
        {
            return name + " : " + data + Environment.NewLine;
        }
    }
}
