using System.IO;

namespace JetpackDowngrader
{
    public class Editor
    {
        // ZCF format and class version: 1.0.0.3
        // Author: Zalexanninev15
        // Repository of ZCF format: https://github.com/Zalexanninev15/ZCF
        // Documentation of ZCF format: https://github.com/Zalexanninev15/ZCF/blob/main/ZCF.md
        // Documentation of ZCF format for developers: https://github.com/Zalexanninev15/ZCF/blob/main/README.md#using
        //
        //
        /*  MIT License
       
        Copyright (c) 2021-2025 Zalexanninev15

        Permission is hereby granted, free of charge, to any person obtaining a copy
        of this software and associated documentation files (the "Software"), to deal
        in the Software without restriction, including without limitation the rights
        to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
        copies of the Software, and to permit persons to whom the Software is
        furnished to do so, subject to the following conditions:

        The above copyright notice and this permission notice shall be included in all
        copies or substantial portions of the Software.

        THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
        IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
        FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
        AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
        LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
        OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
        SOFTWARE.
        */

        string file = null;
        public Editor(string filename)  { file = filename; }

        public string GetValue(string variable)
        {
            if (file != null)
            {
                if (File.Exists(file))
                {
                    string[] lines = File.ReadAllLines(file);
                    string value = "";
                    foreach (string line in lines) 
                    { 
                        if ((!line.StartsWith("## ")) && (!line.StartsWith(" ")) && (!line.StartsWith("[$w]")) && (!line.StartsWith("[$d]")))
                        { 
                            if (line.StartsWith(variable + " <= ")) 
                            { 
                                value = line.Replace(variable + " <= ", ""); 
                                value = System.Text.RegularExpressions.Regex.Replace(value, @" ##.+$", "");
                            } 
                        } 
                    }
                    return value;
                }
                else { return "Error: The specified file does not exist!"; }
            }
           else { return "Error: The value of the variable cannot be set, because the settings file is not specified!"; }
        }
    }
}