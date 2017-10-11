namespace PHPProjectObfuscator.Core {
    interface ITemplateBuilder {
        string Build(string template, string code);
    }
}