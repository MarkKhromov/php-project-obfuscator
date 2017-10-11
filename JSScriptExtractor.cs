using System.Linq;
using System.Text.RegularExpressions;

namespace PHPProjectObfuscator {
    class JSScriptExtractor {
        public JSScriptExtractor(string code) {
            this.code = code;
        }

        readonly string code;

        // TODO: Improve pattern (problem <script><script></script></script>
        const string EXTRACT_REGEX_PATTERN = @"<script[^>]*>(?<Script>[\s\S]*?)<\/script>";

        public ScriptInfo[] Extract() {
            return Regex.Matches(code, EXTRACT_REGEX_PATTERN)
                .Cast<Match>()
                .Select(x => new ScriptInfo(x.Groups["Script"].Index, x.Groups["Script"].Value))
                .Where(x => !string.IsNullOrWhiteSpace(x.Code))
                .ToArray();
            ;
        }
    }
}