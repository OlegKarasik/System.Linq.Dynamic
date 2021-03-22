﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace DynamicLinqWebDocs.Models
{
    public class Argument
    {
        [XmlAttribute(AttributeName = "typeNamespace")]
        public string TypeNamespace { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}