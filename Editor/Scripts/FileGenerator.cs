using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;

namespace ExtendedEditorWindows {
    
    public static class FileGenerator {

        public static async Task Generate(string fileName, string[] lines, Action callback = null) {

            var currentLine = 0;
            var totalLines = lines.Length;

            try {

                if (File.Exists(fileName)) {
                    File.Delete(fileName);
                }

                using var sw = File.CreateText(fileName);
                
                foreach(var line in lines) {
                    await sw.WriteLineAsync(line);
                    currentLine++;
                    if (currentLine < totalLines) continue;
                    sw.Close();
                    AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
                    callback?.Invoke();
                }

            } catch (Exception exception) {
            
                Console.WriteLine(exception.ToString());
            
            }

        }

    }
    
}
