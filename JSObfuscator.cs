using System;
using System.Diagnostics;
using System.IO;
using PHPProjectObfuscator.Core;

namespace PHPProjectObfuscator {
    class JSObfuscator {
        #region Inner classes
        struct ScriptInfo {
            public ScriptInfo(int position, string code) {
                Position = position;
                Code = code;
            }

            public readonly int Position;
            public readonly string Code;
        }
        #endregion

        public JSObfuscator(string code) {
            this.code = code;
        }

        readonly string code;

        static bool installed = false;

        const string JS_OBFUSCATOR_SCRIPT_FILENAME = "js_obfuscator.js";
        const string INSTALL_NPM_JAVASCRIPT_OBFUSCATOR = "/C npm install --save-dev javascript-obfuscator";
        static readonly string RUN_NODE_OBFUSCATE_SCRIPT = "/C node " + "\"" + Path.Combine(Environment.CurrentDirectory, JS_OBFUSCATOR_SCRIPT_FILENAME) + "\"";

        public string Obfuscate() {
            var template = ResourceReader.Read<string>("PHPProjectObfuscator.JSObfuscatorScript.template");
            var obfuscateScriptFilename = Path.Combine(Environment.CurrentDirectory, JS_OBFUSCATOR_SCRIPT_FILENAME);
            ITemplateBuilder templateBuilder = new JSScriptTemplateBuilder();
            File.WriteAllText(obfuscateScriptFilename, templateBuilder.Build(template, code));
            InstallPackages();
            var obfuscatedCode = ObfuscateCore(obfuscateScriptFilename);
            return obfuscatedCode;
        }

        void InstallPackages() {
            if(installed)
                return;
            using(var process = Process.Start("cmd.exe", INSTALL_NPM_JAVASCRIPT_OBFUSCATOR)) {
                process.WaitForExit();
            }
            installed = true;
        }

        string ObfuscateCore(string scriptFilename) {
            string obfuscateCode = null;
            var processInfo = new ProcessStartInfo {
                FileName = "cmd.exe",
                Arguments = RUN_NODE_OBFUSCATE_SCRIPT,
                UseShellExecute = false,
                RedirectStandardOutput = true
            };
            using(var process = Process.Start(processInfo)) {
                obfuscateCode = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
            }
            return obfuscateCode;
        }

        ScriptInfo[] GetScripts() {

            return null;
        }
    }
}