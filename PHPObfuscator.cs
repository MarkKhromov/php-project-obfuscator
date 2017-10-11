using System;
using System.Diagnostics;
using System.IO;

namespace PHPProjectObfuscator {
    class PHPObfuscator : IObfuscator {
        public PHPObfuscator(string code) {
            this.code = code;
        }

        readonly string code;

        public string Obfuscate() {
            CloneRepository();
            InstallPackages();
            return ObfuscateCore();
        }

        string ObfuscateCore() {
            var tmpDir = Path.Combine(Environment.CurrentDirectory, "tmp");
            var tmpFile = Path.Combine(tmpDir, "script.php");
            Directory.CreateDirectory(tmpDir);
            var tmpResultDir = Path.Combine(Environment.CurrentDirectory, "tmp_r");
            var tmpResultFile = Path.Combine(tmpResultDir, "script.php");
            Directory.CreateDirectory(tmpResultDir);
            File.WriteAllText(tmpFile, code);
            var commands = string.Join("&", new[] {
                "cd php-obfuscator/bin",
                $"php ./obfuscate obfuscate {tmpDir} {tmpResultDir}"
            });
            var processInfo = new ProcessStartInfo {
                FileName = "cmd.exe",
                Arguments = $"/C {commands}"
            };
            using(var process = Process.Start(processInfo)) {
                process.WaitForExit();
            }
            var obfuscatedCode = File.ReadAllText(tmpResultFile);
            Directory.Delete(tmpDir, true);
            Directory.Delete(tmpResultDir, true);
            return obfuscatedCode;
        }

        void CloneRepository() {
            var processInfo = new ProcessStartInfo {
                FileName = "cmd.exe",
                Arguments = $"/C git clone https://github.com/naneau/php-obfuscator.git php-obfuscator"
            };
            using(var process = Process.Start(processInfo)) {
                process.WaitForExit();
            }
        }

        void InstallPackages() {
            var commands = string.Join("&", new[] {
                $"cd php-obfuscator",
                "composer install"
            });
            var processInfo = new ProcessStartInfo {
                FileName = "cmd.exe",
                Arguments = $"/C {commands}"
            };
            using(var process = Process.Start(processInfo)) {
                process.WaitForExit();
            }
        }
    }
}