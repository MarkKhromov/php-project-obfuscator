namespace PHPProjectObfuscator.Core {
    class JSScriptTemplateBuilder : ITemplateBuilder {
        const string SCRIPT_PLACEMENT = "SCRIPT_PLACEMENT";

        string ITemplateBuilder.Build(string template, string code) {
            var scriptPlacementIndex = template.IndexOf(SCRIPT_PLACEMENT);
            var obfuscateScript = template.Remove(scriptPlacementIndex, SCRIPT_PLACEMENT.Length).Insert(scriptPlacementIndex, code);
            return obfuscateScript;
        }
    }
}