﻿using System.Collections.Generic;
using VDT.Core.XmlConverter.Elements;
using VDT.Core.XmlConverter.Nodes;

namespace VDT.Core.XmlConverter {
    public class ConverterOptions {
        public List<IElementConverter> ElementConverters { get; set; } = new List<IElementConverter>();

        public IElementConverter DefaultElementConverter { get; set; } = new NoOpElementConverter();

        public INodeConverter TextConverter { get; set; } = new NodeValueConverter(true);
    }
}