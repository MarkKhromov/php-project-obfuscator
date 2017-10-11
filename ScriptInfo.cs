namespace PHPProjectObfuscator {
    struct ScriptInfo {
        public ScriptInfo(int position, string code) {
            Position = position;
            Code = code;
        }

        public readonly int Position;
        public readonly string Code;
    }
}