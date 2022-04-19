﻿using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace VDT.Core.XmlConverter.Markdown {
    /// <summary>
    /// Converter for rendering xml text as Markdown text
    /// </summary>
    public class TextConverter : INodeConverter {
        private static Regex whitespaceNormalizer = new Regex("\\s+", RegexOptions.Compiled);

        private static Dictionary<char, string> markdownEscapeCharacters = new Dictionary<char, string>() {
            { '\\', "\\\\" },
            { '`', "\\`" },
            { '*', "\\*" },
            { '_', "\\_" },
            { '{', "\\{" },
            { '}', "\\}" },
            { '[', "\\[" },
            { ']', "\\]" },
            { '(', "\\(" },
            { ')', "\\)" },
            { '#', "\\#" },
            { '+', "\\+" },
            { '-', "\\-" },
            { '.', "\\." },
            { '!', "\\!" },
            { '|', "\\|" },
            { '<', "&lt;" },
            { '>', "&gt;" },
            { '&', "&amp;" },
            { '\r', "" },
            { '\n', "" }
        };

        /// <inheritdoc/>
        public void Convert(XmlReader reader, TextWriter writer, NodeData data) {
            var value = whitespaceNormalizer.Replace(reader.Value.Trim(), " ");
            
            foreach (var c in value) {
                if (markdownEscapeCharacters.TryGetValue(c, out var str)) {
                    writer.Write(str);
                }
                else {
                    writer.Write(c);
                }
            }
        }
    }
}
