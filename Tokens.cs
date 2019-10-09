using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FilePatcher
{
    class Tokens
    {
        public static string getData(string data, string getType)
        {
            bool boolType = false;
            bool getData = false;
            string type = "";
            string datas = "";
            foreach (char c in data)
            {
                if (c == '{')
                {
                    boolType = true;
                    continue;
                }
                if (c == '}')
                {
                    boolType = false;
                    continue;
                }
                if (c == '[')
                {
                    getData = true;
                    continue;
                }
                if (c == ']')
                {
                    getData = false;
                    continue;
                }
                if (boolType) {
                    type += c;
                }
                if (getData)
                {
                    datas += c;
                }
            }
            if (type == getType)
            {
                return datas;
            }
            return datas;
        }
    }
}
