﻿#if (NETSTANDARD1_0_OR_GREATER && ! NETSTANDARD1_3_OR_GREATER) || ((NET20_OR_GREATER && !NET46_OR_GREATER) && ! NETCOREAPP2_0_OR_GREATER)

using System.Globalization;

namespace System;

public abstract class FormattableString : IFormattable
{
    public abstract string Format { get; }
    public abstract object[] GetArguments();
    public abstract int ArgumentCount { get; }
    public abstract object GetArgument(int index);
    public abstract string ToString(IFormatProvider formatProvider);
    string IFormattable.ToString(string ignored, IFormatProvider formatProvider) => ToString(formatProvider);
    public static string Invariant(FormattableString formattable)
    {
        if (formattable == null)
        {
            throw new ArgumentNullException(nameof(formattable));
        }

        return formattable.ToString(CultureInfo.InvariantCulture);
    }

    public override string ToString() => ToString(CultureInfo.CurrentCulture);
}

#endif
