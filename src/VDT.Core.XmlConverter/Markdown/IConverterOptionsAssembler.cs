﻿namespace VDT.Core.XmlConverter.Markdown {
    internal interface IConverterOptionsAssembler {
        public void SetNodeRemovingConverterForNonMarkdownNodeTypes(ConverterOptions options);

        public void AddHeaderElementConverters(ConverterOptions options);
    }
}
