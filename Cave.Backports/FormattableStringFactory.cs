#if (NETSTANDARD1_0_OR_GREATER && ! NETSTANDARD2_0_OR_GREATER) || NET20_OR_GREATER

namespace System.Runtime.CompilerServices;

public static class FormattableStringFactory
{
    public static FormattableString Create(string format, params object[] arguments)
    {
        if (format == null)
        {
            throw new ArgumentNullException(nameof(format));
        }

        if (arguments == null)
        {
            throw new ArgumentNullException(nameof(arguments));
        }

        return new MyFormattableString(format, arguments);
    }

    sealed class MyFormattableString : FormattableString
    {
        readonly string format;
        readonly object[] arguments;

        internal MyFormattableString(string format, object[] arguments)
        {
            this.format = format;
            this.arguments = arguments;
        }

        public override string Format => format;
        public override object[] GetArguments() => arguments;
        public override int ArgumentCount => arguments.Length;
        public override object GetArgument(int index) => arguments[index];
        public override string ToString(IFormatProvider formatProvider) => string.Format(formatProvider, format, arguments);
    }
}

#endif
